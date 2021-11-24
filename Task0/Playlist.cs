using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task0
{
    public class Playlist<T> : IEnumerable<T>
    {
        private ICollection<T> _items;
        public void Add(T item)
        {

        }
        public void Remove(T item)
        {

        }

        public IEnumerator<T> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_items).GetEnumerator();
        }
    }
}