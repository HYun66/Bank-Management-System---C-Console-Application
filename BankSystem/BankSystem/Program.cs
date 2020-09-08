using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Net;

namespace BankSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Login myLogin = new Login();
            myLogin.LoginScreen();
            myLogin.CheckUser();
        }
    }

    public class Login
    {
        public string username;
        public string password;

        public void LoginScreen()
        {
            //remember add comment
            //Console.Clear();
            //Console.SetWindowSize(60, 40);
            //Console.SetCursorPosition(0,0);
            Console.WriteLine("\t\t\t========================================");
            Console.WriteLine("\t\t\t|   WELCOME TO SIMPLE BANKING SYSTEM   |");
            Console.WriteLine("\t\t\t|======================================|");
            Console.WriteLine("\t\t\t|          LOGIN TO START              |");
            Console.WriteLine("\t\t\t|                                      |");
            //Console.Write("|");
            Console.Write("\t\t\t|\tUsername:");
            int cursorPositionUsernameLeft = Console.CursorLeft;
            Console.Write("\t\t       |\n");
            Console.Write("\t\t\t|\tPassword:");
            int cursorPositionPasswordLeft = Console.CursorLeft;
            Console.Write("\t\t       |\n");
            Console.WriteLine("\t\t\t========================================");
            Console.SetCursorPosition(cursorPositionUsernameLeft, 5);
            username = Console.ReadLine();
            Console.SetCursorPosition(cursorPositionPasswordLeft, 6);
            password = ReadPassword();
            Console.WriteLine("\n\n\t\t\tPlease enter...");
            Console.ReadKey();
        }

        public static string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo info = Console.ReadKey(true);
            while (info.Key != ConsoleKey.Enter)
            {
                if (info.Key != ConsoleKey.Backspace)
                {
                    Console.Write("*");
                    password += info.KeyChar;
                }
                else if (info.Key == ConsoleKey.Backspace)
                {
                    if (!string.IsNullOrEmpty(password))
                    {
                        // remove one character from the list of password characters
                        password = password.Substring(0, password.Length - 1);
                        // get the location of the cursor
                        int pos = Console.CursorLeft;
                        // move the cursor to the left by one character
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                        // replace it with space
                        Console.Write(" ");
                        // move the cursor to the left by one character again
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                    }
                }
                info = Console.ReadKey(true);
            }

            return password;

        }
        public void CheckUser()
        {

            try
            {
                FileStream loginFS = new FileStream("login.txt", FileMode.Open, FileAccess.Read);
                using (StreamReader streamReader = new StreamReader(loginFS, Encoding.UTF8))
                {
                    string line = string.Empty;
                    bool userExist = false;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        // Split each line to two string.
                        string str = line;
                        char separater = '|';
                        string[] strList = str.Split(separater);

                        // Get username ans password.
                        string strUsername = strList[0];
                        string strPassword = strList[1];

                        if (username == strUsername && password == strPassword)
                        {
                            userExist = true;
                            break;
                        }
                    }

                    if (userExist == true)
                    {
                        Console.WriteLine("\t\t\tWelcome! {0}... Please press a key to continue...", username);
                        Console.ReadKey();
                        MainMenu myMainmenu = new MainMenu();
                        Console.Clear();
                        myMainmenu.MainMenuScreen();
                    }
                    else
                    {

                        Console.WriteLine("\t\t\tNot found Username or Password, Please check!           ");
                        Console.WriteLine("\t\t\tPlease press ENTER to try again...                       ");
                        Console.WriteLine("\t\t\tPlease press any other key to exit...    ");
                        ConsoleKeyInfo info = Console.ReadKey(true);
                        if (info.Key == ConsoleKey.Enter)
                        {
                            Console.Clear();
                            Login myLogin = new Login();
                            myLogin.LoginScreen();
                            myLogin.CheckUser();
                        }
                        else if (info.Key != ConsoleKey.Enter)
                        {
                            Console.Clear();
                            Console.WriteLine("\n\t\t\tYou exit the Simple Banking System!");
                            Console.WriteLine("\n\t\t\tPlease press ENTER to Login ...                   ");
                            ConsoleKeyInfo infoKey = Console.ReadKey(true);
                            if (infoKey.Key == ConsoleKey.Enter)
                            {
                                Console.Clear();
                                Login myLogin = new Login();
                                myLogin.LoginScreen();
                                myLogin.CheckUser();
                            }
                        }
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error:{0}", e);
            }
        }

    }

    public class MainMenu
    {

        public int inputNumber;
        public string userInput;
        public int length;
        public int cursorPositionMainmenuLeft;
        public void MainMenuScreen()
        {
            Console.WriteLine("\t\t\t========================================");
            Console.WriteLine("\t\t\t|   WELCOME TO SIMPLE BANKING SYSTEM   |");
            Console.WriteLine("\t\t\t|======================================|");
            Console.WriteLine("\t\t\t|      1. Create a new account         |");
            Console.WriteLine("\t\t\t|      2. Search for an account        |");
            Console.WriteLine("\t\t\t|      3. Deposit                      |");
            Console.WriteLine("\t\t\t|      4. Withdraw                     |");
            Console.WriteLine("\t\t\t|      5. A/C statement                |");
            Console.WriteLine("\t\t\t|      6. Delete account               |");
            Console.WriteLine("\t\t\t|      7. Exit                         |");
            Console.WriteLine("\t\t\t|======================================|");
            Console.Write("\t\t\t|    Enter your choice (1-7):");
            cursorPositionMainmenuLeft = Console.CursorLeft;
            Console.Write("          |\n");
            Console.WriteLine("\t\t\t========================================");
            Console.SetCursorPosition(cursorPositionMainmenuLeft, 11);
            ReadInput();
        }
        public void ReadInput()
        {
            bool validUserInput = false;
            userInput = "";

            while (validUserInput == false)
            {

                if (userInput == "")
                {
                    userInput = Console.ReadLine();
                    length = userInput.Length;
                    Console.SetCursorPosition(cursorPositionMainmenuLeft + length, 11);
                }

                length = userInput.Length;
                Console.SetCursorPosition(cursorPositionMainmenuLeft + length, 11);


                if (Numbers.IsDigitsOnly(userInput) && int.TryParse(userInput, out inputNumber) && inputNumber > 0 && inputNumber < 8 && inputNumber > 0)
                {
                    switch (inputNumber)
                    {
                        case 1:
                            Console.Clear();
                            CreateAccount myCreateAccount = new CreateAccount();
                            myCreateAccount.CreateAccountScreen();
                            myCreateAccount.SaveAccount();
                            break;
                        case 2:
                            Console.Clear();
                            SearchAccount mySearchAccount = new SearchAccount();
                            mySearchAccount.SearchAccountScreen();
                            break;
                        case 3:
                            Console.Clear();
                            Deposit myDeposit = new Deposit();
                            myDeposit.DepositScreen();
                            break;
                        case 4:
                            Console.Clear();
                            Withdrawal myWithdrawal = new Withdrawal();
                            myWithdrawal.WithdrawalScreen();
                            break;
                        case 5:
                            Console.Clear();
                            Statement myStatement = new Statement();
                            myStatement.StatementScreen();
                            break;
                        case 6:
                            Console.Clear();
                            DeleteAccount myDeleteAccount = new DeleteAccount();
                            myDeleteAccount.DeleteAccountScreen();
                            break;
                        case 7:
                            Console.WriteLine("\n\n\n\t\t\tPress a key to exit...");
                            Console.ReadKey();
                            Console.Clear();
                            Console.WriteLine("\n\t\t\tYou exit the Simple Banking System!");
                            Console.WriteLine("\n\t\t\tPlease press ENTER to Login ...                   ");
                            ConsoleKeyInfo infoKey = Console.ReadKey(true);
                            if (infoKey.Key == ConsoleKey.Enter)
                            {
                                Console.Clear();
                                Login myLogin = new Login();
                                myLogin.LoginScreen();
                                myLogin.CheckUser();
                            }
                            break;

                    }
                    validUserInput = true;
                }
                else
                {
                    Console.WriteLine("\n\n\t\t\tInvalid input! Please enter a valid number between 1 to 7...");
                    //userInput = "";
                    Console.SetCursorPosition(cursorPositionMainmenuLeft + length, 11);

                    ConsoleKeyInfo info = Console.ReadKey(true);
                    while (info.Key != ConsoleKey.Enter)
                    {

                        if (info.Key == ConsoleKey.Backspace)
                        {
                            if (!string.IsNullOrEmpty(userInput))
                            {
                                // remove one character from the list of password characters
                                userInput = userInput.Substring(0, userInput.Length - 1);
                                // get the location of the cursor
                                int pos = Console.CursorLeft;
                                // move the cursor to the left by one character
                                Console.SetCursorPosition(pos - 1, Console.CursorTop);
                                // replace it with space
                                Console.Write(" ");
                                // move the cursor to the left by one character again
                                Console.SetCursorPosition(pos - 1, Console.CursorTop);
                            }
                        }
                        else if (info.Key != ConsoleKey.Backspace)
                        {
                            Console.Write(info.KeyChar);
                            userInput += info.KeyChar;
                        }

                        info = Console.ReadKey(true);
                    }

                }
            }
        }
    }

    static class AccountNo
    {
        public static int accountNo = 100000;
    }

    static class Numbers
    {
        public static bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
    }

  
    static class Account
    {
        public static bool AccountExist(int accountNumber)
        {
            string fileName = accountNumber + ".txt";
            // Set a variable to the Documents path.
            string destPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            bool existFlag = File.Exists(destPath);

            if (existFlag)
            {
                return true;
            }

            return false;
        }
        public static int accountNumber;
        public static string accountNumberStr;
        public static bool parseSuccess;
        public static int accountNoLength;
        public static int amount;
        public static string amountStr;
        public static int amountLength;

        public static int ReadAccountNo()
        {
            accountNumberStr = "";
            parseSuccess = false;
            do
            {

                if (accountNumberStr == "")
                {
                    accountNumberStr = Console.ReadLine();
                    accountNoLength = accountNumberStr.Length;
                    Console.SetCursorPosition(47 + accountNoLength, 5);
                }

                accountNoLength = accountNumberStr.Length;
                parseSuccess = int.TryParse(accountNumberStr, out accountNumber);
                    

                if (parseSuccess && Numbers.IsDigitsOnly(accountNumberStr) && accountNoLength < 11 && accountNumber > 0)
                {
                    accountNumber = Convert.ToInt32(accountNumberStr);
                    //Console.WriteLine("\n\n\n\n\n" + accountNumber);
                }
                else
                {
                    Console.WriteLine("\n\n\n\t\t\tNot valid! Please enter no more than 10 number!");
                    Console.WriteLine("\t\t\tUsing backspace to enter again!");
                    Console.SetCursorPosition(47 + accountNoLength, 5);

                    ConsoleKeyInfo info = Console.ReadKey(true);
                    while (info.Key != ConsoleKey.Enter)
                    {

                        if (info.Key == ConsoleKey.Backspace)
                        {
                            if (!string.IsNullOrEmpty(accountNumberStr))
                            {
                                // remove one character from the list of password characters
                                accountNumberStr = accountNumberStr.Substring(0, accountNumberStr.Length - 1);
                                // get the location of the cursor
                                int pos = Console.CursorLeft;
                                // move the cursor to the left by one character
                                Console.SetCursorPosition(pos - 1, Console.CursorTop);
                                // replace it with space
                                Console.Write(" ");
                                // move the cursor to the left by one character again
                                Console.SetCursorPosition(pos - 1, Console.CursorTop);
                            }
                        }
                        else if (info.Key != ConsoleKey.Backspace)
                        {
                            Console.Write(info.KeyChar);
                            accountNumberStr += info.KeyChar;
                        }
                        info = Console.ReadKey(true);
                    }
                }

            } while (!parseSuccess || !Numbers.IsDigitsOnly(accountNumberStr) || accountNoLength >= 11 || accountNumber < 0);

            return accountNumber;
        }

        public static int ReadAmount()
        {
            amountStr = "";
            parseSuccess = false;
            do
            {

                if (amountStr == "")
                {
                    amountStr = Console.ReadLine();
                    amountLength = amountStr.Length;
                    Console.SetCursorPosition(42 + amountLength, 6);
                }

                amountLength = amountStr.Length;
                parseSuccess = int.TryParse(amountStr, out amount);
                

                if (parseSuccess && Numbers.IsDigitsOnly(amountStr) && amountLength < 11 && amount >= 0)
                {
                    amount = Convert.ToInt32(amountStr);
                    Console.WriteLine("\n\n\n\n\n" + amount);
                }
                else
                {
                    Console.WriteLine("\n\n\n\t\t\tNot valid! Please enter amount number no more than 10 number!");
                    Console.WriteLine("\t\t\tUsing backspace to enter again!");
                    Console.SetCursorPosition(42 + amountLength, 6);

                    ConsoleKeyInfo info = Console.ReadKey(true);
                    while (info.Key != ConsoleKey.Enter)
                    {

                        if (info.Key == ConsoleKey.Backspace)
                        {
                            if (!string.IsNullOrEmpty(amountStr))
                            {
                                // remove one character from the list of password characters
                                amountStr = amountStr.Substring(0, amountStr.Length - 1);
                                // get the location of the cursor
                                int pos = Console.CursorLeft;
                                // move the cursor to the left by one character
                                Console.SetCursorPosition(pos - 1, Console.CursorTop);
                                // replace it with space
                                Console.Write(" ");
                                // move the cursor to the left by one character again
                                Console.SetCursorPosition(pos - 1, Console.CursorTop);
                            }
                        }
                        else if (info.Key != ConsoleKey.Backspace)
                        {
                            Console.Write(info.KeyChar);
                            amountStr += info.KeyChar;
                        }
                        info = Console.ReadKey(true);
                    }
                }

            } while (!parseSuccess || !Numbers.IsDigitsOnly(amountStr) || amountLength >= 11 || amount <0);

            return amount;
        }

        public static void DisplayAccount()
        {

            string fileName = accountNumber + ".txt";
            var lines = File.ReadAllLines(fileName);

            for (var i = 0; i < lines.Length; i += 1)
            {
                var line = lines[i];
                Console.WriteLine("\t\t\t\t" + line);

            }
            Console.WriteLine("\t\t\t========================================");
        }
    }


    public class CreateAccount
    {

        public string firstname;
        public string lastname;
        public string address;
        public int phone;
        bool parseSuccess;
        public int phoneLength;
        public string phoneAsString;
        public string email;
        bool validEmailFlag;
        public int emailLength;
        public int cursorPositionPhoneLeft;
        public int cursorPositionEmailLeft;

        public void CreateAccountScreen()
        {
            Console.WriteLine("\t\t\t========================================");
            Console.WriteLine("\t\t\t|         CREATE A NEW ACCOUNT         |");
            Console.WriteLine("\t\t\t|======================================|");
            Console.WriteLine("\t\t\t|          ENTER THE DETAILS           |");
            Console.WriteLine("\t\t\t|                                      |");
            //Console.Write("|");
            Console.Write("\t\t\t|\tFirst Name:");
            int cursorPositionFirstnameLeft = Console.CursorLeft;
            Console.Write("\t\t       |\n");
            Console.Write("\t\t\t|\tLast Name:");
            int cursorPositionLastnameLeft = Console.CursorLeft;
            Console.Write("\t\t       |\n");
            Console.Write("\t\t\t|\tAddress:");
            int cursorPositionAddressLeft = Console.CursorLeft;
            Console.Write("\t\t       |\n");
            Console.Write("\t\t\t|\tPhone:");
            cursorPositionPhoneLeft = Console.CursorLeft;
            Console.Write("\t\t\t       |\n");
            Console.Write("\t\t\t|\tEmail:");
            cursorPositionEmailLeft = Console.CursorLeft;
            Console.Write("\t\t\t       |\n");
            Console.WriteLine("\t\t\t========================================");

            // Read first name
            Console.SetCursorPosition(cursorPositionFirstnameLeft, 5);
            do
            {
                Console.SetCursorPosition(cursorPositionFirstnameLeft, 5);
                firstname = Console.ReadLine();
                //Console.WriteLine("length: {0}", firstname.Length);
            } while (firstname.Length == 0);

            // Read last name
            Console.SetCursorPosition(cursorPositionLastnameLeft, 6);
            do
            {
                Console.SetCursorPosition(cursorPositionLastnameLeft, 6);
                lastname = Console.ReadLine();
            } while (lastname.Length == 0);

            // Read address
            Console.SetCursorPosition(cursorPositionAddressLeft, 7);
            do
            {
                Console.SetCursorPosition(cursorPositionAddressLeft, 7);
                address = Console.ReadLine();
            } while (address.Length == 0);
            
            Console.SetCursorPosition(cursorPositionPhoneLeft, 8);
            phone = ReadPhone();
            Console.SetCursorPosition(cursorPositionEmailLeft, 9);
            email = ReadEmail();
            Console.WriteLine("\n\n\t\t\tPlease press ENTER to save..                         ");
            Console.WriteLine("                                                       ");
            Console.ReadKey();
            SaveAccount();
            Console.WriteLine("\n\n\t\t\tPlease press a key to return to MAIN MENU...       ");
            Console.ReadKey();
            MainMenu myMainmenu = new MainMenu();
            Console.Clear();
            myMainmenu.MainMenuScreen();
        }
        public int ReadPhone()
        {
            phoneAsString = "";
            parseSuccess = false;
            do
            {

                if (phoneAsString == "")
                {
                    phoneAsString = Console.ReadLine();
                    phoneLength = phoneAsString.Length;
                    Console.SetCursorPosition(cursorPositionPhoneLeft + phoneLength, 8);
                }

                phoneLength = phoneAsString.Length;
                parseSuccess = int.TryParse(phoneAsString, out phone);

                if (parseSuccess && Numbers.IsDigitsOnly(phoneAsString) && phoneLength < 11 && phone > 0)
                {
                    phone = Convert.ToInt32(phoneAsString);
                }
                else
                {
                    Console.WriteLine("\n\n\n\t\t\tNot valid! Please enter no more than 10 number!");
                    Console.WriteLine("\t\t\tUsing backspace to enter again!");
                    Console.SetCursorPosition(cursorPositionPhoneLeft + phoneLength, 8);

                    ConsoleKeyInfo info = Console.ReadKey(true);
                    while (info.Key != ConsoleKey.Enter)
                    {

                        if (info.Key == ConsoleKey.Backspace)
                        {
                            if (!string.IsNullOrEmpty(phoneAsString))
                            {
                                // remove one character from the list of password characters
                                phoneAsString = phoneAsString.Substring(0, phoneAsString.Length - 1);
                                // get the location of the cursor
                                int pos = Console.CursorLeft;
                                // move the cursor to the left by one character
                                Console.SetCursorPosition(pos - 1, Console.CursorTop);
                                // replace it with space
                                Console.Write(" ");
                                // move the cursor to the left by one character again
                                Console.SetCursorPosition(pos - 1, Console.CursorTop);
                            }
                        }
                        else if (info.Key != ConsoleKey.Backspace)
                        {
                            Console.Write(info.KeyChar);
                            phoneAsString += info.KeyChar;
                        }
                        info = Console.ReadKey(true);
                    }
                }

            } while (!parseSuccess || !Numbers.IsDigitsOnly(phoneAsString) || phoneLength >= 11 || phone < 0);

            return phone;

        }

        public static bool EmailIsValid(string email)
        {
            string expression = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";

            if (Regex.IsMatch(email, expression))
            {
                if (Regex.Replace(email, expression, string.Empty).Length == 0)
                {
                    return true;
                }
            }
            return false;
        }

        public string ReadEmail()
        {
            email = "";
            validEmailFlag = false;
            do
            {
                if (email == "")
                {
                    email = Console.ReadLine();
                    emailLength = email.Length;
                    Console.SetCursorPosition(cursorPositionEmailLeft + emailLength, 9);
                }

                emailLength = email.Length;
                validEmailFlag = EmailIsValid(email);

                if (validEmailFlag)
                {
                    return email;
                }

                Console.WriteLine("\n\n\t\t\tNot valid! Please enter email address contain @");
                Console.WriteLine("\t\t\tUsing backspace to enter again!");
                Console.SetCursorPosition(cursorPositionEmailLeft + emailLength, 9);

                ConsoleKeyInfo info = Console.ReadKey(true);
                while (info.Key != ConsoleKey.Enter)
                {

                    if (info.Key == ConsoleKey.Backspace)
                    {
                        if (!string.IsNullOrEmpty(email))
                        {
                            // remove one character from the list of password characters
                            email = email.Substring(0, email.Length - 1);
                            // get the location of the cursor
                            int pos = Console.CursorLeft;
                            // move the cursor to the left by one character
                            Console.SetCursorPosition(pos - 1, Console.CursorTop);
                            // replace it with space
                            Console.Write(" ");
                            // move the cursor to the left by one character again
                            Console.SetCursorPosition(pos - 1, Console.CursorTop);
                        }
                    }
                    else if (info.Key != ConsoleKey.Backspace)
                    {
                        Console.Write(info.KeyChar);
                        email += info.KeyChar;
                    }
                    info = Console.ReadKey(true);
                }

            } while (validEmailFlag == false);

            return email;
        }

        public void SaveAccount()
        {
            // Create a string with a line of text.
            string accountNumber = AccountNo.accountNo.ToString();

            // Set file name.
            string fileName = accountNumber + ".txt";
            // Set a variable to the Documents path.
            string destPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

            try
            {
                // Write the text to a new file.
                File.WriteAllText(destPath, "Account Number: " + accountNumber);

                // Create a string array with the additional lines of text
                string[] lines = { "\nFirst Name: " + firstname,
                               "Last Name: " + lastname,
                               "Address: " + address,
                               "Phone: " + phone.ToString(),
                               "Email: " + email,
                               "Account Balance: " + "$0"};

                // Append new lines of text to the file
                File.AppendAllLines(destPath, lines);
                AccountNo.accountNo += 1;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error:{0}", e);
            }
            
        }
    }


    public class SearchAccount
    {
        public int accountNumber;
        public int cursorPositionAccountnumberLeft;
        public bool accountExitFlag;

        public void SearchAccountScreen()
        {
            Console.WriteLine("\t\t\t========================================");
            Console.WriteLine("\t\t\t|          SEARCH AN ACCOUNT           |");
            Console.WriteLine("\t\t\t|======================================|");
            Console.WriteLine("\t\t\t|          ENTER THE DETAILS           |");
            Console.WriteLine("\t\t\t|                                      |");
            Console.Write("\t\t\t|\tAccount number:");
            cursorPositionAccountnumberLeft = Console.CursorLeft;
            Console.Write("\t\t       |\n");
            Console.WriteLine("\t\t\t========================================");
            Console.SetCursorPosition(cursorPositionAccountnumberLeft, 5);
            accountNumber = Account.ReadAccountNo();
            accountExitFlag = Account.AccountExist(accountNumber);
            if (!accountExitFlag)
            {
                Console.WriteLine("\n\n\t\t\tAccount not exist! Please check...            ");
                Console.WriteLine("\t\t\tPlease press ENTER to re-enter another account number...");
                Console.WriteLine("\t\t\tPlease press any other key to return to MAIN MENU...    ");
                ConsoleKeyInfo info = Console.ReadKey(true);
                if (info.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    SearchAccount mySearchAccount = new SearchAccount();
                    mySearchAccount.SearchAccountScreen();
                }
                else if (info.Key != ConsoleKey.Enter)
                {
                    Console.Clear();
                    MainMenu myMainmenu = new MainMenu();
                    myMainmenu.MainMenuScreen();
                }
            }
            else
            {
                Console.WriteLine("\n\n\t\t\tAccount found!\n");
                Console.WriteLine("\t\t\t========================================");
                Console.WriteLine("\t\t\t            ACCOUNT  DETAILS            ");
                Console.WriteLine("\t\t\t========================================");
                Account.DisplayAccount();
                Console.WriteLine("\n\n                                                          ");
                Console.WriteLine("\t\t\tPlease press ENTER to re-enter another account number...");
                Console.WriteLine("\t\t\tPlease press any other key to return to MAIN MENU...    ");
                ConsoleKeyInfo info = Console.ReadKey(true);
                if (info.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    SearchAccount mySearchAccount = new SearchAccount();
                    mySearchAccount.SearchAccountScreen();
                }
                else if (info.Key != ConsoleKey.Enter)
                {
                    Console.Clear();
                    MainMenu myMainmenu = new MainMenu();
                    myMainmenu.MainMenuScreen();
                }
            }
        }
    }

    public class Deposit
    {
        public int accountNumber;
        public int amount;
        public bool accountExitFlag;

        public void DepositScreen()
        {
            Console.WriteLine("\t\t\t========================================");
            Console.WriteLine("\t\t\t|               DEPOSIT                |");
            Console.WriteLine("\t\t\t|======================================|");
            Console.WriteLine("\t\t\t|          ENTER THE DETAILS           |");
            Console.WriteLine("\t\t\t|                                      |");
            Console.Write("\t\t\t|\tAccount number:");
            int cursorPositionAccountnumberLeft = Console.CursorLeft;
            Console.Write("\t\t       |\n");
            Console.Write("\t\t\t|\tAmount: $ ");
            int cursorPositionAmountLeft = Console.CursorLeft;
            Console.Write("\t\t       |\n");
            Console.WriteLine("\t\t\t========================================");
            Console.SetCursorPosition(cursorPositionAccountnumberLeft, 5);
            accountNumber = Account.ReadAccountNo();
            accountExitFlag = Account.AccountExist(accountNumber);

            if (!accountExitFlag)
            {
                Console.WriteLine("\n\n\n\t\t\tAccount not exist! Please check...            ");
                Console.WriteLine("\t\t\tPlease press ENTER to re-enter another account number...");
                Console.WriteLine("\t\t\tPlease press any other key to return to MAIN MENU...    ");
                ConsoleKeyInfo info = Console.ReadKey(true);
                if (info.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    Deposit myDeposit = new Deposit();
                    myDeposit.DepositScreen();

                }
                else if (info.Key != ConsoleKey.Enter)
                {
                    Console.Clear();
                    MainMenu myMainmenu = new MainMenu();
                    myMainmenu.MainMenuScreen();
                }
            }
            else
            {
                Console.WriteLine("\n\n\n\t\t\tAccount found, enter the amount...                 ");
                Console.SetCursorPosition(cursorPositionAmountLeft, 6);
                amount = Account.ReadAmount();
                SaveAmountDeposit();
                Console.WriteLine("\t\t\tPlease press ENTER to re-enter another account number...");
                Console.WriteLine("\t\t\tPlease press any other key to return to MAIN MENU...    ");
                ConsoleKeyInfo info = Console.ReadKey(true);
                if (info.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    Deposit myDeposit = new Deposit();
                    myDeposit.DepositScreen();
                }
                else if (info.Key != ConsoleKey.Enter)
                {
                    Console.Clear();
                    MainMenu myMainmenu = new MainMenu();
                    myMainmenu.MainMenuScreen();
                }
            }
        }

        public void SaveAmountDeposit()
        {
            // Set file name.
            string fileName = accountNumber.ToString() + ".txt";

            // Set a variable to the Documents path.
            string destPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

            try
            {
                // Get text list and the last line
                var fileContent = File.ReadLines(destPath).ToList();
                string lastLine = fileContent[fileContent.Count - 1];

                // Split last line to get balance
                char separater = '$';
                string[] strList = lastLine.Split(separater);
                int balance = Convert.ToInt32(strList[1]);
                int balanceNow = balance + amount;
                string lastLineNew = strList[0] + "$" + balanceNow.ToString();

                // Overwrite last line of the document 
                fileContent[fileContent.Count - 1] = lastLineNew;
                File.WriteAllLines(destPath, fileContent);
                Console.WriteLine("\n\n\n\t\t\tDeposit successful!                                   ");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error:{0}", e);
            }

        }
    }

    public class Withdrawal
    {
        public int accountNumber;
        public int amount;
        public int amountLength;
        public bool accountExitFlag;
        public int cursorPositionAmountLeft;

        public void WithdrawalScreen()
        {
            Console.WriteLine("\t\t\t========================================");
            Console.WriteLine("\t\t\t|              WITHDRAWAL              |");
            Console.WriteLine("\t\t\t|======================================|");
            Console.WriteLine("\t\t\t|          ENTER THE DETAILS           |");
            Console.WriteLine("\t\t\t|                                      |");
            Console.Write("\t\t\t|\tAccount number:");
            int cursorPositionAccountnumberLeft = Console.CursorLeft;
            Console.Write("\t\t       |\n");
            Console.Write("\t\t\t|\tAmount: $ ");
            cursorPositionAmountLeft = Console.CursorLeft;
            Console.Write("\t\t       |\n");
            Console.WriteLine("\t\t\t========================================");
            Console.SetCursorPosition(cursorPositionAccountnumberLeft, 5);
            accountNumber = Account.ReadAccountNo();
            accountExitFlag = Account.AccountExist(accountNumber);

            if (!accountExitFlag)
            {
                Console.WriteLine("\n\n\n\t\t\tAccount not exist! Please check...            ");
                Console.WriteLine("\t\t\tPlease press ENTER to re-enter another account number...");
                Console.WriteLine("\t\t\tPlease press any other key to return to MAIN MENU...    ");

                ConsoleKeyInfo info = Console.ReadKey(true);
                if (info.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    Withdrawal myWithdrawal = new Withdrawal();
                    myWithdrawal.WithdrawalScreen();

                }
                else if (info.Key != ConsoleKey.Enter)
                {
                    Console.Clear();
                    MainMenu myMainmenu = new MainMenu();
                    myMainmenu.MainMenuScreen();
                }
            }
            else
            {
                Console.WriteLine("\n\n\n\t\t\tAccount found, enter the amount...                 ");
                Console.SetCursorPosition(cursorPositionAmountLeft, 6);
                SaveAmountWithdrawal();

                Console.WriteLine("\t\t\tPlease press ENTER to re-enter another account number...");
                Console.WriteLine("\t\t\tPlease press any other key to return to MAIN MENU...    ");
                ConsoleKeyInfo info = Console.ReadKey(true);
                if (info.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    Withdrawal myWithdrawal = new Withdrawal();
                    myWithdrawal.WithdrawalScreen();
                }
                else if (info.Key != ConsoleKey.Enter)
                {
                    Console.Clear();
                    MainMenu myMainmenu = new MainMenu();
                    myMainmenu.MainMenuScreen();
                }

            }
        }

        public void SaveAmountWithdrawal()
        {
            string fileName = accountNumber.ToString() + ".txt";
            // Set a variable to the Documents path.
            string destPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

            try
            {
                var fileContent = File.ReadLines(destPath).ToList();
                string lastLine = fileContent[fileContent.Count - 1];
                char separater = '$';
                string[] strList = lastLine.Split(separater);
                int balance = Convert.ToInt32(strList[1]);
            
                do
                {
                    amount = Account.ReadAmount();
                    if ((balance - amount) < 0)
                    {
                        Console.WriteLine("\n\n\n\t\t\tBalance not enough! Enter amount again...");
                        amountLength = amount.ToString().Length;
                        Console.SetCursorPosition(42 + amountLength, 6);
                        int pos = Console.CursorLeft;

                        // Move the cursor to the left for re-enter.
                        Console.SetCursorPosition(pos - amountLength, 6);

                        // Replace invalid input of amount with space.
                        for (int i = 0; i < amountLength; i++)
                        {
                            Console.Write(" ");
                        }

                        // Move the cursor to the left again.
                        Console.SetCursorPosition(pos - amountLength, 6);
                    }
                } while ((balance - amount) < 0);


                if ((balance - amount) >= 0 )
                {
                    int balanceNow = balance - amount;

                    // Overwrite the last line of balance.
                    string lastLineNew = strList[0] + "$" + balanceNow.ToString();
                    fileContent[fileContent.Count - 1] = lastLineNew;
                    File.WriteAllLines(destPath, fileContent);
                    Console.WriteLine("\n\n\n\t\t\tWithdrawal successful!                                         ");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error:{0}", e);
            }
        }
    }

    public class Statement
    {
        public int accountNumber;
        public int cursorPositionAccountnumberLeft;
        public bool accountExitFlag;
        public string email;

        public void StatementScreen()
        {
            Console.WriteLine("\t\t\t========================================");
            Console.WriteLine("\t\t\t|              STATEMENT               |");
            Console.WriteLine("\t\t\t|======================================|");
            Console.WriteLine("\t\t\t|          ENTER THE DETAILS           |");
            Console.WriteLine("\t\t\t|                                      |");
            Console.Write("\t\t\t|\tAccount number:");
            cursorPositionAccountnumberLeft = Console.CursorLeft;
            Console.Write("\t\t       |\n");
            Console.WriteLine("\t\t\t========================================");
            Console.SetCursorPosition(cursorPositionAccountnumberLeft, 5);
            accountNumber = Account.ReadAccountNo();
            accountExitFlag = Account.AccountExist(accountNumber);
            if (!accountExitFlag)
            {
                Console.WriteLine("\n\n\t\t\tAccount not exist! Please check...            ");
                Console.WriteLine("\t\t\tPlease press ENTER to re-enter another account number...");
                Console.WriteLine("\t\t\tPlease press any other key to return to MAIN MENU...    ");

                ConsoleKeyInfo info = Console.ReadKey(true);
                if (info.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    Statement myStatement = new Statement();
                    myStatement.StatementScreen();
                }
                else if (info.Key != ConsoleKey.Enter)
                {
                    Console.Clear();
                    MainMenu myMainmenu = new MainMenu();
                    myMainmenu.MainMenuScreen();
                }
            }
            else
            {
                Console.WriteLine("\n\n\t\t\tAccount found!\n");
                Console.WriteLine("\t\t\t========================================");
                Console.WriteLine("\t\t\t            ACCOUNT STATEMENT           ");
                Console.WriteLine("\t\t\t========================================");
                Account.DisplayAccount();

                try
                {
                    // Open account file to get email string
                    string fileName = accountNumber.ToString() + ".txt";
                    string destPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
                    var fileContent = File.ReadLines(destPath).ToList();
                    string emailLine = fileContent[fileContent.Count - 2];
                    char separater = ' ';
                    string[] strList = emailLine.Split(separater);
                    email = strList[1];
                    Emial.SendEmail(email, accountNumber);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error:{0}", e);
                }
                

                Console.WriteLine("\t\t\tPlease press ENTER to re-enter another account number...");
                Console.WriteLine("\t\t\tPlease press any other key to return to MAIN MENU...    ");

                ConsoleKeyInfo info = Console.ReadKey(true);
                if (info.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    Statement myStatement = new Statement();
                    myStatement.StatementScreen();
                }
                else if (info.Key != ConsoleKey.Enter)
                {
                    Console.Clear();
                    MainMenu myMainmenu = new MainMenu();
                    myMainmenu.MainMenuScreen();
                }
            }
        }
    }

    public class DeleteAccount
    {
        public int accountNumber;
        public int cursorPositionAccountnumberLeft;
        public bool accountExitFlag;

        public void DeleteAccountScreen()
        {
            Console.WriteLine("\t\t\t========================================");
            Console.WriteLine("\t\t\t|          DELETE AN ACCOUNT           |");
            Console.WriteLine("\t\t\t|======================================|");
            Console.WriteLine("\t\t\t|          ENTER THE DETAILS           |");
            Console.WriteLine("\t\t\t|                                      |");
            Console.Write("\t\t\t|\tAccount number:");
            cursorPositionAccountnumberLeft = Console.CursorLeft;
            Console.Write("\t\t       |\n");
            Console.WriteLine("\t\t\t========================================");
            Console.SetCursorPosition(cursorPositionAccountnumberLeft, 5);
            accountNumber = Account.ReadAccountNo();
            accountExitFlag = Account.AccountExist(accountNumber);

            if (!accountExitFlag)
            {
                Console.WriteLine("\n\n\t\t\tAccount not exist! Please check...            ");
                Console.WriteLine("\t\t\tPlease press ENTER to re-enter another account number...");
                Console.WriteLine("\t\t\tPlease press any other key to return to MAIN MENU...    ");

                ConsoleKeyInfo info = Console.ReadKey(true);
                if (info.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    DeleteAccount myDeleteAccount = new DeleteAccount();
                    myDeleteAccount.DeleteAccountScreen();

                }
                else if (info.Key != ConsoleKey.Enter)
                {
                    Console.Clear();
                    MainMenu myMainmenu = new MainMenu();
                    myMainmenu.MainMenuScreen();
                }
            }
            else
            {
                Console.WriteLine("\n\n\t\t\tAccount found!\n");
                Console.WriteLine("\t\t\t========================================");
                Console.WriteLine("\t\t\t            ACCOUNT  DETAILS            ");
                Console.WriteLine("\t\t\t========================================");
                Account.DisplayAccount();
                Console.WriteLine("\n\n\t\t\tConfirm to delete? Please press ENTER to delete the account...");
                Console.WriteLine("\t\t\tPlease press any other key to return to MAIN MENU...            ");

                ConsoleKeyInfo infoKey = Console.ReadKey(true);
                if (infoKey.Key == ConsoleKey.Enter)
                {
                    try
                    {
                        string fileName = accountNumber.ToString() + ".txt";
                        string destPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
                        File.Delete(destPath);
                        Console.WriteLine("\n\n\t\t\tAccount deleted!                                                 ");
                        Console.WriteLine("\t\t\tPlease press ENTER to re-enter another account number...");
                        Console.WriteLine("\t\t\tPlease press any other key to return to MAIN MENU...    ");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error:{0}", e);
                    }
                    

                    ConsoleKeyInfo info = Console.ReadKey(true);
                    if (info.Key == ConsoleKey.Enter)
                    {
                        Console.Clear();
                        DeleteAccount myDeleteAccount = new DeleteAccount();
                        myDeleteAccount.DeleteAccountScreen();
                    }
                    else if (info.Key != ConsoleKey.Enter)
                    {
                        Console.Clear();
                        MainMenu myMainmenu = new MainMenu();
                        myMainmenu.MainMenuScreen();
                    }
                }
                else if (infoKey.Key != ConsoleKey.Enter)
                {
                    Console.Clear();
                    MainMenu myMainmenu = new MainMenu();
                    myMainmenu.MainMenuScreen();
                }
            }
        }
    }

    public static class Emial
    {
        public static void SendEmail(string email, int accountNumber)
        {
            string fileName = accountNumber.ToString() + ".txt";
            string destPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

            // Get each string line of account file.
            var fileContent = File.ReadLines(destPath).ToList();
            string line1 = fileContent[0];
            string line2 = fileContent[1];
            string line3 = fileContent[2];
            string line4 = fileContent[3];
            string line5 = fileContent[4];
            string line6 = fileContent[5];
            string line7 = fileContent[6];

            // Set MailAddress parameters
            MailAddress to = new MailAddress(email);
            MailAddress from = new MailAddress("niuhy0728@gmail.com");
            MailMessage mail = new MailMessage(from, to);
            mail.Subject = "Simple Banking Statement";
            mail.Body = "========================================" +
                        "\n           BANK  STATEMENT             " +
                        "\n========================================\n" +
                        "\n" + line1 + "\n" + line2 + "\n" + line3 + "\n" + line4 + "\n" +
                        line5 + "\n" + line6 + "\n" + line7;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;

            smtp.Credentials = new NetworkCredential(
                "niuhy0728@gmail.com", "824Nhy5yysqz#!_");
            smtp.EnableSsl = true;
            Console.WriteLine("\t\t\tSending email to " + email + ", please wait...");

            try
            {
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in CreateTestMessage1(): {0}", ex);
            }

        }
    }
}
