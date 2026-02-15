using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Integrations
{
    public interface IEmailClient
    {
        Task SendAsync(string to, string subject, string body);
    }
}
