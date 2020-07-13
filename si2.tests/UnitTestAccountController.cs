using NUnit.Framework;
using si2.tests.Services;

namespace si2.tests
{
    public class UnitTestAccountController
    {
        private AccountControllerTest accountControllerTest;
        private string _email;
        private string _subject;
        private string _message;


        [SetUp]
        public void Setup()
        {
            _email = "john.doe@usj.edu.lb";
            _subject = "SI_Prototype - Register";
            _message = "Please click on this link to confirm your email and reset your password";
        }

        [Test]
        public void TestSendEmail_MatchingEmail()
        {
            accountControllerTest = new AccountControllerTest(_email, _subject, _message);
            this.accountControllerTest.SendEmailAsync_WhenMatchingEmail();
        }

        [Test]
        public void TestSendEmail_MatchingSubject()
        {
            accountControllerTest = new AccountControllerTest(_email, _subject, _message);
            this.accountControllerTest.SendEmailAsync_WhenMatchingSubject();
        }

        [Test]
        public void TestSendEmail_MatchingMessage()
        {
            accountControllerTest = new AccountControllerTest(_email, _subject, _message);
            this.accountControllerTest.SendEmailAsync_WhenMatchingMessage();
        }

        [Test]
        public void TestSendEmail_NotNull()
        {
            accountControllerTest = new AccountControllerTest(_email, _subject, _message);
            this.accountControllerTest.SendEmailAsync_WhenNotNull();
        }


        [Test]
        public void TestSendEmail_NotMatchingEmail()
        {
            accountControllerTest = new AccountControllerTest("jane.doe@usj.edu.lb", _subject, _message);
            this.accountControllerTest.SendEmailAsync_WhenNotMatchingEmail();
        }


        [Test]
        public void TestSendEmail_NotMatchingSubject()
        {
            accountControllerTest = new AccountControllerTest(_email, "SI Prototype - Registration Process", _message);
            this.accountControllerTest.SendEmailAsync_WhenNotMatchingSubject();
        }


        [Test]
        public void TestSendEmail_NotMatchingMessage()
        {
            accountControllerTest = new AccountControllerTest(_email, _subject, "Use the link below for the confirmation process");
            this.accountControllerTest.SendEmailAsync_WhenNotMatchingMessage();
        }
    }
}