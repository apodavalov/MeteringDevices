using Ninject.Activation;
using Ninject.Modules;
using System.Configuration;

namespace MeteringDevices.Data
{
    public class Register : NinjectModule
    {
        private const string _ConnectionStringName = "Main";

        private ISession CreateSession(IContext context)
        {
            return new Session(ConfigurationManager.ConnectionStrings[_ConnectionStringName].ConnectionString);
        }

        public override void Load()
        {
            Kernel.Bind<ISession>().ToMethod(CreateSession);
        }
    }
}
