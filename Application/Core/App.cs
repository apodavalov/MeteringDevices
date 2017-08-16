using System;
using System.Collections.Generic;
using System.Linq;

namespace MeteringDevices.Core
{
    class App : IApp
    {
        private readonly IReadOnlyList<IDataProvider> _DataProviders;

        public App(params IDataProvider[] dataProviders)
        {
            if (!dataProviders.All(dataProvider => dataProvider != null))
            {
                throw new ArgumentNullException(nameof(dataProviders));
            }

            _DataProviders = dataProviders.ToArray();
        }

        public void Run()
        {
            foreach (IDataProvider dataProvider in _DataProviders)
            {
                dataProvider.Provide();
            }
        }
    }
}
