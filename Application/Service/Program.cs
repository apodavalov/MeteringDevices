using MeteringDevices.Data;
using Ninject;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace MeteringDevices.Service
{
    class Program
    {
        private const string _KznUsernameKey = "Kzn.Service.Username";
        private const string _KznPasswordKey = "Kzn.Service.Password";
        private const string _KznAccountNumberKey = "Kzn.Flat.AccountNumber";
        private const string _KznMeteringDeviceDayIdKey = "Kzn.MeteringDevice.DayId";
        private const string _KznMeteringDeviceNightIdKey = "Kzn.MeteringDevice.NightId";
        private const string _KznMeteringDeviceColdIdKey = "Kzn.MeteringDevice.ColdId";
        private const string _KznMeteringDeviceHotIdKey = "Kzn.MeteringDevice.HotId";        

        public static IKernel Kernel { get; private set; }

        static void Main(string[] args)
        {
            Kernel = new StandardKernel();
            Kernel.Load(new Register());

            try
            {
                Data.Kzn.CurrentMeteringValue currentValues;

                using (ISession session = Kernel.Get<ISession>())
                {
                    currentValues = session.CurrentMeteringValueRepository.Fetch().AsEnumerable().SingleOrDefault();
                }

                IDictionary<string, int> values = new Dictionary<string, int>(StringComparer.Ordinal)
                {
                    { ConfigurationManager.AppSettings[_KznMeteringDeviceDayIdKey], currentValues.Day },
                    { ConfigurationManager.AppSettings[_KznMeteringDeviceNightIdKey], currentValues.Night },
                    { ConfigurationManager.AppSettings[_KznMeteringDeviceColdIdKey], currentValues.Cold },
                    { ConfigurationManager.AppSettings[_KznMeteringDeviceHotIdKey], currentValues.Hot }
                };

                Kzn.GovService govService = new Kzn.GovService();

                govService.Login(ConfigurationManager.AppSettings[_KznUsernameKey], ConfigurationManager.AppSettings[_KznPasswordKey]);                
                govService.PutValues(values, ConfigurationManager.AppSettings[_KznAccountNumberKey]);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
