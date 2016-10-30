using ModernRoute.WildData.Core;

namespace MeteringDevices.Database
{
    interface ISession : IBaseSession
    {
        IReadOnlyRepository<CurrentMeteringValue> CurrentMeteringValueRepository { get; }
        IReadWriteRepository<MeteringValue, int> MeteringValueRepository { get; }
    }
}
