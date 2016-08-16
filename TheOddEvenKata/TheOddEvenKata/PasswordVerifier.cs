using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace TheOddEvenKata
{
    public class PasswordVerifier
    {
        public bool Verify(string password)
        {
            bool result = false;

            if(password != string.Empty)
            {
                if (password.Length > 8)
                {
                    if(password.Any(c => char.IsUpper(c)))
                    {
                        if(password.Any(c => char.IsLower(c)))
                        {
                            if(password.Any(c => char.IsDigit(c)))
                            {
                                result = true;
                            }
                            else
                            {
                                throw new NoDigitException();
                            }
                        }
                        else
                        {
                            throw new NoLowerLetterException();
                        }
                    }
                    else
                    {
                        throw new NoUpperLetterException();
                    }
                }
                else
                {
                    throw new ToShortStringException();
                }
            }
            else
            {
                throw new EmptyStringException();
            }
            

            return result;
        }
    }

    public class EmptyStringException:Exception
    {
    }

    public class ToShortStringException : Exception
    {
    }

    public class NoUpperLetterException : Exception
    {
    }

    public class NoLowerLetterException : Exception
    {
    }

    public class NoDigitException : Exception
    {
    }


}
