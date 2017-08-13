using MeteringDevices.Data;
using Ninject;
using System;
using System.Collections.Generic;

namespace MeteringDevices.Service
{
    class Program
    {
        private const string _KznAccountNumberKey = "Kzn.AccountNumber";
        private const string _KznEnabledKey = "Kzn.Enabled";
        
        private const string _SpbAccountNumberKey = "Spb.AccountNumber";
        private const string _SpbEnabledKey = "Spb.Enabled";

        private const string _TelegramNotifierToken = "Telegram.Notifier.Token";
        private const string _TelegramNotifierChatId = "Telegram.Notifier.ChatId";


        public static IKernel Kernel { get; private set; }

        static void Main(string[] args)
        {
            Kernel = new StandardKernel();
            Kernel.Load(new Register());

            ProcessInfo[] processInfos = new[] 
            {
                new ProcessInfo(
                    new Kzn.RetrieverService(), 
                    new Kzn.SenderService(), 
                    ConfigUtils.GetBoolFromConfig(_KznEnabledKey),
                    ConfigUtils.GetStringFromConfig(_KznAccountNumberKey)
                ),
                new ProcessInfo(
                    new Spb.RetrieverService(), 
                    new Spb.SenderService(), 
                    ConfigUtils.GetBoolFromConfig(_SpbEnabledKey),
                    ConfigUtils.GetStringFromConfig(_SpbAccountNumberKey)
                )
            };

            TelegramNotifier notifier = new TelegramNotifier(ConfigUtils.GetStringFromConfig(_TelegramNotifierToken));

            foreach (ProcessInfo processInfo in processInfos)
            {
                if (!processInfo.Enabled)
                {
                    continue;
                }

                try
                {
                    IDictionary<string, int> values = Retrieve(processInfo.Retriever);

                    if (values == null)
                    {
                        continue;
                    }

                    processInfo.Sender.PutValues(values, processInfo.AccountNumber);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                }
            }
        }

        private static IDictionary<string, int> Retrieve(IRetrieveService retriever)
        {
            using (ISession session = Kernel.Get<ISession>())
            {
                return retriever.GetCurrentValues(session);
            }
        }
    }
}
