using Ninject.Modules;
using MeteringDevices.Core.RestSharp;
using MeteringDevices.Core.Notification;
using Ninject.Activation;
using Ninject;
using MeteringDevices.Core.Common;

namespace MeteringDevices.Core
{
    public class Register : NinjectModule
    {
        private const string _TelegramNotifierToken = "Telegram.Notifier.Token";
        private const string _TelegramNotifierChatId = "Telegram.Notifier.ChatId";

        private const string _KznAccountNumberKey = "Kzn.AccountNumber";
        private const string _KznEnabledKey = "Kzn.Enabled";
        private const string _KznMeteringDeviceDayIdKey = "Kzn.MeteringDevice.DayId";
        private const string _KznMeteringDeviceNightIdKey = "Kzn.MeteringDevice.NightId";
        private const string _KznMeteringDeviceColdIdKey = "Kzn.MeteringDevice.ColdId";
        private const string _KznMeteringDeviceHotIdKey = "Kzn.MeteringDevice.HotId";

        private const string _UsernameKey = "Spb.Service.Username";
        private const string _PasswordKey = "Spb.Service.Password";

        private const string _SpbAccountNumberKey = "Spb.AccountNumber";
        private const string _SpbEnabledKey = "Spb.Enabled";
        private const string _SpbMeteringDeviceDayIdKey = "Spb.MeteringDevice.DayId";
        private const string _SpbMeteringDeviceNightIdKey = "Spb.MeteringDevice.NightId";
        
        public override void Load()
        {
            Kernel.Bind<ISessionFactory>().To<SessionFactory>().InSingletonScope();
            Kernel.Bind<IJsonSerializerFactory>().To<JsonSerializerFactory>().InSingletonScope();
            Kernel.Bind<IRestSharpFactory>().To<RestSharpFactory>().InSingletonScope();
            Kernel.Bind<INotifier>().ToMethod(CreateNotifier).InSingletonScope();
            Kernel.Bind<IApp>().ToMethod(CreateApp).InSingletonScope();
        }

        private IApp CreateApp(IContext arg)
        {
            return new App(
                new DataProvider(
                    arg.Kernel.Get<ISessionFactory>(),
                    arg.Kernel.Get<INotifier>(),
                    new Kzn.RetrieverService(
                        ConfigUtils.GetStringFromConfig(_KznMeteringDeviceDayIdKey),
                        ConfigUtils.GetStringFromConfig(_KznMeteringDeviceNightIdKey),
                        ConfigUtils.GetStringFromConfig(_KznMeteringDeviceColdIdKey),
                        ConfigUtils.GetStringFromConfig(_KznMeteringDeviceHotIdKey)
                    ),
                    new Kzn.SendService(),
                    ConfigUtils.GetBoolFromConfig(_KznEnabledKey),
                    ConfigUtils.GetStringFromConfig(_KznAccountNumberKey)
                ),
                new DataProvider(
                    arg.Kernel.Get<ISessionFactory>(),
                    arg.Kernel.Get<INotifier>(),
                    new Spb.RetrieverService(
                        ConfigUtils.GetStringFromConfig(_SpbMeteringDeviceDayIdKey),
                        ConfigUtils.GetStringFromConfig(_SpbMeteringDeviceNightIdKey)
                    ),
                    new Spb.SendService(),
                    ConfigUtils.GetBoolFromConfig(_SpbEnabledKey),
                    ConfigUtils.GetStringFromConfig(_SpbAccountNumberKey)
               )
            );
        }

        private INotifier CreateNotifier(IContext arg)
        {
            return new TelegramNotifier(
                ConfigUtils.GetStringFromConfig(_TelegramNotifierToken), 
                ConfigUtils.GetLongFromConfig(_TelegramNotifierChatId),
                Kernel.Get<IRestSharpFactory>()
            );
        }
    }
}
