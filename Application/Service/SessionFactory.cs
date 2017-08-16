using System;
using MeteringDevices.Data;
using Ninject;

namespace MeteringDevices.Service
{
    class SessionFactory : ISessionFactory
    {
        private readonly IKernel _Kernel;

        public SessionFactory(IKernel kernel)
        {
            if (kernel == null)
            {
                throw new ArgumentNullException(nameof(kernel));
            }

            _Kernel = kernel;
        }

        public ISession GetSession()
        {
            return _Kernel.Get<ISession>();
        }
    }
}
