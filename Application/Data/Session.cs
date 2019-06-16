using ModernRoute.WildData.Core;
using ModernRoute.WildData.Npgsql.Core;
using System;

namespace MeteringDevices.Data
{
    class Session : BaseSession, ISession
    {
        private Lazy<IReadOnlyRepository<Spb.CurrentMeteringValue>> _SpbCurrentMeteringValueRepository;
        private Lazy<IReadWriteRepository<Spb.MeteringValue, int>> _SpbMeteringValueRepository;

        public Session(string connectionString) : base(connectionString)
        {
            _SpbCurrentMeteringValueRepository = new Lazy<IReadOnlyRepository<Spb.CurrentMeteringValue>>(CreateReadOnlyRepository<Spb.CurrentMeteringValue>);
            _SpbMeteringValueRepository = new Lazy<IReadWriteRepository<Spb.MeteringValue, int>>(CreateReadWriteRepository<Spb.MeteringValue, int>);
        }


        public IReadOnlyRepository<Spb.CurrentMeteringValue> SpbCurrentMeteringValueRepository
        {
            get
            {
                return _SpbCurrentMeteringValueRepository.Value;
            }
        }

        public IReadWriteRepository<Spb.MeteringValue, int> SpbMeteringValueRepository
        {
            get
            {
                return _SpbMeteringValueRepository.Value;
            }
        }
    }
}
