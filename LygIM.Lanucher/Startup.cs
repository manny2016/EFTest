using LygIM.Buiness;
using LygIM.Models;
using Mehdime.Entity;
using Microsoft.Extensions.Hosting;

using NLog;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LygIM.Lanucher
{
	public class Startup : IHostedService
	{

		private readonly IDbContextScopeFactory _dbContextScopeFactory;

		private readonly IWorkspaceRepository _workspaceRepository;

		private readonly IAmbientDbContextLocator _ambientDbContextLocator;
		public Startup(IDbContextScopeFactory dbContextScopeFactory,
			IWorkspaceRepository workspaceRepository)
		{

			_dbContextScopeFactory = dbContextScopeFactory;

			_workspaceRepository = workspaceRepository;


		}

		private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

		public async Task StartAsync(CancellationToken cancellationToken)
		{
			Logger.Info("Starting...");

			using (var dbContextScope = _dbContextScopeFactory.Create(new AuditContext(Guid.NewGuid(), "d", "d")))
			{
				try
				{
					var entity = await _workspaceRepository.GetAsync(Guid.Parse("73fc973a-dbd7-ec11-9edd-680715f2d888"));

					entity.Name = DateTime.Now.ToString("HH:mm:ss");
					foreach(var configuration in entity.Configurations)
					{
						configuration.Value = DateTime.Now.ToString("HH:mm:ss");
					}

					_workspaceRepository.Update(entity);

					var workspace = new Workspace() { Name = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };

					_workspaceRepository.Add(workspace);

					await dbContextScope.SaveChangesAsync();
				}
				catch (InvalidOperationException ex)
				{
					Logger.Error(ex.Message);
				}
			}
			Logger.Info("End");
			Console.WriteLine("Press any key to exit.");

		}

		public async Task StopAsync(CancellationToken cancellationToken)
		{

		}
	}
}
