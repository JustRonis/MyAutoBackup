using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace AutoBackup
{
    internal class ConfigureService
    {
        internal static void Configure()
        {
            HostFactory.Run(configure =>
            {
                configure.Service<ServicoAutoBackup>(service =>
                {
                    service.ConstructUsing(s => new ServicoAutoBackup());
                    service.WhenStarted(s => s.Start());
                    service.WhenStopped(s => s.Stop());
                });
                configure.RunAsLocalSystem();
                configure.SetServiceName("AutoBackup");
                configure.SetDisplayName("AutoBackup");
                configure.SetDescription("Servico que move determinada pasta para a nuvem do OneDrive");
            });
        }
    }
}
