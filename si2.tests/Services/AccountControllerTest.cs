using Moq;
using NUnit.Framework;
using si2.bll.Services;
using si2.tests.Dtos;
using System.Threading.Tasks;

namespace si2.tests.Services
{
    [TestFixture]
    public class AccountControllerTest
    {
        private Mock<IEmailSender> _mockService;
        private IEmailSender _emailSender;
        private string _email;
        private string _subject;
        private string _message;


        // Used if test is being performed from the UnitTestAccountController class
        public AccountControllerTest(string email, string subject, string message)
        {
            _email = email;
            _subject = subject;
            _message = message;
            this._mockService = new Mock<IEmailSender>();
            this._emailSender = new EmailSender();
        }

        private SendEmailDto mockSendEmailDto = new SendEmailDto()
        {
            Email = "john.doe@usj.edu.lb",
            Subject = "SI_Prototype - Register",
            Message = "Please click on this link to confirm your email and reset your password"
        };


        // Used if test is being performed directly from this class
        [SetUp]
        public void AccountControllerTestSetup()
        {
            this._mockService = new Mock<IEmailSender>();
            this._emailSender = new EmailSender();
            // _email = "jane.doe@usj.edu.lb";
            //_subject = "SI_Prototype - Register process";
            //_message = "Please use this link to confirm your email and reset your password";
        }


        [Test]
        public void SendEmailAsync_WhenMatchingEmail()
        {
            // Arrange
            this._mockService.Setup(_mockService => _mockService.SendEmailAsync(_email, _subject, _message)).Returns(Task.FromResult(mockSendEmailDto));


            // Act
            var expected = this._emailSender.SendEmailAsync(mockSendEmailDto.Email, mockSendEmailDto.Subject, mockSendEmailDto.Message);

            // Assert
            Assert.AreEqual(mockSendEmailDto.Email, _email, "Mock email and provided email are equal");
            Assert.IsTrue(mockSendEmailDto.Email.Equals(_email), "Mock email value is true to provided email value");
        }


        [Test]
        public void SendEmailAsync_WhenMatchingSubject()
        {
            // Arrange
            this._mockService.Setup(_mockService => _mockService.SendEmailAsync(_email, _subject, _message)).Returns(Task.FromResult(mockSendEmailDto));

            // Act
            var expected = this._emailSender.SendEmailAsync(mockSendEmailDto.Email, mockSendEmailDto.Subject, mockSendEmailDto.Message);

            // Assert
            Assert.AreEqual(mockSendEmailDto.Subject, _subject, "Mock email subject and provided subject are equal");
            Assert.IsTrue(mockSendEmailDto.Subject.Equals(_subject), "Mock email subject value is true to provided subject value");
        }


        [Test]
        public void SendEmailAsync_WhenMatchingMessage()
        {
            // Arrange
            this._mockService.Setup(_mockService => _mockService.SendEmailAsync(_email, _subject, _message)).Returns(Task.FromResult(mockSendEmailDto));

            // Act
            var expected = this._emailSender.SendEmailAsync(mockSendEmailDto.Email, mockSendEmailDto.Subject, mockSendEmailDto.Message);

            // Assert
            Assert.AreEqual(mockSendEmailDto.Message, _message, "Mock email message and provided message are equal");
            Assert.IsTrue(mockSendEmailDto.Message.Equals(_message), "Mock email message value is true to provided message value");
        }


        [Test]
        public void SendEmailAsync_WhenNotMatchingEmail()
        {
            // Arrange
            this._mockService.Setup(_mockService => _mockService.SendEmailAsync(_email, _subject, _message)).Returns(Task.FromResult(mockSendEmailDto));


            // Act
            var expected = this._emailSender.SendEmailAsync(mockSendEmailDto.Email, mockSendEmailDto.Subject, mockSendEmailDto.Message);

            // Assert
            Assert.AreNotEqual(mockSendEmailDto.Email, _email, "Mock email is not equal to provided email ");
            Assert.IsFalse(mockSendEmailDto.Email.Equals(_email), "Mock email value is false to provided email value");
        }



        [Test]
        public void SendEmailAsync_WhenNotMatchingSubject()
        {
            // Arrange
            this._mockService.Setup(_mockService => _mockService.SendEmailAsync(_email, _subject, _message)).Returns(Task.FromResult(mockSendEmailDto));


            // Act
            var expected = this._emailSender.SendEmailAsync(mockSendEmailDto.Email, mockSendEmailDto.Subject, mockSendEmailDto.Message);

            // Assert
            Assert.AreNotEqual(mockSendEmailDto.Subject, _subject, "Mock email subject is not equal to provided email subject");
            Assert.IsFalse(mockSendEmailDto.Subject.Equals(_subject), "Mock email subject value is false to provided email subject value");
        }


        [Test]
        public void SendEmailAsync_WhenNotMatchingMessage()
        {
            // Arrange
            this._mockService.Setup(_mockService => _mockService.SendEmailAsync(_email, _subject, _message)).Returns(Task.FromResult(mockSendEmailDto));


            // Act
            var expected = this._emailSender.SendEmailAsync(mockSendEmailDto.Email, mockSendEmailDto.Subject, mockSendEmailDto.Message);

            // Assert
            Assert.AreNotEqual(mockSendEmailDto.Message, _message, "Mock email message is not equal to provided email message");
            Assert.IsFalse(mockSendEmailDto.Message.Equals(_message), "Mock email message value is false to provided email message value");
        }


        [Test]
        public void SendEmailAsync_WhenNotNull()
        {
            // Arrange
            this._mockService.Setup(_mockService => _mockService.SendEmailAsync(_email, _subject, _message)).Returns(Task.FromResult(mockSendEmailDto));


            // Act
            var expected = this._emailSender.SendEmailAsync(mockSendEmailDto.Email, mockSendEmailDto.Subject, mockSendEmailDto.Message);

            // Assert
            Assert.IsNotNull(_email, "Mock email cannot be null");
            Assert.IsNotNull(_subject, "Mock email subject cannot be null");
            Assert.IsNotNull(_message, "Mock email message cannot be null");
        }
    }
}
