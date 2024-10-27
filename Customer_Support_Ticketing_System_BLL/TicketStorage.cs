using Customer_Support_Ticketing_System_DTO;

namespace Customer_Support_Ticketing_System_BLL
{
    public class TicketStorage : ListManager<Ticket>
    {
        public TicketStorage() : base() { }
        public override string[] ToStringArray()
        {
            var TicketArray = new string[_list.Count];

            for (int i = 0; i < _list.Count; i++)
            {
                TicketArray[i] = _list[i].ToString();
            }
            return TicketArray;
        }

        public List<int> GetIds()
        {
            var list = new List<int>();

            foreach (Ticket Ticket in _list)
            {
                list.Add(Ticket.TicketId);
            }

            return list;
        }

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
