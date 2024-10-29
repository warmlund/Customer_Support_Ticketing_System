using Customer_Support_Ticketing_System_DTO;
using Newtonsoft.Json;

namespace Customer_Support_Ticketing_System_DAL
{
    public class CustomerSupportDAL : ICustomerSupportDAL
    {
        //strings for paths to the sample json files.
        private static readonly string projectRoot = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.Parent.FullName;
        private static readonly string ticketStoragePath = Path.Combine(projectRoot, "Sample_Data", "ticket_data.json");
        private static readonly string customerStoragePath = Path.Combine(projectRoot, "Sample_Data", "customer_data.json");

        public bool SaveTickets(List<Ticket> tickets)
        {
            try
            {
                // Ensure the directory exists
                Directory.CreateDirectory(Path.GetDirectoryName(ticketStoragePath));

                // Serialize tickets to JSON format
                string jsonTickets = JsonConvert.SerializeObject(tickets, Formatting.Indented);

                // Write JSON data to the file
                File.WriteAllText(ticketStoragePath, jsonTickets);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SaveCustomers(List<Customer> customers)
        {
            try
            {
                // Ensure the directory exists
                Directory.CreateDirectory(Path.GetDirectoryName(customerStoragePath));

                // Serialize customers to JSON format
                string jsonCustomers = JsonConvert.SerializeObject(customers, Formatting.Indented);

                // Write JSON data to the file
                File.WriteAllText(customerStoragePath, jsonCustomers);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<Ticket> LoadTickets()
        {
            try
            {
                if (File.Exists(ticketStoragePath)) //Checks if file exists
                {
                    string jsonPlaylist = File.ReadAllText(ticketStoragePath); //Reads the json file
                    return JsonConvert.DeserializeObject<List<Ticket>>(jsonPlaylist); //returns the deserialized json file as a playlist
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public List<Customer> LoadCustomers()
        {
            try
            {
                if (File.Exists(customerStoragePath)) //Checks if file exists
                {
                    string jsonPlaylist = File.ReadAllText(customerStoragePath); //Reads the json file
                    return JsonConvert.DeserializeObject<List<Customer>>(jsonPlaylist); //returns the deserialized json file as a playlist
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
