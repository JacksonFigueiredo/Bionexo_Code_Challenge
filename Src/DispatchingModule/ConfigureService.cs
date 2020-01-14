using DispatchingModule.Business;
using Topshelf;

namespace DispatchingModule
{
    public class ConfigureService
    {
        internal static void Configure()
        {
            HostFactory.Run(configure =>
            {
                configure.Service<DispatchingService>(service =>
                {
                    service.ConstructUsing(s => new DispatchingService());
                    service.WhenStarted(s => s.Start());
                    service.WhenStopped(s => s.Stop());
                });

                configure.RunAsLocalSystem();
                configure.SetServiceName("Dispatching");
                configure.SetDisplayName("Dispatching");
                configure.SetDescription("Service Client Made to Bionexo to Store computer in one base, and then foward to another");
            });
        }
    }
}
