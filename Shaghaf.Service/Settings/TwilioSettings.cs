using System;

namespace Shaghaf.Service.Settings
{
    public class TwilioSettings
    {
        public string AccountSID { get; set; } // Twilio Account SID
        public string AuthToken { get; set; } // Twilio Auth Token
        public string TwilioPhoneNumber { get; set; } // Twilio phone number used to send SMS
    }
}
