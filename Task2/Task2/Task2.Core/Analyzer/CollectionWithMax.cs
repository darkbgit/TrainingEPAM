using System;
using System.Collections;
using System.Collections.Generic;
using Task2.Core.Model.Interfaces;

namespace Task2.Core.Analyzer
{
    internal class CollectionWithMax<T> : IEnumerable<T>
    {
        private readonly ICollection<T> _collection = new List<T>();

        private readonly int _max;

        public CollectionWithMax(int max)
        {
            _max = max;
        }

        internal void Add(T item)
        {
            if (_collection.Count >= _max)
            {
                var skipped = string.Join(string.Empty, _collection);
                _collection.Clear();
                _collection.Add(item);

                throw new ArgumentOutOfRangeException(typeof(T).Name, $"Too big. \"{skipped}\" skipped");
            }
            _collection.Add(item);
        }

        public void Clear()
        {
            _collection.Clear();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_collection).GetEnumerator();
        }
    }
}