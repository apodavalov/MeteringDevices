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

        [Column("KitchenCold")]
        public int KitchenCold
        {
            get;
            set;
        }

        [Column("KitchenHot")]
        public int KitchenHot
        {
            get;
            set;
        }

        [Column("BathroomCold")]
        public int BathroomCold
        {
            get;
            set;
        }

        [Column("BathroomHot")]
        public int BathroomHot
        {
            get;
            set;
        }
    }
}
