using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeteringDevices.Core.Spb
{
    interface ISendApiServiceFactory
    {
        ISendApiService GetService(Uri baseUri);
    }
}
