using ModernRoute.WildData.Attributes;
using ModernRoute.WildData.Models;
using System;

namespace MeteringDevices.Data
{
    [Storage("Values")]
    public class MeteringValue : CurrentMeteringValue, IReadWriteModel<int>
    {
        private bool _Persistent = false;

        [VolatileOnStore]
        [VolatileOnUpdate]
        [Column("When")]
        public override DateTime When
        {
            get;
            set;
        }

        [VolatileOnStore]
        [Column("Id")]
        public int Id
        {
            get;
            set;
        }
        
        public bool IsPersistent()
        {
            return _Persistent;
        }

        public void SetPersistent(bool value)
        {
            _Persistent = value;
        }
    }
}
