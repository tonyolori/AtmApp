using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmApp.Messages
{
    public class QuirkyMessages : IMessages
    {
        public void WelcomeGreeting()
        {
            string welcomeMessage = @"
****************************************************
*                                                  *
*                                                  *
*    _____  ________      ________ _   _ _______   * 
*   |  __ \|  ____\ \    / /  ____| \ | |__   __|  * 
*   | |__) | |__   \ \  / /| |__  |  \| |  | |     * 
*   |  _  /|  __|   \ \/ / |  __| | . ` |  | |     * 
*   | | \ \| |____   \  /  | |____| |\  |  | |     * 
*   |_|  \_\______|   \/   |______|_| \_|  |_|     * 
*                                                  * 
*                                                  * 
*                                                  *
*                                                  *
*              WELCOME TO YOUR ATM                 *
*                   Press 1                        *
*             to begin your transaction            *
*                                                  *
****************************************************
";
            Console.WriteLine(welcomeMessage);
        }

        public void Input(string text)
        {
            Console.WriteLine($"input your {text}");
        }

        public void Success(string text)
        {
            Console.WriteLine($"congrats, you even know how to {text}");
        }

        public void Error(string text)
        {
            Console.WriteLine($"haha, you messed up a simple {text}");
        }

        public void DisplayValidationError(string error)
        {
            Console.WriteLine($"enter a proper {error}, or you dont know how to?");
        }

        public void Invalid(string text)
        {
            Console.WriteLine($"try again, you can enter a proper {text}");
        }

        public void Custom(string text)
        {
            Console.WriteLine(text);
        }
    }
}
