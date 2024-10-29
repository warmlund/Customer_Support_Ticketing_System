using Customer_Support_Ticketing_System_DTO;

namespace Customer_Support_Ticketing_System_BLL
{
    /// <summary>
    /// Class storing tickets. Inherits listmanager
    /// </summary>
    public class TicketStorage : ListManager<Ticket>
    {
        public TicketStorage() : base() { }

        /// <summary>
        /// A method that returns all tickets as a string array
        /// </summary>
        /// <returns></returns>
        public override string[] ToStringArray()
        {
            var TicketArray = new string[_list.Count];

            for (int i = 0; i < _list.Count; i++)
            {
                TicketArray[i] = _list[i].ToString();
            }
            return TicketArray;
        }


        /// <summary>
        /// Returns all ticket ids as a list
        /// </summary>
        /// <returns></returns>
        public List<int> GetIds()
        {
            var list = new List<int>();

            foreach (Ticket Ticket in _list)
            {
                list.Add(Ticket.TicketId);
            }

            return list;
        }

        /// <summary>
        /// Returns all tickets as list
        /// </summary>
        /// <returns></returns>
        public List<Ticket> GetAllTickets()
        {
            var list = new List<Ticket>();

            foreach (Ticket Ticket in _list)
            {
                list.Add(Ticket);
            }
            return list;
        }
    }
}
