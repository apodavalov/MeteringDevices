using MeteringDevices.Data;

namespace MeteringDevices.Service
{
    interface ISessionFactory
    {
        ISession GetSession();
    }
}
