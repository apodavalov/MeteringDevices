using System;

namespace MeteringDevices.Core.Notification
{
    class Notifier : INotifier
    {
        private readonly IMessageSender _Sender;

        public Notifier(IMessageSender sender)
        {
            if (sender == null)
            {
                throw new ArgumentNullException(nameof(sender));
            }

            _Sender = sender;
        }

        public void Notify(string message)
        {
            _Sender.Send(message);
        }
    }
}
