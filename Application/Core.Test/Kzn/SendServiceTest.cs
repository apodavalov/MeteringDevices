using MeteringDevices.Core.Common;
using MeteringDevices.Core.Kzn;
using MeteringDevices.Core.Kzn.Dto;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MeteringDevices.Core.Test.Kzn
{
    public class SendServiceTest
    {
        private Mock<ISendApiService> sendApiServiceMock;

        [SetUp]
        public void SetUp()
        {
            sendApiServiceMock = new Mock<ISendApiService>();
        }

        [Test]
        public void PutValuesNoFlatModelTest()
        {
            //given
            const string username = "test-user-name";
            const string password = "test-user-passord";
            const string accountNumber = "account-number";
            const string firstDeviceUniqueId = "key-1";
            const string secondDeviceUniqueId = "key-2";

            IList<FlatModelDto> flatModelDtoList = CreateFlatModelDtoList(
                CreateFlatModelDto("other-account-number", "other-flat-number", "other-holder-surname")
            );

            SetupMocks(username, password, flatModelDtoList);

            SendService sendService = new SendService(sendApiServiceMock.Object, username, password);

            IDictionary<string, int> dictionary = new Dictionary<string, int>();
            dictionary.Add(firstDeviceUniqueId, 1);
            dictionary.Add(secondDeviceUniqueId, 2);

            //when  
            Assert.Throws<InvalidOperationException>(() => sendService.PutValues(accountNumber, dictionary.AsReadOnly()));
        }

        [Test]
        public void PutValuesNoInputAllowedTest()
        {
            //given
            const string username = "test-user-name";
            const string password = "test-user-passord";
            const string accountNumber = "account-number";
            const string firstDeviceUniqueId = "key-1";
            const string secondDeviceUniqueId = "key-2";

            const string flatNumber = "flat-number";
            const string houseHolderSurname = "house-holder-surname";

            FlatModelDto flatModelDto = CreateFlatModelDto(accountNumber, flatNumber, houseHolderSurname);
            IList<FlatModelDto> flatModelDtoList = CreateFlatModelDtoList(flatModelDto);

            DeviceInfoResponseDto deviceInfoResponseDto = CreateDeviceInfoResponseDto(0, false, firstDeviceUniqueId, secondDeviceUniqueId);
            SetValuesResponseDto setValuesResponseDto = CreateSetValuesResponseDto(0);
            IReadOnlyDictionary<string, int> actualValues =
                SetupMocks(username, password, flatModelDtoList, flatModelDto, deviceInfoResponseDto, setValuesResponseDto);

            SendService sendService = new SendService(sendApiServiceMock.Object, username, password);

            IDictionary<string, int> dictionary = new Dictionary<string, int>();
            dictionary.Add(firstDeviceUniqueId, 1);
            dictionary.Add(secondDeviceUniqueId, 2);

            //when  
            Assert.Throws<InvalidOperationException>(() => sendService.PutValues(accountNumber, dictionary.AsReadOnly()));
        }

        [Test]
        public void PutValuesNumberOfDevicesAreDifferentTest()
        {
            //given
            const string username = "test-user-name";
            const string password = "test-user-passord";
            const string accountNumber = "account-number";
            const string firstDeviceUniqueId = "key-1";
            const string secondDeviceUniqueId = "key-2";

            const string flatNumber = "flat-number";
            const string houseHolderSurname = "house-holder-surname";

            FlatModelDto flatModelDto = CreateFlatModelDto(accountNumber, flatNumber, houseHolderSurname);
            IList<FlatModelDto> flatModelDtoList = CreateFlatModelDtoList(flatModelDto);
            DeviceInfoResponseDto deviceInfoResponseDto = CreateDeviceInfoResponseDto(0, true, firstDeviceUniqueId);

            SetupMocks(username, password, flatModelDtoList, flatModelDto, deviceInfoResponseDto);

            SendService sendService = new SendService(sendApiServiceMock.Object, username, password);

            IDictionary<string, int> dictionary = new Dictionary<string, int>();
            dictionary.Add(firstDeviceUniqueId, 1);
            dictionary.Add(secondDeviceUniqueId, 2);

            //when  
            Assert.Throws<InvalidOperationException>(() => sendService.PutValues(accountNumber, dictionary.AsReadOnly()));
        }

        [Test]
        public void PutValuesSendApiServicePutValuesErrorTest()
        {
            //given
            const string username = "test-user-name";
            const string password = "test-user-passord";
            const string accountNumber = "account-number";
            const string firstDeviceUniqueId = "key-1";
            const string secondDeviceUniqueId = "key-2";

            const string flatNumber = "flat-number";
            const string houseHolderSurname = "house-holder-surname";

            FlatModelDto flatModelDto = CreateFlatModelDto(accountNumber, flatNumber, houseHolderSurname);
            IList<FlatModelDto> flatModelDtoList = CreateFlatModelDtoList(flatModelDto);
            DeviceInfoResponseDto deviceInfoResponseDto = CreateDeviceInfoResponseDto(0, true, firstDeviceUniqueId, secondDeviceUniqueId);
            SetValuesResponseDto setValuesResponseDto = CreateSetValuesResponseDto(1);
            IReadOnlyDictionary<string, int> actualValues =
                SetupMocks(username, password, flatModelDtoList, flatModelDto, deviceInfoResponseDto, setValuesResponseDto);

            SendService sendService = new SendService(sendApiServiceMock.Object, username, password);

            IDictionary<string, int> dictionary = new Dictionary<string, int>();
            dictionary.Add(firstDeviceUniqueId, 1);
            dictionary.Add(secondDeviceUniqueId, 2);

            //when  
            Assert.Throws<InvalidOperationException>(() => sendService.PutValues(accountNumber, dictionary.AsReadOnly()));
        }

        [Test]
        public void PutValuesTest()
        {
            //given
            const string username = "test-user-name";
            const string password = "test-user-passord";
            const string accountNumber = "account-number";
            const string firstDeviceUniqueId = "key-1";
            const string secondDeviceUniqueId = "key-2";

            const string flatNumber = "flat-number";
            const string houseHolderSurname = "house-holder-surname";

            FlatModelDto flatModelDto = CreateFlatModelDto(accountNumber, flatNumber, houseHolderSurname);
            IList<FlatModelDto> flatModelDtoList = CreateFlatModelDtoList(
                flatModelDto,
                CreateFlatModelDto("other-account-number", "other-flat-number", "other-holder-surname")
            );
            DeviceInfoResponseDto deviceInfoResponseDto = CreateDeviceInfoResponseDto(0, true, firstDeviceUniqueId, secondDeviceUniqueId);
            SetValuesResponseDto setValuesResponseDto = CreateSetValuesResponseDto(0);
            IReadOnlyDictionary<string, int> actualValues = 
                SetupMocks(username, password, flatModelDtoList, flatModelDto, deviceInfoResponseDto, setValuesResponseDto);

            SendService sendService = new SendService(sendApiServiceMock.Object, username, password);

            IDictionary<string, int> dictionary = new Dictionary<string, int>();
            dictionary.Add(firstDeviceUniqueId, 1);
            dictionary.Add(secondDeviceUniqueId, 2);

            //when  
            sendService.PutValues(accountNumber, dictionary.AsReadOnly());

            //then
            CollectionAssert.AreEquivalent(dictionary, actualValues);
        }

        private static SetValuesResponseDto CreateSetValuesResponseDto(int code)
        {
            return new SetValuesResponseDto
            {
                Result = CreateResponseDto(code)
            };
        }

        private static ResponseResultDto CreateResponseDto(int code)
        {
            return new ResponseResultDto
            {
                Code = code
            };
        }

        private static IList<FlatModelDto> CreateFlatModelDtoList(params FlatModelDto[] flatModelDtoArray)
        {
            return flatModelDtoArray.ToList();
        }

        private static DeviceInfoResponseDto CreateDeviceInfoResponseDto(int code, bool inputAllowed, params string[] deviceUniqueIds)
        {
            return new DeviceInfoResponseDto
            {
                Result = CreateResponseDto(code),
                Settings = new DeviceSettingsDto
                {
                    InputAllowed = inputAllowed
                },
                Counters = new DeviceCountersDto
                {
                    Devices = deviceUniqueIds.Select(deviceUniqueId =>
                        new DeviceInfoDto
                        {
                            UniqueId = deviceUniqueId
                        }
                    ).ToList()
                }
            };
        }

        private static FlatModelDto CreateFlatModelDto(string accountNumber, string flatNumber, string houseHolderSurname)
        {
            return new FlatModelDto { AccountNumber = accountNumber, FlatNumber = flatNumber, HouseHolderSurname = houseHolderSurname };
        }

        private IReadOnlyDictionary<string, int> SetupMocks(string username, string password, IList<FlatModelDto> flatModelDtoList, 
            FlatModelDto flatModelDto, DeviceInfoResponseDto deviceInfoResponseDto, SetValuesResponseDto setValuesResponseDto)
        {
            SecurityToken securityToken = SetupMocks(username, password, flatModelDtoList, flatModelDto, deviceInfoResponseDto);

            IDictionary<string, int> values = new Dictionary<string, int>();

            sendApiServiceMock.Setup(sendApiService => sendApiService.PutValues(securityToken, flatModelDto, It.IsAny<IList<DeviceInfoDto>>()))
                .Returns<SecurityToken, FlatModelDto, IList<DeviceInfoDto>>(
                    (paramSecurityToken, paramFlatModelDto, paramDeviceInfoDtoList) =>
                        MockPutValues(paramSecurityToken, paramFlatModelDto, paramDeviceInfoDtoList, setValuesResponseDto, values)
                );

            return values.AsReadOnly();
        }

        private SecurityToken SetupMocks(string username, string password, IList<FlatModelDto> flatModelDtoList, FlatModelDto flatModelDto, DeviceInfoResponseDto deviceInfoResponseDto)
        {
            SecurityToken securityToken = SetupMocks(username, password, flatModelDtoList);

            sendApiServiceMock.Setup(sendApiService => sendApiService.GetDevicesInfo(securityToken, flatModelDto))
                .Returns(deviceInfoResponseDto);

            return securityToken;
        }

        private SecurityToken SetupMocks(string username, string password, IList<FlatModelDto> flatModelDtoList)
        {
            SecurityToken securityToken = new SecurityToken(username, "session-token");
            sendApiServiceMock.Setup(sendApiService => sendApiService.Login(username, password)).Returns(securityToken);
            sendApiServiceMock.Setup(sendApiService => sendApiService.GetFlatInfo(securityToken)).Returns(flatModelDtoList);
            return securityToken;
        }

        private static SetValuesResponseDto MockPutValues(SecurityToken securityToken, FlatModelDto flatModelDto, 
            IList<DeviceInfoDto> deviceInfoDtoList, SetValuesResponseDto setValuesResponseDto, IDictionary<string, int> dictionaryToStoreValues)
        {
            foreach (DeviceInfoDto deviceInfoDto in deviceInfoDtoList)
            {
                dictionaryToStoreValues.Add(deviceInfoDto.UniqueId, deviceInfoDto.Value);
            }

            return setValuesResponseDto;
        }
    }
}
