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
        private const string _UsernameKey = "Service.Username";
        private const string _PasswordKey = "Service.Password";
        private const string _AccountNumberKey = "Flat.AccountNumber";
        private const string _MeteringDeviceDayIdKey = "MeteringDevice.DayId";
        private const string _MeteringDeviceNightIdKey = "MeteringDevice.NightId";
        private const string _MeteringDeviceColdIdKey = "MeteringDevice.ColdId";
        private const string _MeteringDeviceHotIdKey = "MeteringDevice.HotId";        

        public static IKernel Kernel { get; private set; }

        static void Main(string[] args)
        {
            Kernel = new StandardKernel();
            Kernel.Load(new Data.Register());

            try
            {
                CurrentMeteringValue currentValues;

                using (ISession session = Kernel.Get<ISession>())
                {
                    currentValues = session.CurrentMeteringValueRepository.Fetch().AsEnumerable().SingleOrDefault();
                }

                IDictionary<string, int> values = new Dictionary<string, int>
                {
                    { ConfigurationManager.AppSettings[_MeteringDeviceDayIdKey], currentValues.Day },
                    { ConfigurationManager.AppSettings[_MeteringDeviceNightIdKey], currentValues.Night },
                    { ConfigurationManager.AppSettings[_MeteringDeviceColdIdKey], currentValues.Cold },
                    { ConfigurationManager.AppSettings[_MeteringDeviceHotIdKey], currentValues.Hot }
                };

                GovService govService = new GovService();

                govService.Login(ConfigurationManager.AppSettings[_UsernameKey], ConfigurationManager.AppSettings[_PasswordKey]);                
                govService.PutValues(values, ConfigurationManager.AppSettings[_AccountNumberKey]);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
