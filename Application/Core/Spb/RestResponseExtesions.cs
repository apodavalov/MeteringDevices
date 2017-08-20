using MeteringDevices.Core.Spb.Dto;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MeteringDevices.Core.Spb
{
    static class RestResponseExtesions
    {
        public static void CheckSuccess<T>(this IRestResponse<ResponseDto<T>> restResponse) where T : class
        {
            if (restResponse == null)
            {
                throw new ArgumentNullException(nameof(restResponse));
            }

            if (restResponse.ErrorException != null)
            {
                throw restResponse.ErrorException;
            }

            if (restResponse.Data == null)
            {
                throw new InvalidOperationException("No data was read.");
            }

            if (restResponse.Data.Status != 200)
            {
                throw new InvalidOperationException(
                    string.Format("Status code {0} is not equal to 200.", restResponse.Data.Status)
                    );
            }

            IReadOnlyList<MessageDto> messageDtoList = restResponse.Data.Messages;

            if (messageDtoList == null || messageDtoList.Count == 0)
            {
                return;
            }

            if (messageDtoList.Count != 1)
            {
                throw new InvalidOperationException(
                    string.Format("The collection '{0}' is empty or contains multiple items.", nameof(messageDtoList))
                    );
            }

            MessageDto messageDto = messageDtoList.Single();

            if (messageDto == null)
            {
                throw new InvalidOperationException(
                    string.Format("Message '{0}' is empty.", nameof(messageDto))
                    );
            }

            if (messageDto.Type != "success")
            {
                throw new InvalidOperationException(
                    string.Format("Operation is not successfully completed: {0}.", messageDto.Type)
                    );
            }
        }
    }
}
