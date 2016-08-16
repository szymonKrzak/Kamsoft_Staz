using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator
{
    public class StringCalc
    {
        public int Add(string numbers)
        {
            int result = 0;

            if(isContainsEmptyString(numbers))
            {
                result = 0;
            }
            else if(isContainsSingleNumber(numbers))
            {
                result = SumOfNumbers(numbers);
            }
            else if(isContainsNewLine(numbers))
            {
                result = SumOfNumbers(numbers[0].ToString(), numbers[2].ToString(), numbers[4].ToString());
            }
            else if(isContainsComma(numbers))
            {
                result = SumOfNumbers(numbers[0].ToString(), numbers[2].ToString());
            }
            else
            {
                throw new NotImplementedException();
            }

            return result;
        }

        public bool isContainsEmptyString(string str)
        {
            if (str == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool isContainsSingleNumber(string str)
        {
            if (str.Length <= 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool isContainsComma(string str)
        {
            if (str.Contains(","))
            {
                return true;
            }
            else
                return false;
        }
        public bool isContainsNewLine(string str)
        {
            if (str.Contains("\n"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int ConvertStringToInt(string str)
        {
            int temp;
            if(str == null)
            {
                return 0;
            }
            else
            {
                temp = Int32.Parse(str);

                if(temp > 0)
                {
                    return temp;
                }
                else
                {
                    throw new NegativesNotAllowedException(temp);
                }
            }
 
        }

        public int SumOfNumbers(string firstStr)
        {
            return SumOfNumbers(firstStr, null, null);
        }
        public int SumOfNumbers(string firstStr, string secondStr)
        {
            return SumOfNumbers(firstStr, secondStr, null);
        }
        public int SumOfNumbers(string firstStr, string secondStr, string thirdStr)
        {
            return ConvertStringToInt(firstStr) + ConvertStringToInt(secondStr) + ConvertStringToInt(thirdStr);
        }
    }

    public class NegativesNotAllowedException: Exception
    {
        List<int> listOfNegativeNumbers;
        public NegativesNotAllowedException(int str)
        {
            listOfNegativeNumbers = new List<int>();
            listOfNegativeNumbers.Add(str);
        }
    }
}
