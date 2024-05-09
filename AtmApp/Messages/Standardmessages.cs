namespace AtmApp
{
    public class Standardmessages : IMessages
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
*                   Press 2                        *
*             to begin your transaction            *
*                                                  *
*                    1  to                         *
*               Create an account                  *
*                                                  * 
*                   0 to cancel                    *
****************************************************
";

            Console.WriteLine(welcomeMessage);

        }

        public void Input(string text)
        {
            Console.WriteLine($"Please input your {text}");
        }

        public void Success(string text)
        {
            Console.WriteLine($"{text} Success");
        }

        public void Error(string text)
        {
            Console.WriteLine($"{text} failed");
        }

        public void DisplayValidationError(string error)
        {
            Console.WriteLine($"Invalid {error}");
        }

        public void Invalid(string text)
        {
            Console.WriteLine($"invalid {text}, try again");
        }

        public void Custom(string text)
        {
            Console.WriteLine(text);
        }
    }
}
