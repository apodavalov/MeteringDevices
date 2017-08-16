using MeteringDevices.Data;

namespace MeteringDevices.Core
{
    interface ISessionFactory
    {
        ISession GetSession();
    }
}
