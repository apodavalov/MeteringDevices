using ModernRoute.WildData.Core;
using ModernRoute.WildData.Npgsql.Core;
using System;

namespace MeteringDevices.Data
{
    class Session : BaseSession, ISession
    {
        private Lazy<IReadOnlyRepository<Kzn.CurrentMeteringValue>> _KznCurrentMeteringValueRepository;
        private Lazy<IReadWriteRepository<Kzn.MeteringValue, int>> _KznMeteringValueRepository;

        private Lazy<IReadOnlyRepository<Spb.CurrentMeteringValue>> _SpbCurrentMeteringValueRepository;
        private Lazy<IReadWriteRepository<Spb.MeteringValue, int>> _SpbMeteringValueRepository;

        public Session(string connectionString) : base(connectionString)
        {
            _KznCurrentMeteringValueRepository = new Lazy<IReadOnlyRepository<Kzn.CurrentMeteringValue>>(CreateReadOnlyRepository<Kzn.CurrentMeteringValue>);
            _KznMeteringValueRepository = new Lazy<IReadWriteRepository<Kzn.MeteringValue,int>>(CreateReadWriteRepository<Kzn.MeteringValue,int>);

            _SpbCurrentMeteringValueRepository = new Lazy<IReadOnlyRepository<Spb.CurrentMeteringValue>>(CreateReadOnlyRepository<Spb.CurrentMeteringValue>);
            _SpbMeteringValueRepository = new Lazy<IReadWriteRepository<Spb.MeteringValue, int>>(CreateReadWriteRepository<Spb.MeteringValue, int>);
        }

        public IReadOnlyRepository<Kzn.CurrentMeteringValue> KznCurrentMeteringValueRepository
        {
            get
            {
                return _KznCurrentMeteringValueRepository.Value;
            }
        }

        public IReadWriteRepository<Kzn.MeteringValue, int> KznMeteringValueRepository
        {
            get
            {
                return _KznMeteringValueRepository.Value;
            }
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
