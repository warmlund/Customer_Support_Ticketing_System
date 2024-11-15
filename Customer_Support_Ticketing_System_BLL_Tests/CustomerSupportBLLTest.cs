using Customer_Support_Customering_System_BLL;
using Customer_Support_Ticketing_System_DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Customer_Support_Ticketing_System_BLL;

namespace Customer_Support_Ticketing_System_BLL_Tests
{
    public class CustomerSupportBLLTest
    {
        private CustomerSupportBLL customerSupport;

        [SetUp]
        public void Setup()
        {
            customerSupport = new CustomerSupportBLL();

            //Adding sample data
            customerSupport.CStorage.Add(new Customer { CustomerId = 1, Name = "John Doe" });
            customerSupport.CStorage.Add(new Customer { CustomerId = 2, Name = "Jane Smith" });
            customerSupport.CStorage.Add(new Customer { CustomerId = 3, Name = "Bob Johnson" });
            customerSupport.CStorage.Add(new Customer { CustomerId = 5, Name = "Alice Brown" });
        }

        [Test]
        public void CreateCustomerIdTest()
        {
            //Act
             int result=customerSupport.CreateCustomerId();

            //Assert: Should return 4 because the next free id number is five in the sample data
            Assert.That(result, Is.EqualTo(4), "CreateCustomerId should return 4 because it is the next available Id after the sample data.");
        }
    }
}
