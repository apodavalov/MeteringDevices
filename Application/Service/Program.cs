using Ninject;

namespace MeteringDevices.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            IKernel kernel = new StandardKernel();
            kernel.Load(new Data.Register());
            kernel.Load(new Core.Register());

            kernel.Get<Core.IApp>().Run();
        }
    }
}
