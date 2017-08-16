using Ninject.Modules;
using MeteringDevices.Service.RestSharp;
using MeteringDevices.Service.Notification;
using Ninject.Activation;
using Ninject;

namespace MeteringDevices.Service
{
    class Register : NinjectModule
    {
        private const string _TelegramNotifierToken = "Telegram.Notifier.Token";
        private const string _TelegramNotifierChatId = "Telegram.Notifier.ChatId";

        private const string _KznAccountNumberKey = "Kzn.AccountNumber";
        private const string _KznEnabledKey = "Kzn.Enabled";

        private const string _SpbAccountNumberKey = "Spb.AccountNumber";
        private const string _SpbEnabledKey = "Spb.Enabled";

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
                    new Kzn.RetrieverService(),
                    new Kzn.SendService(),
                    ConfigUtils.GetBoolFromConfig(_KznEnabledKey),
                    ConfigUtils.GetStringFromConfig(_KznAccountNumberKey)
                ),
                new DataProvider(
                    arg.Kernel.Get<ISessionFactory>(),
                    arg.Kernel.Get<INotifier>(),
                    new Spb.RetrieverService(),
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
