using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Contracts
{
    public interface INotificationService
    {
        Task SendAsync(string to, string subject, string body);
    }
}
