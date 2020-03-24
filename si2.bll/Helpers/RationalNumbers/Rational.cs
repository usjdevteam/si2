using System;
using System.Collections.Generic;
using System.Text;

namespace si2.bll.Helpers.RationalNumbers
{
    public class Rational
    {
        private int _num;
        private int _den;

        public int Numerator
        {
            get
            {
                return _num;
            }
        }

        public int Denominator
        {
            get
            {
                return _den;
            }
        }

        public Rational(int num, int den = 1)
        {
            if (den < 0)
            {
                den = -den;
                num = -num;
            }
            if (den == 0)
            {
                throw new InfiniteRationalNumber();
            }
            if (num == 0)
            {
                _num = 0;
                _den = 1;
            }
            else
            {
                int p = pgcd(num, den);
                _num = num / p;
                _den = den / p;
            }
        }

        public Rational(float f)
        {
            _num = Convert.ToInt32(f*1000);
            _den = 1000;
            int p = pgcd(_num, _den);
            _num = _num / p;
            _den = _den / p;
        }

        public Rational(double f)
        {
            _num = Convert.ToInt32(f * 1000);
            _den = 1000;
            int p = pgcd(_num, _den);
            _num = _num / p;
            _den = _den / p;
        }

        public Rational(string s)
        {
            string[] parts = s.Split('/');
            if (parts.Length == 1)
            {
                _num = int.Parse(parts[0]);
                _den = 1;
            }
            else if (parts.Length == 2)
            {
                _num = int.Parse(parts[0]);
                _den = int.Parse(parts[1]);
                int p = pgcd(_num, _den);
                if (_den < 0)
                {
                    _num = -_num / p;
                    _den = -_den / p;
                }
                else
                {
                    _num = _num / p;
                    _den = _den / p;
                }

            }
            else
            {
                throw new RationalConversionException();
            }
        }

        public Rational(Rational x)
        {
            _num = x._num;
            _den = x._den;
        }

        private static int pgcd(int a, int b)
        {
            if (a < 0) a = -a;
            if (b < 0) b = -b;
            if (a == 1 || b == 1)
                return 1;
            if (a % b == 0)
            {
                return b;
            }
            return pgcd(b, a % b);
            
        }

        public override string ToString()
        {
            if (_den == 1)
            {
                return _num.ToString();
            }
            return _num.ToString() + "/" + _den.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj is Rational)
                return this == (Rational) obj;
            return false;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public static Rational operator+(Rational a, Rational b)
        {
            return new Rational(a._num * b._den + a._den * b._num, a._den * b._den);
        }

        public static Rational operator-(Rational a, Rational b)
        {
            return new Rational(a._num * b._den - a._den * b._num, a._den * b._den);
        }

        public static Rational operator*(Rational a, Rational b)
        {
            return new Rational(a._num * b._num, a._den * b._den);
        }
        public static Rational operator/(Rational a, Rational b)
        {
            return new Rational(a._num * b._den, a._den * b._num);
        }

        public static bool operator <(Rational a, Rational b)
        {
            return a._num * b._den - a._den * b._num < 0;
        }
        public static bool operator >(Rational a, Rational b)
        {
            return a._num * b._den - a._den * b._num > 0;
        }

        public static bool operator <=(Rational a, Rational b)
        {
            return a._num * b._den - a._den * b._num <= 0;
        }
        public static bool operator >=(Rational a, Rational b)
        {
            return a._num * b._den - a._den * b._num >= 0;
        }

        public static bool operator==(Rational a, Rational b)
        {
            return a._num * b._den == a._den * b._num;
        }

        public static bool operator!=(Rational a, Rational b)
        {
            return a._num * b._den != a._den * b._num;
        }

        public static bool operator<(Rational a, int i)
        {
            return a._num < a._den * i;
        }
        public static bool operator >(Rational a, int i)
        {
            return a._num > a._den * i;
        }
    }
}
