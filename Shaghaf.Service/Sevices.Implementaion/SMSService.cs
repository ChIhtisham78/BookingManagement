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

        public MessageResource Send(string mobileNumber, string body)
        {
            TwilioClient.Init(_twilio.AccountSID, _twilio.AuthToken);

            // Format the phone number to E.164 format before sending
            var formattedNumber = FormatPhoneNumber(mobileNumber);

            var result = MessageResource.Create(
                    body: body,
                    from: new Twilio.Types.PhoneNumber(_twilio.TwilioPhoneNumber),
                    to: new Twilio.Types.PhoneNumber(formattedNumber)
                );

            return result;
        }

        private string FormatPhoneNumber(string phoneNumber)
        {
            if (phoneNumber.StartsWith("0"))
            {
                phoneNumber = phoneNumber.Substring(1);
            }

            if (!phoneNumber.StartsWith("+"))
            {
                phoneNumber = "+2" + phoneNumber; // Assuming +2 is the country code for Egypt
            }

            return phoneNumber;
        }
    }
}
