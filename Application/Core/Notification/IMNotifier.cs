using System;

namespace MeteringDevices.Core.Notification
{
    class IMNotifier : INotifier
    {
        private readonly IIMSender _Sender;

        public IMNotifier(IIMSender sender)
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
