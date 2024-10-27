namespace Customer_Support_Ticketing_System_DTO
{
    public class Customer
    {
        public int CustomerId { get; set; }  // Unique identifier for the customer
        public string Name { get; set; } // Customer's first name

        public Customer()
        {
            CustomerId = 0;
            Name=String.Empty;
        }
    }
}
