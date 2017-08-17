using Moq;
using NUnit.Framework;
using System;
using System.Linq;

namespace MeteringDevices.Core.Test
{
    public class AppTest
    {
        [Test]
        public void RunTest()
        {
            //given
            Mock<IDataProvider>[] mockDataProviders = 
                Enumerable.Repeat<Func<Mock<IDataProvider>>>(() => new Mock<IDataProvider>(), 3).Select(f => f()).ToArray();

            foreach (Mock<IDataProvider> mockDataProvider in mockDataProviders)
            {
                mockDataProvider.Setup(mock => mock.Provide()).Verifiable();
            }

            //when
            App app = new App(mockDataProviders.Select(mockDataProvider => mockDataProvider.Object).ToArray());
            app.Run();

            //then
            foreach (Mock<IDataProvider> mockDataProvider in mockDataProviders)
            {
                mockDataProvider.Verify(mock => mock.Provide());
            }
        }
    }
}
