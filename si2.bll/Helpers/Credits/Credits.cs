using si2.bll.Helpers.RationalNumbers;
using System;
using System.Collections.Generic;
using System.Text;

namespace si2.bll.Helpers.Credits
{
    public class Credits
    {
        public Decimal Value { get; }

        public Credits(float credits)
        {
            if (credits < 0)
            {
                throw new NegativeCreditsException();
            }
            Value = Convert.ToDecimal(credits);
        }

        public Credits(int credits)
        {
            if (credits < 0)
            {
                throw new NegativeCreditsException();
            }
            Value = new Decimal(credits);
        }

        public Credits(Decimal credits)
        {
            if (credits < 0)
            {
                throw new NegativeCreditsException();
            }
            Value = credits;
        }

        override public string ToString()
        {
            return Value.ToString() + " Cr.";
        }

        public static Credits operator+(Credits a, Credits b)
        {
            return new Credits(a.Value + b.Value);
        }

        public static Credits operator -(Credits a, Credits b)
        {
            return new Credits(a.Value - b.Value);
        }
    }
}
