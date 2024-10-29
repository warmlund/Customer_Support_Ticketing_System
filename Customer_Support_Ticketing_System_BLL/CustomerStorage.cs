using Customer_Support_Ticketing_System_BLL;
using Customer_Support_Ticketing_System_DTO;

namespace Customer_Support_Customering_System_BLL
{
    /// <summary>
    /// Class storing customers. inherits from class listmanager
    /// </summary>
    public class CustomerStorage : ListManager<Customer>
    {
        public CustomerStorage() : base() { }

        /// <summary>
        /// A method for coverting the list to a string array
        /// </summary>
        /// <returns></returns>
        public override string[] ToStringArray()
        {
            var CustomerArray = new string[_list.Count];

            for (int i = 0; i < _list.Count; i++)
            {
                CustomerArray[i] = _list[i].ToString();
            }
            return CustomerArray;
        }


        /// <summary>
        /// Method that returns all customer ids to list
        /// </summary>
        /// <returns></returns>
        public List<int> GetIds()
        {
            var list = new List<int>();

            foreach (Customer Customer in _list)
            {
                list.Add(Customer.CustomerId);
            }

            return list;
        }

        /// <summary>
        /// Method that returns all customers in a list
        /// </summary>
        /// <returns></returns>
        public List<Customer> GetAllCustomers()
        {
            var list = new List<Customer>();

            foreach (Customer customer in _list)
            {
                list.Add(customer);
            }
            return list;
        }

        /// <summary>
        /// Checks if a customer exists in the list by checking the name through
        /// Linq queries and a lambda expresssion
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool DoesCustomerNameExist(string name)
        {
            if (_list.Any(n => n.Name == name))
                return true;
            return false;
        }

        /// <summary>
        /// Gets the customer by quering the name
        /// </summary>
        /// <param name="customerName"></param>
        /// <returns></returns>
        public Customer GetCustomer(string customerName)
        {
            return _list.First(n => n.Name == customerName);
        }
    }
}
