using MeteringDevices.Core.Common;
using MeteringDevices.Core.Notification;
using MeteringDevices.Core.RestSharp;
using MeteringDevices.Data;
using Ninject;
using Ninject.Activation;
using Ninject.Modules;
using System;

namespace MeteringDevices.Core
{
    public class Register : NinjectModule
    {
        private const string _IMSenderTelegramBaseUrl = "IMSender.Telegram.BaseUrl";
        private const string _IMSenderTelegramToken = "IMSender.Telegram.Token";
        private const string _IMSenderTelegramChatId = "IMSender.Telegram.ChatId";
        private const string _IMSenderTelegramProxyUri = "IMSender.Telegram.ProxyUri";

        private const string _KznUsernameKey = "Kzn.Service.Username";
        private const string _KznPasswordKey = "Kzn.Service.Password";
        private const string _KznBaseUrlKey = "Kzn.Service.BaseUrl";

        private const string _KznAccountNumberKey = "Kzn.AccountNumber";
        private const string _KznEnabledKey = "Kzn.Enabled";
        private const string _KznDayOfMonthToSendKey = "Kzn.DayOfMonthToSend";
        private const string _KznMeteringDeviceDayIdKey = "Kzn.MeteringDevice.DayId";
        private const string _KznMeteringDeviceNightIdKey = "Kzn.MeteringDevice.NightId";
        private const string _KznMeteringDeviceColdIdKey = "Kzn.MeteringDevice.ColdId";
        private const string _KznMeteringDeviceHotIdKey = "Kzn.MeteringDevice.HotId";

        private const string _SpbAccountNumberKey = "Spb.AccountNumber";
        private const string _SpbEnabledKey = "Spb.Enabled";
        private const string _SpbDayOfMonthToSendKey = "Spb.DayOfMonthToSend";
        private const string _SpbMeteringDeviceDayLabelKey = "Spb.MeteringDevice.DayLabel";
        private const string _SpbMeteringDeviceNightLabelIdKey = "Spb.MeteringDevice.NightLabel";
        private const string _SpbMeteringDeviceKitchenColdLabelKey = "Spb.MeteringDevice.KitchenColdLabel";
        private const string _SpbMeteringDeviceKitchenHotLabelKey = "Spb.MeteringDevice.KitchenHotLabel";
        private const string _SpbMeteringDeviceBathroomColdLabelKey = "Spb.MeteringDevice.BathroomColdLabel";
        private const string _SpbMeteringDeviceBathroomHotLabelKey = "Spb.MeteringDevice.BathroomHotLabel";

        public override void Load()
        {
            Kernel.Bind<Kzn.ISendApiService>().ToMethod(CreateKznSendApiService).InSingletonScope();
            Kernel.Bind<IJsonSerializerFactory>().To<JsonSerializerFactory>().InSingletonScope();
            Kernel.Bind<IRestSharpFactory>().To<RestSharpFactory>().InSingletonScope();
            Kernel.Bind<INotifier>().ToMethod(CreateNotifier).InSingletonScope();
            Kernel.Bind<IIMSender>().ToMethod(CreateIMSender).InSingletonScope();
            Kernel.Bind<ICurrentDateTimeProvider>().To<CurrentDateTimeProvider>().InSingletonScope();
            Kernel.Bind<IApp>().ToMethod(CreateApp).InSingletonScope();
        }

        private Kzn.ISendApiService CreateKznSendApiService(IContext arg)
        {
            return new Kzn.SendApiService(arg.Kernel.Get<IRestSharpFactory>(), ConfigUtils.GetStringFromConfig(_KznBaseUrlKey));
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
                    new Kzn.SendService(
                        arg.Kernel.Get<Kzn.ISendApiService>(),
                        ConfigUtils.GetStringFromConfig(_KznUsernameKey),
                        ConfigUtils.GetStringFromConfig(_KznPasswordKey)
                    ),
                    arg.Kernel.Get<ICurrentDateTimeProvider>(),
                    ConfigUtils.GetBoolFromConfig(_KznEnabledKey),
                    ConfigUtils.GetStringFromConfig(_KznAccountNumberKey),
                    ConfigUtils.GetIntFromConfig(_KznDayOfMonthToSendKey)
                ),
                new DataProvider(
                    arg.Kernel.Get<ISessionFactory>(),
                    arg.Kernel.Get<INotifier>(),
                    new Spb.RetrieverService(
                        ConfigUtils.GetStringFromConfig(_SpbMeteringDeviceDayLabelKey),
                        ConfigUtils.GetStringFromConfig(_SpbMeteringDeviceNightLabelIdKey),
                        ConfigUtils.GetStringFromConfig(_SpbMeteringDeviceKitchenColdLabelKey),
                        ConfigUtils.GetStringFromConfig(_SpbMeteringDeviceKitchenHotLabelKey),
                        ConfigUtils.GetStringFromConfig(_SpbMeteringDeviceBathroomColdLabelKey),
                        ConfigUtils.GetStringFromConfig(_SpbMeteringDeviceBathroomHotLabelKey)
                    ),
                    new Spb.SendService(arg.Kernel.Get<IIMSender>()),
                    arg.Kernel.Get<ICurrentDateTimeProvider>(),
                    ConfigUtils.GetBoolFromConfig(_SpbEnabledKey),
                    ConfigUtils.GetStringFromConfig(_SpbAccountNumberKey),
                    ConfigUtils.GetIntFromConfig(_SpbDayOfMonthToSendKey)
               )
            );
        }

        private INotifier CreateNotifier(IContext arg)
        {
            return new IMNotifier(arg.Kernel.Get<IIMSender>());
        }

        private IIMSender CreateIMSender(IContext arg)
        {
            string proxyUriString = ConfigUtils.GetStringFromConfig(_IMSenderTelegramProxyUri);

            return new TelegramSender(
                ConfigUtils.GetStringFromConfig(_IMSenderTelegramBaseUrl),
                ConfigUtils.GetStringFromConfig(_IMSenderTelegramToken),
                ConfigUtils.GetLongFromConfig(_IMSenderTelegramChatId),
                arg.Kernel.Get<IRestSharpFactory>(),
                proxyUriString == null ? null : new Uri(proxyUriString)                
            );
        }
    }
}
