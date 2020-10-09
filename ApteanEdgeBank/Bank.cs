using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApteanEdgeBank
{
    class Bank
    {
        public Bank()
        {
            IFSC = "APT7610";
            name = "Aptean Edge Bank";
            address = "Manjunath Nagar, Rajaji Nagar Bangalore";
            telephoneNo = "(654)-530-9876";
            accounts = new List<Account>();
            customers = new List<Customer>();
            AddAccount("ApteanEdgeBank", BankGenralAccountNumber,(int) Acctype.chequingaccount, BankBalance, 5, true, "null", Int32.MaxValue); //Creating bank's General Account
        }
        /// <summary>
        /// this will return general bank details
        /// </summary>
        /// <returns></returns>
        public Bank BankDetails() 
        {
            return new Bank();
        }
        
        /// <summary>
        /// this is menu to be displayed at the start and it will return the choice entered by the user
        /// </summary>
        /// <returns></returns>
        public int BankMenu()  
        {
            Console.WriteLine("Press 1 For adding a Account\nPress 2 For Viewing Your Account details\nPress 3 for checking Balance");
            Console.WriteLine("Press 4 for Depositing money\nPress 5 For Withdrawl money \nPress 6 For Customer Details Updation");
            Console.WriteLine("press 7 for Deleting your Account \nPress 8 for applying loan \nPress 9 for all Customer details ");
            Console.WriteLine("press 10 For check Banks General Account Balance \nPress 11 For Loan Repayment \nPress 12 For Trminating the Program");
            int choice = int.Parse(Console.ReadLine());
            return choice;
        }
        /// <summary>
        /// this will return all customer list
        /// </summary>
        /// <returns></returns>
        public List<Customer> GetCustomersList() 
        {
            return customers;
        }

        /// <summary>
        /// this will return account list
        /// </summary>
        /// <returns></returns>
        public List<Account> GetAccountsList() 
        {
            return accounts;
        }

        /// <summary>
        /// this will return Accounts object when we know the customer id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Account GetAccountByCustomerId(string id) 
        {
            foreach (Account account in accounts)
            {
                if (account.CustomerId(account).ToString() == id)
                {
                    return account;
                }
            }
            return null;  // if id is wrong it will return null
        }

        /// <summary>
        /// this will return customers object when we have customer id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Customer GetCustomer(string id)  // this will rturn customer object by only entering id
        {
            
            foreach (Customer customer in customers)
            {
                if (customer.CustomerId(customer).ToString() == id)
                {
                    return customer;
                }
            }
            return null;    // if the id is not there in the list it will return null
        }

        /// <summary>
        /// this will return the account details when we have account number
        /// </summary>
        /// <param name="accountNo"></param>
        /// <returns></returns>
        public Account GetAccountByAccountNo(string accountNo) // this will return object by entering accountNo;
        {
            foreach (Account account in accounts)
            {
                if (account.AccountNo(account).ToString() == accountNo)
                {
                    return account;
                }
            }
            return null; // if account number is wrong it will return null
        }
        /// <summary>
        /// Adding new Account
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="accountNo"></param>
        /// <param name="accountType"></param>
        /// <param name="balance"></param>
        /// <param name="intrestRate"></param>
        /// <param name="status"></param>
        /// <param name="openingDate"></param>
        /// <param name="maxValue"></param>

        public void AddAccount(string customerId, string accountNo, int accountType,
                                 double balance, double intrestRate, bool status, string openingDate,double maxValue)  //adding new Account
        {
            accounts.Add( new Account(customerId, accountNo, accountType, balance, intrestRate,status,openingDate,maxValue));
        }
        
        /// <summary>
        /// Adding a new customer
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="name"></param>
        /// <param name="address"></param>
        /// <param name="dob"></param>
        /// <param name="phoneNumber"></param>
        public void AddCustomer(string customerId, string name, string address, string dob, string phoneNumber)  //Create New Customer
        {
            customers.Add(new Customer(customerId, name, address, dob, phoneNumber));
        }
        /// <summary>
        /// Deactivating account
        /// </summary>
        /// <param name="account"></param>
        public void RemoveAccount(Account account)   //it will turn the account status to inactive
        {
            if(account.Balance(account) == 0)
            {
                if(account.AccountType(account) == (int)Acctype.customerliabilityaccount) // customer liability Account
                {
                    Console.WriteLine("This is a customer liability Account. So, it cannot be closed");
                }
                else
                {
                    account.UpdateAccountStatus(false, account);
                }
            }
            else
            {
                Console.WriteLine("There are some Funds left in your account so its cannot be deactivated");
            }
        }
        /// <summary>
        /// This will activate the deactivated account
        /// </summary>
        /// <param name="account"></param>
        public void ActivateAccount(Account account)   //it will turn the accunt status to active
        {
            account.UpdateAccountStatus(true, account);
        }


        public string IFSC;
        public string name;
        public string address;
        public string telephoneNo;
        public string BankGenralAccountNumber = "2020";
        public double BankBalance = 0;
        private List<Account> accounts;
        private List<Customer> customers;
    }

}
