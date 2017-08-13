using ModernRoute.WildData.Core;
using ModernRoute.WildData.Npgsql.Core;
using System;

namespace MeteringDevices.Data
{
    class Session : BaseSession, ISession
    {
        private Lazy<IReadOnlyRepository<Kzn.CurrentMeteringValue>> _CurrentMeteringValueRepository;
        private Lazy<IReadWriteRepository<Kzn.MeteringValue, int>> _MeteringValueRepository;

        public Session(string connectionString) : base(connectionString)
        {
            _CurrentMeteringValueRepository = new Lazy<IReadOnlyRepository<Kzn.CurrentMeteringValue>>(CreateReadOnlyRepository<Kzn.CurrentMeteringValue>);
            _MeteringValueRepository = new Lazy<IReadWriteRepository<Kzn.MeteringValue,int>>(CreateReadWriteRepository<Kzn.MeteringValue,int>);
        }

        public IReadOnlyRepository<Kzn.CurrentMeteringValue> CurrentMeteringValueRepository
        {
            get
            {
                return _CurrentMeteringValueRepository.Value;
            }
        }

        public IReadWriteRepository<Kzn.MeteringValue, int> MeteringValueRepository
        {
            get
            {
                return _MeteringValueRepository.Value;
            }
        }
    }
}
