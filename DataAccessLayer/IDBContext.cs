using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer
{
    public interface IDBContext<T, K> where T : IDBEntity where K : IConvertible
    {
        void Create(T item);

        T Read(K key);

        ICollection<T> ReadAll();

        void Update(T item);

        void Delete(K key);

    }
}
