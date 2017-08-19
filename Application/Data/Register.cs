using Ninject.Activation;
using Ninject.Modules;
using System.Configuration;

namespace MeteringDevices.Data
{
    public class Register : NinjectModule
    {
        private const string _ConnectionStringName = "Main";

        private ISessionFactory CreateSessionFactory(IContext context)
        {
            return new SessionFactory(ConfigurationManager.ConnectionStrings[_ConnectionStringName].ConnectionString);
        }

        public override void Load()
        {
            Kernel.Bind<ISessionFactory>().ToMethod(CreateSessionFactory);
        }
    }
}
