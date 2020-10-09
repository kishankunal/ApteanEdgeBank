using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ApteanEdgeBank
{
    class Customer
    {
        public Customer()
        {  }
        
        public Customer(string customerId, string name, string address, string dob, string phoneNumber)
        {
            this.customerId = customerId;
            this.name = name;
            this.address = address;
            this.dob = dob;          //make condition on date of birth
            this.phoneNumber = phoneNumber;
            
        }
        /// <summary>
        /// This will dispaly the customer update menu and return the choice of user
        /// </summary>
        /// <returns></returns>

        public int CustomerUpdate()   //Customer details Update menu
        {
            Console.WriteLine("Press 1 to update your Name\nPress 2 to Update your address\nPress 3 to update Date of Birth");
            Console.WriteLine("Press 4 to Update your Phone Number\n press any other key to go to back menu");
            int choice = int.Parse(Console.ReadLine());
            return choice;
        }
        
        public string CustomerId(Customer customer) // it will return the customer id
        {
            return customer.customerId;
        }

        public string Name(Customer customer) // it will return the name
        {
            return customer.name;
        }

        public string Address(Customer customer) // it will return the address
        {
            return customer.address;
        }

        public string DOB(Customer customer) // it will return customers date of birth
        {
            return customer.dob;
        }

        public string PhoneNumber(Customer customer) // it will return the phone number of customer
        {
            return customer.phoneNumber;
        }

        public void UpdateName(Customer customer, string name)  //update name
        {
            customer.name = name;
        }

        public void UpdateAddress(Customer customer, string address) // update address
        {
            customer.address = address;
        }

        public void UpdateDOB(Customer customer, string dob) //update date of birth
        {
            customer.dob = dob; 
        }

        public void UpdatePhoneNumber(Customer customer, string phoneNumber) //update phone number
        {
            customer.phoneNumber = phoneNumber;
        }
       

        private string customerId; //  it will work as a primary key
        private string name;
        private  string address;
        private string dob;
        private string phoneNumber;
        
    }
}
