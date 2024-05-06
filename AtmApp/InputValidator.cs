using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmApp
{
    internal class InputValidator : IInputValidator
    {
        public bool ValidateAgainstList<T>(T givenInput, List<T> acceptedInputs)
        {
            if (givenInput == null)
            {
                return false;
            }
            foreach (T option in acceptedInputs)
            {

                if (givenInput.Equals(option))
                {
                    return true;
                }
            }
            return false;
        }


        public bool ValidateFormat<T>(Func<string, T> ParseFunc, string? input, out T? result)
        {
            try
            {
                result = ParseFunc(input);
            }
            catch
            {
                result = default;
                return false;
            }

            return true;
        }

        public bool ValidateLength<T>(T input, int length) 
        {
            if(input.ToString().Length == length)
            return true;

            return false;
        }

        public T LoopValidateFormat<T>(Func<string, T> ParseFunc, IMessages outputMessage)
        {
            while (true)
            {
                string? input = Console.ReadLine();

                try
                {
                    T result = ParseFunc(input);
                    return result;
                }
                catch
                {
                    //var message = typeof(T).Name;
                    outputMessage.Invalid("Input");
                    continue;
                }
            }
        }


    }
}
