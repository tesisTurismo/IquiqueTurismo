using System;
using System.Collections.Generic;
using System.Text;

namespace appTurismoIqq.Helpers
{
    using System;
    using System.Net.Mail;

    public static class RegexHelper
    {
        public static bool IsValidEmailAddress(string emailaddress)
        {
            try
            {
                var email = new MailAddress(emailaddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
