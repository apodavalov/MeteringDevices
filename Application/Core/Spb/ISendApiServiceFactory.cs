using System;

namespace MeteringDevices.Core.Spb
{
    interface ISendApiServiceFactory
    {
        ISendApiService GetService(Uri baseUri);
    }
}
