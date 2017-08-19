using System;

namespace MeteringDevices.Data
{
    class SessionFactory : ISessionFactory
    {
        private readonly string _ConnectionString;

        public SessionFactory(string connectionString)
        {
            if (connectionString == null)
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            _ConnectionString = connectionString;
        }

        public ISession OpenSession()
        {
            return new Session(_ConnectionString);
        }
    }
}
