using ModernRoute.WildData.Core;

namespace MeteringDevices.Data
{
    public interface ISession : IBaseSession
    {
        IReadOnlyRepository<CurrentMeteringValue> CurrentMeteringValueRepository { get; }
        IReadWriteRepository<MeteringValue, int> MeteringValueRepository { get; }
    }
}
