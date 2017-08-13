using ModernRoute.WildData.Attributes;
using ModernRoute.WildData.Models;
using System;

namespace MeteringDevices.Data.Spb
{
    [Storage("SpbCurrentValues")]
    public class CurrentMeteringValue : IReadOnlyModel
    {
        [Column("When")]
        public virtual DateTime When
        {
            get;
            set;
        }

        [Column("Day")]
        public int Day
        {
            get;
            set;
        }

        [Column("Night")]
        public int Night
        {
            get;
            set;
        }
    }
}
