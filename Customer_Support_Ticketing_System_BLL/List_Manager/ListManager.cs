namespace Customer_Support_Ticketing_System_BLL
{
    public abstract class ListManager<T> : IListManager<T>
    {
        protected List<T> _list; //Protected instance variable of a list
        public int Count => _list.Count; //Property for count 

        protected ListManager()
        {
            _list = new List<T>(); //Creates instance of list
        }

        /// <summary>
        /// Adds an item to the list
        /// </summary>
        /// <param name="item"></param>
        public void Add(T item)
        {
            _list.Add(item);
        }


        /// <summary>
        /// Removes an item from the list
        /// </summary>
        /// <param name="item"></param>
        public void Remove(T item)
        {
            if (_list.Contains(item))
            {
                _list.Remove(item);
            }
        }

        /// <summary>
        /// Replaces an item in the list at index
        /// </summary>
        /// <param name="item"></param>
        /// <param name="index"></param>
        public void ReplaceAt(T item, int index)
        {
            if (index >= 0 && index < _list.Count)
                _list[index] = item;
        }


        /// <summary>
        /// Gets an item from the list at index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T Get(int index)
        {
            if (index >= 0 && index < _list.Count)
                return _list[index];

            else
                return default;
        }

        /// <summary>
        /// Abstract method for converting a string to an array
        /// </summary>
        /// <returns></returns>
        public abstract string[] ToStringArray();
    }
}
