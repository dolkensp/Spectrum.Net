using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Spectrum.Net.Core
{
    public static class ExtensionMethods
    {
        public static String ToBase(this Int32 input, Int32 radix, String digits = "0123456789abcdefghijklmnopqrstuvwxyz")
        {
            return ExtensionMethods.ToBase((Int64)input, radix, digits);
        }

        /// <summary>
        /// Converts the given decimal number to the numeral system with the
        /// specified radix (in the range [2, 36]).
        /// </summary>
        /// <param name="input">The number to convert.</param>
        /// <param name="radix">The radix of the destination numeral system (in the range [2, 36]).</param>
        /// <returns></returns>
        public static String ToBase(this Int64 input, Int32 radix, String digits = "0123456789abcdefghijklmnopqrstuvwxyz")
        {
            const Int32 bitsInLong = 64;
            
            if (radix < 2 || radix > digits.Length)
                throw new ArgumentException("The radix must be >= 2 and <= " + digits.Length.ToString());

            if (input == 0)
                return "0";

            Int32 index = bitsInLong - 1;
            Int64 currentNumber = Math.Abs(input);
            Char[] charArray = new Char[bitsInLong];

            while (currentNumber != 0)
            {
                Int32 remainder = (Int32)(currentNumber % radix);
                charArray[index--] = digits[remainder];
                currentNumber = currentNumber / radix;
            }

            String result = new String(charArray, index + 1, bitsInLong - index - 1);
            if (input < 0)
            {
                result = "-" + result;
            }

            return result;
        }
    }
}
