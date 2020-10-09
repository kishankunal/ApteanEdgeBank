using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApteanEdgeBank
{
    class Program
    {
        static Bank bank = new Bank();
        static Account account = new Account();
        static Program program = new Program();
        static Customer customer = new Customer();
        static int AccountNo = 20201;
        static void Main(string[] args)
        {
            Customer customer = new customer();
            int mainchoice = bank.bankMenu();
            program.Menu(mainchoice);
            //Startup start = new UserInterface.Startup();
        }

        /// <summary>
        /// handing main displayed menu
        /// </summary>
        /// <param name="mainChoice"></param>
        public void Menu(int mainChoice)
        {
            switch (mainChoice)
            {
                case 1:                                               //Open A Account
                    program.GetDetails();
                    int choice = bank.BankMenu();
                    Menu(choice);
                    break;
                case 2:                                                //View Account
                    Console.WriteLine("Press 1 to enter Account Number\n Press 2 to enter Customer Id");
                    int Choice = int.Parse(Console.ReadLine());
                    string number = Console.ReadLine();
                    Customer user = null;
                    Account usersAccount = null;
                    if(Choice == 1)
                    {
                        usersAccount = bank.GetAccountByAccountNo(number);
                        if(usersAccount != null)                     //checking for wrong details which does not exist
                        {
                            user = bank.GetCustomer(usersAccount.CustomerId(usersAccount));
                        }
                    }
                    else if(Choice == 2)
                    {
                        user = bank.GetCustomer(number);
                        if (user != null)      
                        {
                            usersAccount = bank.GetAccountByCustomerId(number);
                        }
                    }
                    program.displayDetails(user, usersAccount);
                    choice = bank.BankMenu();
                    Menu(choice);
                    break;
                case 3:                                                         //Check Balance
                    Console.WriteLine("Enter Your Account Number");
                    number = Console.ReadLine();
                    usersAccount = bank.GetAccountByAccountNo(number);
                    if (usersAccount == null)                                   //checking for correct account number
                    {
                        Console.WriteLine("Invalid Account Number");
                    }
                    else
                    {
                        Console.WriteLine("Current Balance is :" + usersAccount.Balance(usersAccount));
                    }
                    
                    choice = bank.BankMenu();
                    Menu(choice);
                    break;
                case 4:                                                   //Deposit
                    Console.WriteLine("Enter Your Account Number");
                    number = Console.ReadLine();
                    usersAccount = bank.GetAccountByAccountNo(number);
                    if (usersAccount == null)                             //checking for correct account number
                    {
                        Console.WriteLine("Invalid Account Number");
                    }
                    else
                    {
                        Console.WriteLine("Enter the Ammount You want to Deposit");
                        double money = double.Parse(Console.ReadLine());
                        
                        if (money <= 0)                               //less then 0 cannot be deposited
                        {
                            Console.WriteLine("Invalid Ammount");
                            choice = bank.BankMenu();
                            Menu(choice);
                            break;
                        }
                        if(usersAccount.AccountType(usersAccount) == (int)Acctype.customerliabilityaccount) //not eligible to deposit
                        {
                            Console.WriteLine("You are not allowed to deposit money in Customer Liable Account");
                        }
                        else
                        {
                            usersAccount.Deposit(usersAccount, money);
                            bank.BankBalance += money;                                //Adding money to bank's General Balalnce
                            Console.WriteLine("Current Balance is :" + usersAccount.Balance(usersAccount));
                        }
                        
                    }
                    choice = bank.BankMenu();
                    Menu(choice);
                    break;
                case 5:                                                           //Withdraw
                    Console.WriteLine("Enter Your Account Number");
                    number = Console.ReadLine();
                    usersAccount = bank.GetAccountByAccountNo(number);
                    if (usersAccount == null)                                 //checking for correct account number
                    {
                        Console.WriteLine("Invalid Account Number");
                    }
                    else
                    {
                        Console.WriteLine("Enter the Ammount You want to withdraw");
                        double money = double.Parse(Console.ReadLine());
                        if(money <= 0)                                         //less then 0 cannot be withdraw
                        {
                            Console.WriteLine("Invalid Ammount");
                            choice = bank.BankMenu();
                            Menu(choice);
                            break;
                        }
                        if(usersAccount.Withdraw(usersAccount, money))        //if withdrwan successful it will return true
                        {
                            bank.BankBalance -= money;                            //subtracting money from banks General account Balalnce
                            Console.WriteLine("Withdrwal SuccessFull");
                        }
                        else
                        {
                            Console.WriteLine("Request Declined because of insufficient Balance");
                        }
                        Console.WriteLine("Current Balance is :" + usersAccount.Balance(usersAccount));
                    }
                    choice = bank.BankMenu();
                    Menu(choice);
                    break;
                case 6:                                                 // Update Customer Details
                    Console.WriteLine();
                    int updateChoice = customer.CustomerUpdate();
                    Console.WriteLine("Enter Account Number");
                    number = Console.ReadLine();
                    usersAccount = bank.GetAccountByAccountNo(number);
                    if (usersAccount != null)                                                    // checking input details 
                    {
                        user = bank.GetCustomer(usersAccount.CustomerId(usersAccount));
                        if (usersAccount.AccountStatus(usersAccount) == true)                         //Ony Active Account can be updated
                        {
                            program.updateCustomerDetails(user, updateChoice);
                            Console.WriteLine("Updation SuccessFully");
                        }
                        else
                        {
                            Console.WriteLine("Account is inactive");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid Account Number");
                    }
                    choice = bank.BankMenu();
                    Menu(choice);
                    break;
                case 7:                                                //Deactivating Account
                    Console.WriteLine("Enter Account Number");
                    number = Console.ReadLine();
                    usersAccount = bank.GetAccountByAccountNo(number);
                    if(usersAccount == null)
                    {
                        Console.WriteLine("invalid Account Number");
                    }
                    else
                    {
                        bank.RemoveAccount(usersAccount);
                    }
                    choice = bank.BankMenu();
                    Menu(choice);
                    break;
                case 8:                                                         // Giving Loan
                    Console.WriteLine("Enter the Account Number");
                    number = Console.ReadLine();
                    usersAccount = bank.GetAccountByAccountNo(number);
                    if (usersAccount == null)
                    {
                        Console.WriteLine("invalid Account Number");
                    }
                    else
                    {
                        Console.WriteLine("Enter the Loan Ammount");
                        double loanAmmount = double.Parse(Console.ReadLine());
                        if(loanAmmount <= bank.BankBalance)
                        {
                            if(usersAccount.AccountType(usersAccount) == (int)Acctype.customerliabilityaccount) // only this account is eleigible
                            {
                                usersAccount.LoanCredit(usersAccount, loanAmmount);
                                bank.BankBalance -= loanAmmount;                                 //loan is given from banks general account
                            }
                            else
                            {
                                Console.WriteLine("Account is not eligible for loan. Apply for customer liability Account");
                            }
                            
                        }
                        else
                        {
                            Console.WriteLine("Insufficient Funds in Bank leadger");
                        }
                        
                    }
                    choice = bank.BankMenu();
                    Menu(choice);
                    break;
                case 9:                                                  //Get Customer List
                    Console.WriteLine("***************"+bank.name+" Customer List ********************");
                    List<Customer> customers= bank.GetCustomersList();
                    foreach(Customer customer in customers)
                    {
                        Console.WriteLine("Name : " + customer.Name(customer) + " Customer id : " + customer.CustomerId(customer));
                    }
                    Console.WriteLine("******************End of page************************");
                    choice = bank.BankMenu();
                    Menu(choice);
                    break;
                case 10:                                                           //Banks General Account
                    Console.WriteLine("Enter the Bank's General Account Number");
                    number = Console.ReadLine();
                    if(number == bank.BankGenralAccountNumber)
                    {
                        Console.WriteLine("Total Funds in the bank Leadger "+ bank.BankBalance);
                    }
                    else
                    {
                        Console.WriteLine("Invalid input details");
                    }
                    choice = bank.BankMenu();
                    Menu(choice);
                    break;
                case 11:                                                               //Loan Payment
                    Console.WriteLine("Enter the Loan Account Number");
                    number = Console.ReadLine();
                    usersAccount = bank.GetAccountByAccountNo(number);
                    if (usersAccount == null)
                    {
                        Console.WriteLine("invalid Account Number");
                    }
                    else
                    {
                        if (usersAccount.AccountType(usersAccount) == (int)Acctype.customerliabilityaccount) 
                        {
                            Console.WriteLine("Enter the Payment Ammount");
                            double loanAmmount = double.Parse(Console.ReadLine());
                            if(usersAccount.LoanDebit(usersAccount, loanAmmount))
                            {
                                bank.BankBalance += loanAmmount;                       //Loan Payment is added to banks general account
                            }           
                        }
                        else
                        {
                            Console.WriteLine("No Loan is pending on this Account");
                        }
                    }
                    choice = bank.BankMenu();
                    Menu(choice);
                    break;
                case 12:
                    Environment.Exit(0);
                    return;
                default:
                    Console.WriteLine("Invalid Input!! Enter again");
                    choice = bank.BankMenu();
                    Menu(choice);
                    break;
            }
        }
        /// <summary>
        /// it will update the customer details
        /// </summary>
        /// <param name="user"></param>
        /// <param name="updateChoice"></param>
        private void updateCustomerDetails(Customer user, int updateChoice)
        {
            if(updateChoice == 1)
            {
                Console.WriteLine("Enter the Name to be updated");
                string name = Console.ReadLine();
                user.UpdateName(user, name);
            }
            else if(updateChoice == 2)
            {
                Console.WriteLine("Enter the Address to be updated");
                string address = Console.ReadLine();
                user.UpdateAddress(user, address);
            }
            else if(updateChoice == 3)
            {
                Console.WriteLine("Enter The Date of Birth to be updated");
                string dob = Console.ReadLine();
                user.UpdateDOB(user, dob);
            }
            else if(updateChoice == 4)
            {
                Console.WriteLine("Enter the Phone Number to be updated");
                string phone = Console.ReadLine();
                user.UpdatePhoneNumber(user, phone);
            }
            else
            {
                Console.WriteLine("Invalid Input");
            }
        }
        /// <summary>
        /// This will display the data of given account along with its customers details
        /// </summary>
        /// <param name="user"></param>
        /// <param name="usersAccount"></param>
        private void displayDetails(Customer user, Account usersAccount) // it will dipaly the details of users and its account
        {
            if(user == null || usersAccount == null)
            {
                Console.WriteLine("User does not exist");
            }
            else if (usersAccount.AccountStatus(usersAccount) == false)
            {
                Console.WriteLine("Your Account is Inactive");
            }
            else
            {
                string accountType = null;
                if (usersAccount.AccountType(usersAccount) == (int)Acctype.chequingaccount)
                {
                    accountType = "chequing account";
                }
                else if (usersAccount.AccountType(usersAccount) == (int)Acctype.taxfreesavingsaccount)
                {
                    accountType = "tax free savings account";
                }
                else
                {
                    accountType = "customer liability account";
                }
                string status = null;
                if (usersAccount.AccountStatus(usersAccount))
                {
                    status = "Active";
                }
                else
                {
                    status = "Inactive";
                }

                Console.WriteLine("**********************************"+bank.name+"**************************************");
                Console.WriteLine("IFSC CODE : "+bank.IFSC + "                 "+bank.address + "         Tel: " + bank.telephoneNo);
                Console.WriteLine();
                Console.WriteLine("Name :" + user.Name(user) + "       Customer Id :" + user.CustomerId(user) );
                Console.WriteLine("Address :" + user.Address(user) + "       Date Of Birth :" + user.DOB(user));
                Console.WriteLine("Phone Number :" + user.PhoneNumber(user) + "       Account Number :" + usersAccount.AccountNo(usersAccount));
                Console.WriteLine("Account Type : " + accountType+"     Balance : " + usersAccount.Balance(usersAccount));
                Console.WriteLine("Account Status : " + status + "     Date of Opening : " + usersAccount.openingDate);
                Console.WriteLine("***************************End Of Page*******************************************************");
            }
        }
        /// <summary>
        /// This will take input from user to create a new account
        /// </summary>
        public void GetDetails()
        {
            DateTime Date = DateTime.Now;
            Console.WriteLine("Enter your Name");
            string name = Console.ReadLine();
            Console.WriteLine("Enter your Adress");
            string address = Console.ReadLine();
            string accountNo = AccountNo.ToString(); // creating Account Number
            AccountNo++;
            string customerId = bank.BankDetails().IFSC + accountNo; //Creating CustomerId
            Console.WriteLine("Enter Your Phone Number");
            string phone = Console.ReadLine();  // it should consist of numbers only
            Console.WriteLine("Enter your date of birth");
            string dob = Console.ReadLine();
            Console.WriteLine("Enter the Account Type");
            Console.WriteLine("\nPress 1 for Chequing Account\nPress 2 for Tax Free Account \nPress 3 for Customer Liability Account");
            int acctype = int.Parse(Console.ReadLine());
            string DateOfOpen;
            double maxLimit;
            double intrestRate = 5;
            if(acctype == 1)
            {
                acctype = (int)Acctype.chequingaccount;
                DateOfOpen = Date.ToString("dddd, dd MMMM yyyy HH:mm:ss");
                maxLimit = Int32.MaxValue;
            }
            else if(acctype == 2)
            {
                acctype = (int)Acctype.taxfreesavingsaccount;
                DateOfOpen = Date.ToString("dddd, dd MMMM yyyy HH:mm:ss");
                maxLimit = 500000;
            }
            else               // it will deal with the loan
            {
                acctype = (int)Acctype.customerliabilityaccount;
                DateOfOpen = "";
                maxLimit = Int32.MaxValue;
            }
            double balance = 0;
            bank.AddAccount(customerId, accountNo, acctype, balance, intrestRate, true, DateOfOpen, maxLimit);
            bank.AddCustomer(customerId,name,address,dob,phone);
            Console.WriteLine("Congrats, we have successfully created your account");
            Console.WriteLine("Your Account Number is : " + accountNo);
            Console.WriteLine("Your Unique customer id is : " +customerId);
            Console.WriteLine("Please Note it for further refrence\n");
        }
    }

    /// <summary>
    /// It will take care of the account type
    /// </summary>
    public enum Acctype
    {
        chequingaccount = 1,
        taxfreesavingsaccount = 2,
        customerliabilityaccount = 3
    }
}
