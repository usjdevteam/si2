using NUnit.Framework;
using si2.bll.Helpers.Credits;
using System;

namespace si2.tests.Helpers
{
    [TestFixture]
    class CreditsTest
    {
        [Test]
        public void CreditsCreation()
        {
            Credits c = new Credits(5);
            Decimal r = new Decimal(5);
            Assert.AreEqual(r, c.Value);
            Assert.Throws<NegativeCreditsException>(delegate {
                new Credits(-1);
            });
            c = new Credits(0);
            r = new Decimal(0);
            Assert.AreEqual(r, c.Value);
        }

        [Test]
        public void CreditsAddOperator()
        {
            Credits a = new Credits(5);
            Credits b = new Credits(7);
            Credits c = a + b;
            Decimal r = new Decimal(12);
            Assert.AreEqual(r, c.Value);
        }

        [Test]
        public void CreditsSubstractOperator()
        {
            Credits a = new Credits(5);
            Credits b = new Credits(7);
            Credits c = b-a;
            Decimal r = new Decimal(2);
            Assert.AreEqual(r, c.Value);
            Assert.Throws<NegativeCreditsException>(delegate {
                Credits d = a - b;
            });
        }

        [Test]
        public void CreditsDisplay()
        {
            Credits a = new Credits(12.5F);
            Assert.AreEqual("12.5 Cr.", a.ToString());
        }
    }
}
