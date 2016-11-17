using ModernRoute.WildData.Core;
using ModernRoute.WildData.Npgsql.Core;
using System;
using System.Configuration;

namespace MeteringDevices.Data
{
    class Session : BaseSession, ISession
    {
        private Lazy<IReadOnlyRepository<CurrentMeteringValue>> _CurrentMeteringValueRepository;
        private Lazy<IReadWriteRepository<MeteringValue, int>> _MeteringValueRepository;

        public Session(string connectionString) : base(connectionString)
        {
            _CurrentMeteringValueRepository = new Lazy<IReadOnlyRepository<CurrentMeteringValue>>(CreateReadOnlyRepository<CurrentMeteringValue>);
            _MeteringValueRepository = new Lazy<IReadWriteRepository<MeteringValue,int>>(CreateReadWriteRepository<MeteringValue,int>);
        }

        public IReadOnlyRepository<CurrentMeteringValue> CurrentMeteringValueRepository
        {
            get
            {
                return _CurrentMeteringValueRepository.Value;
            }
        }

        public IReadWriteRepository<MeteringValue, int> MeteringValueRepository
        {
            get
            {
                return _MeteringValueRepository.Value;
            }
        }
    }
}
