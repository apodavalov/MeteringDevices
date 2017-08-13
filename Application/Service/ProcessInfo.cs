using System;

namespace MeteringDevices.Service
{
    class ProcessInfo
    {
        public IRetrieveService Retriever { get; private set; }

        public ISendService Sender { get;  private set; }

        public bool Enabled { get; private set; }

        public string AccountNumber { get; private set; }

        public ProcessInfo(IRetrieveService retriever, ISendService sender, bool enabled, string accountNumber)
        {
            if (retriever == null)
            {
                throw new ArgumentNullException(nameof(retriever));
            }

            if (sender == null)
            {
                throw new ArgumentNullException(nameof(sender));
            }

            if (accountNumber == null)
            {
                throw new ArgumentNullException(nameof(accountNumber));
            }

            Retriever = retriever;
            Sender = sender;
            Enabled = enabled;
            AccountNumber = accountNumber;
        }
    }
}
