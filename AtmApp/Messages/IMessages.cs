using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmApp
{
    public interface IMessages
    {
        public void WelcomeGreeting();
        public void Input(string text);

        public void Success(string text);

        public void Error(string text);

        public void DisplayValidationError(string error);
        public void Invalid(string text);
        public void Custom(string text);

    }
}
