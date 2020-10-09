using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApteanEdgeBank
{
    class Account
    {
        public Account()
        { }
        
        
        public Account(string customerId,string accountNo, int accountType, double balance, double intrestRate, bool status, string openingDate,double maxValue)
        {
            this.customerId = customerId;
            this.accountNo = accountNo;
            this.accountType = accountType;
            this.balance = balance;
            this.intrestRate = intrestRate;
            this.status = status;
            this.openingDate = openingDate;
            this.maxValue = maxValue;
        }        

        public string CustomerId(Account account) // it will return customer id
        {
            return account.customerId;
        }
        public int AccountNo(Account account)   //to get account Number
        {
            return int.Parse(account.accountNo);
        }

        public double Balance(Account account)    //to get the balance
        {
            return account.balance;
        }

        public int AccountType(Account account) // to get Account Type
        {
            return account.accountType;
        }

        public bool AccountStatus(Account account)  // to get the status of account : true for active and false for inactive
        {
            return account.status;
        }


        /// <summary>
        /// // it will deposit money and will return the final balance
        /// </summary>
        /// <param name="account"></param>
        /// <param name="money"></param>
        /// <returns></returns>
        public double Deposit(Account account, double money) 
        {
            if(account.AccountStatus(account) == false)
            {
                Console.WriteLine("this account is Inactive");
            }
            else if(account.accountType == (int)Acctype.customerliabilityaccount) // checking for customer liabilty account
            {
                Console.WriteLine("This is a Customer Liability Account, Hence you can not Deposit Money");
            }
            else
            {
                
                account.balance += money;
                if (account.balance > account.maxValue) // checking if total ammount exceeds the maximum limit of the account
                {
                    Console.WriteLine("You have reached the maximum deposit limit so we can not procees your request");
                    Console.WriteLine("Sorry your request is declined");
                    account.balance -= money;
                }
            }
            return account.balance;
        }


        /// <summary>
        /// it will withdraw money and return status of withdrawl
        /// </summary>
        /// <param name="account"></param>
        /// <param name="money"></param>
        /// <returns></returns>
        public bool Withdraw(Account account, double money)  
        {
            if(account.AccountStatus(account) == false) //checking if account is active or not
            {
                Console.WriteLine("This Account is inactive");
            }
            else if ((account.balance >= money))    // to check wether sufficent funds are there or not
            {
                account.balance -= money;
                return true;
            }
            return false;
        }
        public void UpdateAccountStatus(bool status,Account account)
        {
            account.status = status;
            Console.WriteLine("Account is deactivated");
        }

        /// <summary>
        /// Crediting Loan Ammount
        /// </summary>
        /// <param name="account"></param>
        /// <param name="money"></param>
        public void LoanCredit(Account account, double money)
        {
            //Loan will be credited only in customer liability account
            account.balance += money;
            Console.WriteLine("Loan Ammount Credited successfuly in your account");          
        }


        /// <summary>
        /// Loan Ammount Payment
        /// </summary>
        /// <param name="account"></param>
        /// <param name="money"></param>
        /// <returns></returns>
        public bool LoanDebit(Account account, double money)
        {
            if(account.Balance(account) <= money)        //Checking for loan ammount
            {
                account.balance -= money;
                Console.WriteLine("Loan Payement Done successfuly");
                return true;
            }
            else
            {
                Console.WriteLine("Loan Ammount is lesser then the deposited value");
            }
            return false;
            
        }
        private string customerId;  // it will work as a primary key
        private string accountNo;
        private int accountType;
        private double balance;
        private double intrestRate;
        private bool status;
        public double maxValue;
        public string openingDate;

    }
}
