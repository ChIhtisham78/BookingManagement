using Twilio.Rest.Api.V2010.Account;

namespace Shaghaf.Core.Services.Contract
{
	public interface ISMSService
	{
		MessageResource Send(string mobileNumber, string body, string countryCode);
	}
}
