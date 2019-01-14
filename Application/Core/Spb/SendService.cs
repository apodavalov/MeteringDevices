using System;
using System.Collections.Generic;
using System.Linq;

namespace MeteringDevices.Core.Spb
{
    class SendService : ISendService
    {
        private readonly IMessageSender _Sender;

        public SendService(IMessageSender sender)
        {
            if (sender == null)
            {
                throw new ArgumentNullException(nameof(sender));
            }

            _Sender = sender;
        }

        public void PutValues(string accountNumber, IReadOnlyDictionary<string, int> values)
        {
            string message = 
                string.Concat(
                    "Показания приборов учета:\n\t",
                    string.Join("\n\t", values.Select(kv => string.Concat(kv.Key, ": ", kv.Value)))
                );

            _Sender.Send(message);
        }
    }
}
