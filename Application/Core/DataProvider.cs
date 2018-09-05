using MeteringDevices.Core.Notification;
using MeteringDevices.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MeteringDevices.Core
{
    class DataProvider : IDataProvider
    {
        private readonly IRetrieveService _RetrieveService;
        private readonly ISendService _SendService;
        private readonly INotifier _Notifier;
        private readonly ISessionFactory _SessionFactory;
        private readonly ICurrentDateTimeProvider _CurrentDateTimeProvider;
        private readonly bool _Enabled;
        private readonly string _AccountNumber;
        private readonly int _DayOfMonthToSend;

        public DataProvider(ISessionFactory sessionFactory, INotifier notifier, IRetrieveService retrieveService, 
            ISendService sendService, ICurrentDateTimeProvider currentDateTimeProvider, bool enabled, string accountNumber, int dayOfMonthToSend)
        {
            if (sessionFactory == null)
            {
                throw new ArgumentNullException(nameof(sessionFactory));
            }

            if (notifier == null)
            {
                throw new ArgumentNullException(nameof(notifier));
            }

            if (retrieveService == null)
            {
                throw new ArgumentNullException(nameof(retrieveService));
            }

            if (sendService == null)
            {
                throw new ArgumentNullException(nameof(sendService));
            }

            if (currentDateTimeProvider == null)
            {
                throw new ArgumentNullException(nameof(currentDateTimeProvider));
            }

            if (accountNumber == null)
            {
                throw new ArgumentNullException(nameof(accountNumber));
            }

            _Enabled = enabled;
            _AccountNumber = accountNumber;
            _RetrieveService = retrieveService;
            _Notifier = notifier;
            _SendService = sendService;
            _CurrentDateTimeProvider = currentDateTimeProvider;
            _SessionFactory = sessionFactory;
            _DayOfMonthToSend = dayOfMonthToSend;
        }

        public void Provide()
        {
            if (!_Enabled || _CurrentDateTimeProvider.GetUtcNow().Day != _DayOfMonthToSend)
            {
                return;
            }

            try
            {
                IDictionary<string, int> values = Retrieve();

                if (values == null)
                {
                    return;
                }

                _SendService.PutValues(_AccountNumber, new ReadOnlyDictionary<string, int>(values));
                _Notifier.Notify(BuildMessage(values));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        private string BuildMessage(IDictionary<string, int> values)
        {
            StringBuilder messageBuilder = new StringBuilder();

            messageBuilder.AppendFormat("Submitted metering devices values for account '{0}'.", _AccountNumber);

            if (values.Count > 0)
            {
                messageBuilder.Append("\nThe following values have been sent:");
                foreach (KeyValuePair<string, int> keyValue in values)
                {
                    messageBuilder.AppendFormat("\n\t{0}: {1}", keyValue.Key, keyValue.Value);
                }
            }

            return messageBuilder.ToString();
        }

        private IDictionary<string, int> Retrieve()
        {
            using (ISession session = _SessionFactory.OpenSession())
            {
                return _RetrieveService.GetCurrentValues(session);
            }
        }
    }
}
