using MeteringDevices.Core.Notification;
using MeteringDevices.Data;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MeteringDevices.Core.Test
{
    public class DataProviderTest
    {
        private Mock<ISessionFactory> _SessionFactoryMock;
        private Mock<ISession> _SessionMock;
        private Mock<INotifier> _NotifierMock;
        private Mock<IRetrieveService> _RetrieveServiceMock;
        private Mock<ISendService> _SendServiceMock;
        private Mock<ICurrentDateTimeProvider> _CurrentDateTimeProvider;

        [SetUp]
        public void SetUp()
        {
            _SessionFactoryMock = new Mock<ISessionFactory>();
            _SessionMock = new Mock<ISession>();
            _NotifierMock = new Mock<INotifier>();
            _RetrieveServiceMock = new Mock<IRetrieveService>();
            _SendServiceMock = new Mock<ISendService>();
            _CurrentDateTimeProvider = new Mock<ICurrentDateTimeProvider>();
        }

        [Test]
        public void ProvideTest()
        {
            //given
            IDictionary<string, int> values = new Dictionary<string, int> { { "key1", 5 }, { "key2", 6 } };
            string accountNumber = "some-account-name";
            string notificationMessage = "Submitted metering devices values for account 'some-account-name'.\n" + 
                "The following values have been sent:\n\tkey1: 5\n\tkey2: 6";
            int dayOfMonthToSend = 9;
            SetupMocks(values, accountNumber, notificationMessage, dayOfMonthToSend);

            DataProvider provider = new DataProvider(_SessionFactoryMock.Object, _NotifierMock.Object,
                _RetrieveServiceMock.Object, _SendServiceMock.Object, _CurrentDateTimeProvider.Object, 
                true, accountNumber, dayOfMonthToSend);

            //when
            provider.Provide();

            //then
            _SendServiceMock.Verify(sendService => 
                sendService.PutValues(accountNumber, new ReadOnlyDictionary<string, int>(values)));
            _NotifierMock.Verify(notifier => notifier.Notify(notificationMessage));
        }

        [Test]
        public void NullValuesTest()
        {
            //given
            string accountNumber = "some-account-name";
            int dayOfMonthToSend = 9;
            SetupMocks(null, accountNumber, It.IsAny<string>(), dayOfMonthToSend);

            DataProvider provider = new DataProvider(_SessionFactoryMock.Object, _NotifierMock.Object,
                _RetrieveServiceMock.Object, _SendServiceMock.Object, _CurrentDateTimeProvider.Object, 
                true, accountNumber, dayOfMonthToSend);

            //when
            provider.Provide();

            //then
            _SendServiceMock.Verify(
                sendService => sendService.PutValues(
                    It.IsAny<string>(), 
                    It.IsAny<IReadOnlyDictionary<string,int>>()), Times.Never);
            _NotifierMock.Verify(notifier => notifier.Notify(It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void DisableTest()
        {
            //given
            IDictionary<string, int> values = new Dictionary<string, int> { { "key3", 8 }, { "key4", 9 } };
            int dayOfMonthToSend = 9;
            string accountNumber = "some-account-name";
            SetupMocks(values, accountNumber, It.IsAny<string>(), dayOfMonthToSend);

            DataProvider provider = new DataProvider(_SessionFactoryMock.Object, _NotifierMock.Object,
                _RetrieveServiceMock.Object, _SendServiceMock.Object, _CurrentDateTimeProvider.Object, 
                false, accountNumber, dayOfMonthToSend);

            //when
            provider.Provide();

            //then
            _SessionFactoryMock.Verify(sessionFactory => sessionFactory.OpenSession(), Times.Never);
            _RetrieveServiceMock.Verify(retrieveService =>
                retrieveService.GetCurrentValues(_SessionMock.Object), Times.Never);
            _SendServiceMock.Verify(sendService =>
                sendService.PutValues(It.IsAny<string>(), It.IsAny<IReadOnlyDictionary<string, int>>()), Times.Never);
            _NotifierMock.Verify(notifier => notifier.Notify(It.IsAny<string>()), Times.Never);
            _NotifierMock.Verify(notifier => notifier.Notify(It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void WrongDayTest()
        {
            //given
            IDictionary<string, int> values = new Dictionary<string, int> { { "key3", 8 }, { "key4", 9 } };
            string accountNumber = "some-account-name";
            SetupMocks(values, accountNumber, It.IsAny<string>(), 9);

            DataProvider provider = new DataProvider(_SessionFactoryMock.Object, _NotifierMock.Object,
                _RetrieveServiceMock.Object, _SendServiceMock.Object, _CurrentDateTimeProvider.Object,
                true, accountNumber, 8);

            //when
            provider.Provide();

            //then
            _SessionFactoryMock.Verify(sessionFactory => sessionFactory.OpenSession(), Times.Never);
            _RetrieveServiceMock.Verify(retrieveService =>
                retrieveService.GetCurrentValues(_SessionMock.Object), Times.Never);
            _SendServiceMock.Verify(sendService =>
                sendService.PutValues(It.IsAny<string>(), It.IsAny<IReadOnlyDictionary<string, int>>()), Times.Never);
            _NotifierMock.Verify(notifier => notifier.Notify(It.IsAny<string>()), Times.Never);
            _NotifierMock.Verify(notifier => notifier.Notify(It.IsAny<string>()), Times.Never);
        }

        private void SetupMocks(IDictionary<string, int> values, string accountNumber, 
            string notificationMessage, int dayOfMonthToSend)
        {
            _SessionFactoryMock.Setup(sessionFactory => sessionFactory.OpenSession())
                .Returns(_SessionMock.Object).Verifiable();

            _NotifierMock.Setup(notifier => notifier.Notify(notificationMessage)).Verifiable();

            _RetrieveServiceMock.Setup(retrieveService =>
                retrieveService.GetCurrentValues(_SessionMock.Object)).Returns(values);

            _SendServiceMock.Setup(sendService =>
                sendService.PutValues(accountNumber, It.IsAny<IReadOnlyDictionary<string, int>>())).Verifiable();

            _CurrentDateTimeProvider.Setup(currentDateTimeProvider => currentDateTimeProvider.GetUtcNow())
                .Returns(new DateTime(2018, 09, dayOfMonthToSend, 12, 0, 0, DateTimeKind.Utc));
        }
    }
}
