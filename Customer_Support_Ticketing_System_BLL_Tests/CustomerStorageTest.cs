using Customer_Support_Customering_System_BLL;
using Customer_Support_Ticketing_System_DTO;

namespace Customer_Support_Ticketing_System_BLL_Tests
{
    public class CustomerStorageTest
    {
        private CustomerStorage customerStorage;

        [SetUp]
        public void Setup()
        {
            customerStorage = new CustomerStorage();

            //Adding sample data
            customerStorage.Add(new Customer { CustomerId = 1, Name = "John Doe" });
            customerStorage.Add(new Customer { CustomerId = 2, Name = "Jane Smith" });
        }

        [Test]
        public void GetCustomerTest()
        {
            //Arrange: initiates a customer name that exists in the sample data
            string customerName = "John Doe";

            //Act
            Customer testCustomer = customerStorage.GetCustomer(customerName);

            //Assert: Should not return null because the customer exists
            Assert.IsNotNull(testCustomer, "Result should not be null");
        }

        [Test]
        public void DoesCustomerNameExistTest()
        {
            //Arrange: initiates a customer name that does not exist
            string customerName = "Test Customer";

            //Act
            bool result = customerStorage.DoesCustomerNameExist(customerName);

            //Assert: Should return false because customer doesn't exist
            Assert.That(result, Is.False, "DoesCustomerNameExist should return false when calling a name of customer that doesn't exist");
        }
    }
}