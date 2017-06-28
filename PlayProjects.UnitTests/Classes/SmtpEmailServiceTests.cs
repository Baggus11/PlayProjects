using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Common.Classes.Tests
{
    [TestClass()]
    public class SmtpEmailServiceTests
    {
        [TestMethod()]
        public void SendMailTest()
        {
            string username = "baggus11@gmail.com";
            string password = "DashIsBestPony17";
            string host = "smtp.gmail.com";


            SmtpEmailService svc = new SmtpEmailService(host, username, password);
            svc.SendEmail("michael.n.preston@gmail.com", "test", "hello from baggus");


        }
    }
}