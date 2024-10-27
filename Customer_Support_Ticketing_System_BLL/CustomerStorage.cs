using Customer_Support_Ticketing_System_BLL;
using Customer_Support_Ticketing_System_DTO;

namespace Customer_Support_Customering_System_BLL
{
    public class CustomerStorage : ListManager<Customer>
    {
        public CustomerStorage() : base() { }
        public override string[] ToStringArray()
        {
            var CustomerArray = new string[_list.Count];

            for (int i = 0; i < _list.Count; i++)
            {
                CustomerArray[i] = _list[i].ToString();
            }
            return CustomerArray;
        }

        public List<int> GetIds()
        {
            var list = new List<int>();

            foreach (Customer Customer in _list)
            {
                list.Add(Customer.CustomerId);
            }

            return list;
        }

        public List<Customer> GetAllCustomers()
        {
            var list = new List<Customer>();

            foreach (Customer customer in _list)
            {
                list.Add(customer);
            }
            return list;
        }

        public bool DoesCustomerNameExist(string name)
        {
            if(_list.Any(n=>n.Name==name))
                return true;
            return false;
        }
    }
}
