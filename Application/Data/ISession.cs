using ModernRoute.WildData.Core;

namespace MeteringDevices.Data
{
    public interface ISession : IBaseSession
    {
        IReadOnlyRepository<Kzn.CurrentMeteringValue> KznCurrentMeteringValueRepository { get; }
        IReadWriteRepository<Kzn.MeteringValue, int> KznMeteringValueRepository { get; }

        IReadOnlyRepository<Spb.CurrentMeteringValue> SpbCurrentMeteringValueRepository { get; }
        IReadWriteRepository<Spb.MeteringValue, int> SpbMeteringValueRepository { get; }
    }
}
