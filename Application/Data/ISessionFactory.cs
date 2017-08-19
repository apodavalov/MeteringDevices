namespace MeteringDevices.Data
{
    public interface ISessionFactory
    {
        ISession OpenSession();
    }
}
