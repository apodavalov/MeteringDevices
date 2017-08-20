using MeteringDevices.Core.Notification;
using MeteringDevices.Data;
using System;
using System.Collections.Generic;

namespace MeteringDevices.Core
{
    class DataProvider : IDataProvider
    {
        private readonly IRetrieveService _RetrieveService;
        private readonly ISendService _SendService;
        private readonly INotifier _Notifier;
        private readonly ISessionFactory _SessionFactory;
        private readonly bool _Enabled;
        private readonly string _AccountNumber;

        public DataProvider(ISessionFactory sessionFactory, INotifier notifier, IRetrieveService retrieveService, 
            ISendService sendService, bool enabled, string accountNumber)
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

            if (accountNumber == null)
            {
                throw new ArgumentNullException(nameof(accountNumber));
            }

            _Enabled = enabled;
            _AccountNumber = accountNumber;
            _RetrieveService = retrieveService;
            _Notifier = notifier;
            _SendService = sendService;
            _SessionFactory = sessionFactory;
        }

        public void Provide()
        {
            if (!_Enabled)
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

                _SendService.PutValues(_AccountNumber, values);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
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
