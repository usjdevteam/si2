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


        private readonly SendEmailDto mockSendEmailDto = new SendEmailDto()
        {
            Email = "john.doe@usj.edu.lb",
            Subject = "SI_Prototype - Register",
            Message = "Please click on this link to confirm your email and reset your password"
        };


        [SetUp]
        public void AccountControllerTestSetup()
        {
            _mockService = new Mock<IEmailSender>();
            _emailSender = new EmailSender();
            _email = "jane.doe@usj.edu.lb";
            _subject = "SI_Prototype - Register process";
            _message = "Please use this link to confirm your email and reset your password";
        }


        [Test]
        public void SendEmailAsync_WhenMatchingEmail()
        {
            // Arrange
            //_email = mockSendEmailDto.Email;
            _mockService.Setup(_mockService => _mockService.SendEmailAsync(_email, _subject, _message)).Returns(Task.FromResult(mockSendEmailDto));


            // Act
            var expected = _emailSender.SendEmailAsync(mockSendEmailDto.Email, mockSendEmailDto.Subject, mockSendEmailDto.Message);


            // Assert
            Assert.AreEqual(mockSendEmailDto.Email, _email, "Mock email and provided email are not equal");
            //Assert.IsTrue(mockSendEmailDto.Email.Equals(_email), "Mock email value and provided email value are not true");
        }


        [Test]
        public void SendEmailAsync_WhenMatchingSubject()
        {
            // Arrange
            //_subject = mockSendEmailDto.Subject;
            _mockService.Setup(_mockService => _mockService.SendEmailAsync(_email, _subject, _message)).Returns(Task.FromResult(mockSendEmailDto));

            // Act
            var expected = _emailSender.SendEmailAsync(mockSendEmailDto.Email, mockSendEmailDto.Subject, mockSendEmailDto.Message);

            // Assert
            Assert.AreEqual(mockSendEmailDto.Subject, _subject, "Mock email subject and provided subject are not equal");
            //Assert.IsTrue(mockSendEmailDto.Subject.Equals(_subject), "Mock email subject value and provided subject value are not true");
        }


        [Test]
        public void SendEmailAsync_WhenMatchingMessage()
        {
            // Arrange
           //_message = mockSendEmailDto.Message;
            _mockService.Setup(_mockService => _mockService.SendEmailAsync(_email, _subject, _message)).Returns(Task.FromResult(mockSendEmailDto));

            // Act
            var expected = _emailSender.SendEmailAsync(mockSendEmailDto.Email, mockSendEmailDto.Subject, mockSendEmailDto.Message);

            // Assert
            Assert.AreEqual(mockSendEmailDto.Message, _message, "Mock email message and provided message are not equal");
            //Assert.IsTrue(mockSendEmailDto.Message.Equals(_message), "Mock email message value and provided message value are not true");
        }


        [Test]
        public void SendEmailAsync_WhenNotNull()
        {
            // Arrange
            this._mockService.Setup(_mockService => _mockService.SendEmailAsync(_email, _subject, _message)).Returns(Task.FromResult(mockSendEmailDto));


            // Act
            var expected = this._emailSender.SendEmailAsync(mockSendEmailDto.Email, mockSendEmailDto.Subject, mockSendEmailDto.Message);

            // Assert
            // _email = null;
            // _subject = null;
            // _message = null;
            Assert.IsNotNull(_email, "Mock email cannot be null");
            Assert.IsNotNull(_subject, "Mock email subject cannot be null");
            Assert.IsNotNull(_message, "Mock email message cannot be null");
        }
    }
}
