using ModernRoute.WildData.Core;

namespace MeteringDevices.Data
{
    public interface ISession : IBaseSession
    {
        IReadOnlyRepository<Spb.CurrentMeteringValue> SpbCurrentMeteringValueRepository { get; }
        IReadWriteRepository<Spb.MeteringValue, int> SpbMeteringValueRepository { get; }
    }
}
