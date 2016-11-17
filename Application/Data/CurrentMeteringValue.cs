using ModernRoute.WildData.Attributes;
using ModernRoute.WildData.Models;
using System;

namespace MeteringDevices.Data
{
    [Storage("CurrentValues")]
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


        [Column("Hot")]
        public int Hot
        {
            get;
            set;
        }

        [Column("Cold")]
        public int Cold
        {
            get;
            set;
        }

        [Column("Kitchen")]
        public int Kitchen
        {
            get;
            set;
        }

        [Column("Room")]
        public int Room
        {
            get;
            set;
        }
    }
}
