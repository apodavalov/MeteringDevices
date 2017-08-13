using ModernRoute.WildData.Core;

namespace MeteringDevices.Data
{
    public interface ISession : IBaseSession
    {
        IReadOnlyRepository<Kzn.CurrentMeteringValue> CurrentMeteringValueRepository { get; }
        IReadWriteRepository<Kzn.MeteringValue, int> MeteringValueRepository { get; }
    }
}
