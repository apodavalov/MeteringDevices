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
        private const string _MailSenderHost = "MailSender.Host";
        private const string _MailSenderPort = "MailSender.Port";
        private const string _MailSenderEnableSsl = "MailSender.EnableSsl";
        private const string _MailSenderFromAddress = "MailSender.FromAddress";
        private const string _MailSenderPassword = "MailSender.Password";
        private const string _MailSenderToAddress = "MailSender.ToAddress";
        private const string _MailSenderSubject = "MailSender.Subject";

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
            Kernel.Bind<IJsonSerializerFactory>().To<JsonSerializerFactory>().InSingletonScope();
            Kernel.Bind<IRestSharpFactory>().To<RestSharpFactory>().InSingletonScope();
            Kernel.Bind<INotifier>().ToMethod(CreateNotifier).InSingletonScope();
            Kernel.Bind<IMessageSender>().ToMethod(CreateMessageSender).InSingletonScope();
            Kernel.Bind<ICurrentDateTimeProvider>().To<CurrentDateTimeProvider>().InSingletonScope();
            Kernel.Bind<IApp>().ToMethod(CreateApp).InSingletonScope();
        }

        private IApp CreateApp(IContext arg)
        {
            return new App(
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
                    new Spb.SendService(arg.Kernel.Get<IMessageSender>()),
                    arg.Kernel.Get<ICurrentDateTimeProvider>(),
                    ConfigUtils.GetBoolFromConfig(_SpbEnabledKey),
                    ConfigUtils.GetStringFromConfig(_SpbAccountNumberKey),
                    ConfigUtils.GetIntFromConfig(_SpbDayOfMonthToSendKey)
               )
            );
        }

        private INotifier CreateNotifier(IContext arg)
        {
            return new Notifier(arg.Kernel.Get<IMessageSender>());
        }

        private IMessageSender CreateMessageSender(IContext arg)
        {
            return new MailSender(
                ConfigUtils.GetStringFromConfig(_MailSenderHost),
                ConfigUtils.GetIntFromConfig(_MailSenderPort),
                ConfigUtils.GetBoolFromConfig(_MailSenderEnableSsl),
                ConfigUtils.GetStringFromConfig(_MailSenderFromAddress),
                ConfigUtils.GetStringFromConfig(_MailSenderPassword),
                ConfigUtils.GetStringFromConfig(_MailSenderToAddress),
                ConfigUtils.GetStringFromConfig(_MailSenderSubject)                
            );
        }
    }
}
