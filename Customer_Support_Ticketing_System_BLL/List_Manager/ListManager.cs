using Customer_Support_Ticketing_System_DTO;

namespace Customer_Support_Ticketing_System_BLL
{
    public abstract class ListManager<T> : IListManager<T>
    {
        protected List<T> _list;
        public int Count => _list.Count;

        protected ListManager()
        {
            _list = new List<T>();
        }

        public void Add(T item)
        {
            _list.Add(item);
        }

        public void Remove(T item)
        {
            if (_list.Contains(item))
            {
                _list.Remove(item);
            }
        }

        public void ReplaceAt(T item, int index)
        {
            if (index >= 0 && index < _list.Count)
                _list[index] = item;
        }

        public T Get(int index)
        {
            if (index >= 0 && index < _list.Count)
                return _list[index];

            else
                return default;
        }

        public abstract string[] ToStringArray();
    }
}
