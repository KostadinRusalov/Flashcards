using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IDbContext<T, K> where K : IConvertible
    {
        void Create(T item);

        Task<T> Read(K key, bool useNavigationProperties = false);

        Task<IEnumerable<T>> ReadAll(bool useNavigationProperties = false);

        void Update(T item, bool useNavigationProperties = false);

        void Delete(K key);

        Task<IEnumerable<T>> Get(int skip, int take, bool useNavigationProperties = false);

        Task<IEnumerable<T>> Find(object[] args, bool useNavigationProperties = false);
    }
}
