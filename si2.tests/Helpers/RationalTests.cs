using NUnit.Framework;
using si2.bll.Helpers.RationalNumbers;
using System;

namespace si2.tests.Helpers
{
    [TestFixture]
    class RationalTests
    {
        Random generator;

        public RationalTests()
        {
            generator = new Random();
        }

        [Test]
        public void RationalNumbersCreationTest()
        {
            Assert.Throws<InfiniteRationalNumber>(delegate
            {
                new Rational(-1, 0);
            });
            Assert.Throws<InfiniteRationalNumber>(delegate
            {
                new Rational(1, 0);
            });
            for (int i = 1; i < 50000; ++i)
            {
                int x = generator.Next() % 1000 -500;
                int y;
                do
                {
                    y = generator.Next() % 1000 - 500;
                }
                while (y == 0);
                int r;
                do
                {
                    r = generator.Next() % 1000 - 500;
                }
                while (r == 0);
                Rational a = new Rational(3 * r, 5 * r);
                Assert.AreEqual(3, Math.Abs(a.Numerator));
                Assert.AreEqual(5, Math.Abs(a.Denominator));
                Assert.AreEqual(15, a.Numerator * a.Denominator);
                a = new Rational(-3 * r, 5 * r);
                Assert.AreEqual(3, Math.Abs(a.Numerator));
                Assert.AreEqual(5, Math.Abs(a.Denominator));
                Assert.AreEqual(-15, a.Numerator * a.Denominator);
                a = new Rational(3 * r, -5 * r);
                Assert.AreEqual(3, Math.Abs(a.Numerator));
                Assert.AreEqual(5, Math.Abs(a.Denominator));
                Assert.AreEqual(-15, a.Numerator * a.Denominator);
                a = new Rational(-3 * r, -5 * r);
                Assert.AreEqual(3, Math.Abs(a.Numerator));
                Assert.AreEqual(5, Math.Abs(a.Denominator));
                Assert.AreEqual(15, a.Numerator * a.Denominator);
                Rational b = new Rational(x, y);
                Rational c = new Rational(x * r, y * r);
                Assert.AreEqual(b, c);
                Assert.AreEqual(b.Numerator*c.Denominator, c.Numerator*b.Denominator);
            }
        }

        Rational RandomNumber()
        {
            int x = generator.Next() % 1000 - 500;
            int y;
            do
            {
                y = generator.Next() % 1000 - 500;
            }
            while (y == 0);
            return new Rational(x, y);
        }

        [Test]
        public void AddingRationalNumbersTest()
        {
            for (int i = 0; i < 50000; ++i)
            {
                Rational x = RandomNumber();
                Rational y = RandomNumber();
                Rational z = x + y;
                Assert.AreEqual(z.Numerator * x.Denominator * y.Denominator, z.Denominator * (x.Numerator * y.Denominator + x.Denominator * y.Numerator));
            }
        }

        [Test]
        public void MultiplyingRationaNumbersTest()
        {
            for (int i = 0; i < 50000; ++i)
            {
                Rational x = RandomNumber();
                Rational y = RandomNumber();
                Rational z = x * y;
                Assert.AreEqual(z.Numerator * x.Denominator * y.Denominator, z.Denominator * x.Numerator*y.Numerator);
            }
        }

        [Test]
        public void RationalNumbersEqualityTest()
        {
            for(int i=0;i<50000;++i)
            {
                Rational a = RandomNumber();
                int r;
                do
                {
                    r = generator.Next() % 1000 - 500;
                }
                while (r == 0);
                Rational b = new Rational(a.Numerator * r, a.Denominator * r);
                Assert.AreEqual(true, a == b);
                Assert.AreEqual(true, a <= b);
                Assert.AreEqual(true, a >= b);
                Assert.AreEqual(true, b == a);
                Assert.AreEqual(true, b <= a);
                Assert.AreEqual(true, b >= a);
                Assert.AreEqual(true, a.Equals(b));
                Assert.AreEqual(true, b.Equals(a));
                Assert.AreEqual(false, a < b);
                Assert.AreEqual(false, a > b);
                Assert.AreEqual(false, a != b);
            }
        }

        [Test]
        public void RationalNumberOrdinalOperatorsTest()
        {
            for(int i = 0; i < 50000; ++i)
            {
                Rational a = RandomNumber();
                Rational b = RandomNumber();
                if (a.Numerator * b.Denominator < a.Denominator * b.Numerator)
                {
                    Assert.AreEqual(true, a < b);
                    Assert.AreEqual(true, b > a);
                    Assert.AreEqual(false, a > b);
                    Assert.AreEqual(false, b < a);
                    Assert.AreEqual(true, a <= b);
                    Assert.AreEqual(true, b >= a);
                    Assert.AreEqual(false, a == b);
                    Assert.AreEqual(true, a != b);
                    Assert.AreEqual(false, b == a);
                    Assert.AreEqual(true, b != a);
                }
                if (a.Numerator * b.Denominator > a.Denominator * b.Numerator)
                {
                    Assert.AreEqual(true, a > b);
                    Assert.AreEqual(true, b < a);
                    Assert.AreEqual(false, a < b);
                    Assert.AreEqual(false, b > a);
                    Assert.AreEqual(true, a >= b);
                    Assert.AreEqual(true, b <= a);
                    Assert.AreEqual(true, a != b);
                    Assert.AreEqual(false, b == a);
                    Assert.AreEqual(true, b != a);
                }
            }
        }

         [Test]
        public void RationalFloatConversionTest()
        {
            Rational a = new Rational(5.0F);
            Assert.AreEqual(5,a.Numerator);
            a = new Rational(1.234F);
            Rational b = new Rational(1234, 1000);
            Assert.AreEqual(a, b);
            Assert.AreEqual(b,a);
        } 
        
        [Test]
        public void RationalDoubleConversionTest()
        {
            Rational a = new Rational(5.0);
            Assert.AreEqual(5, a.Numerator);
            a = new Rational(1.234);
            Rational b = new Rational(1234, 1000);
            Assert.AreEqual(a, b);
            Assert.AreEqual(b, a);
        }
        [Test]
        public void RationalToStringTest()
        {
            Rational a = new Rational(5);
            Assert.AreEqual("5", a.ToString());
            a = new Rational(-5, 1);
            Assert.AreEqual("-5", a.ToString());
            a = new Rational(5, -1);
            Assert.AreEqual("-5", a.ToString());
            a = new Rational(5, 3);
            Assert.AreEqual("5/3", a.ToString());
            a = new Rational(-5, 3);
            Assert.AreEqual("-5/3", a.ToString());
            a = new Rational(5, -3);
            Assert.AreEqual("-5/3", a.ToString());
            a = new Rational(-5, -3);
            Assert.AreEqual("5/3", a.ToString());

        }


    }
}
