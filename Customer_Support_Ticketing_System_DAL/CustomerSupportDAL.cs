﻿using Customer_Support_Ticketing_System_DTO;
using Newtonsoft.Json;
using System.IO;

namespace Customer_Support_Ticketing_System_DAL
{
    public class CustomerSupportDAL :ICustomerSupportDAL
    {
        private const string ticketStoragePath = "Sample_Data/ticket_data.json";
        private const string customerStoragePath = "Sample_Data/customer_data.json";

        public bool SaveTickets(List<Ticket> tickets)
        {
            try
            {
                string jsonTickets = JsonConvert.SerializeObject(tickets, Formatting.Indented); //converts to json 
                File.WriteAllText(ticketStoragePath, jsonTickets); //write to file
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
                string jsonCustomers = JsonConvert.SerializeObject(customers, Formatting.Indented); //converts to json 
                File.WriteAllText(customerStoragePath, jsonCustomers); //write to file
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
