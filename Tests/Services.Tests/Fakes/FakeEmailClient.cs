using Data.Integrations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Tests.Fakes
{
    public class FakeEmailClient : IEmailClient
    {
        public List<(string To, string Subject, string Body)> SentEmails { get; } = new();

        public Task SendAsync(string to, string subject, string body)
        {
            SentEmails.Add((to, subject, body));
            return Task.CompletedTask;
        }
    }
}
