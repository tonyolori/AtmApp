using AtmApp.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmApp
{
    public class Factory
    {
        public static IMessages CreateMessages()
        {
            return new Standardmessages();
        }

        public static IInputValidator CreateValidator()
        {
            return new InputValidator();
        }
    }
}
