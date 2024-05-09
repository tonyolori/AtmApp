using AtmApp;
using System;
using System.IO;

public class FileIO
{
    // Todo: can you get a relative path
    static readonly string filePath = "C:\\Users\\Tony\\source\\repos\\AtmApp\\AtmApp\\Database\\accounts.csv";

    public static void write(List<UserAccount> accounts)
    {
        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
           
            foreach (var account in accounts)
            {
                writer.WriteLine($"{account.AccountNumber}, {account.Balance}, {account.Pin}");
            }
        }
    }

    public static List<UserAccount> Read()
    {
        List<UserAccount> accounts = new List<UserAccount>();
        using (StreamReader reader = new StreamReader(filePath))
        {
            // Skip header line
            reader.ReadLine();
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] parts = line.Split(',');
                if (parts.Length == 3)
                {
                    UserAccount account = new UserAccount
                    {
                        AccountNumber = uint.Parse(parts[0]),
                        Balance = uint.Parse(parts[1]),
                        Pin = uint.Parse(parts[2])
                    };
                    accounts.Add(account);
                }
            }
        }
        return accounts;
    }

    public static void Update(UserAccount updatedAccount)
    {
        string tempFile = Path.GetTempFileName();
        using (StreamReader reader = new StreamReader(filePath))
        using (StreamWriter writer = new StreamWriter(tempFile))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] parts = line.Split(',');
                if (parts.Length == 3 && uint.Parse(parts[0]) == updatedAccount.AccountNumber)
                {
                    writer.WriteLine($"{updatedAccount.AccountNumber},{updatedAccount.Balance},{updatedAccount.Pin}");
                }
                else
                {
                    writer.WriteLine(line);
                }
            }
        }
        File.Delete(filePath);
        File.Move(tempFile, filePath);
    }

}