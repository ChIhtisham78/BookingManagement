// SMSService.cs
using Microsoft.Extensions.Options;
using Shaghaf.Core.Services.Contract;
using Shaghaf.Service.Settings;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Shaghaf.Service.Sevices.Implementaion
{
    public class SMSService : ISMSService
    {
        private readonly TwilioSettings _twilio;

        public SMSService(IOptions<TwilioSettings> twilio)
        {
            _twilio = twilio.Value;
        }

        public MessageResource Send(string mobileNumber, string body,string countryCode)
        {
            TwilioClient.Init(_twilio.AccountSID, _twilio.AuthToken);

            // Format the phone number to E.164 format before sending
            var formattedNumber = FormatPhoneNumber(mobileNumber, countryCode);

       
            var result = MessageResource.Create(
                    body: body,
                    from: new Twilio.Types.PhoneNumber(_twilio.TwilioPhoneNumber),
                    to: new Twilio.Types.PhoneNumber(formattedNumber)
                );

            return result;
        }

        private static readonly Dictionary<string, string> CountryCodeMap = new()
        {
                { "EG", "+20" }, 
                { "PK", "+92" }, 
                { "IT", "+39" },
                { "US", "+1" }, 
                { "IN", "+91" }
        };


        private string FormatPhoneNumber(string phoneNumber, string countryCode)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentException("Phone number cannot be empty.");

            if (!CountryCodeMap.TryGetValue(countryCode.ToUpper(), out var internationalPrefix))
                throw new ArgumentException($"Unsupported country code: {countryCode}");

            phoneNumber = phoneNumber.Trim();

            if (phoneNumber.StartsWith("0"))
                phoneNumber = phoneNumber.Substring(1);

            if (!phoneNumber.StartsWith("+"))
                phoneNumber = internationalPrefix + phoneNumber;

            return phoneNumber;
        }

    }
}
