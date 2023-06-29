using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class UserContext : IDbContext<User, int>
    {
        private FlashcardsDbContext context;

        public UserContext(FlashcardsDbContext context)
        {
            this.context = context;
        }

        public async void Create(User item)
        {
            try
            {
                ICollection<Folder> items = new List<Folder>(item.Folders.Count);

                foreach (Folder folder in item.Folders)
                {
                    Folder itemFromDb = await context.Folders.FindAsync(folder.ID);

                    if (itemFromDb != null)
                    {
                        items.Add(itemFromDb);
                    }
                    else
                    {
                        items.Add(folder);
                    }
                }

                item.Folders = items;

                context.Users.Add(item);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<User> Read(int key, bool useNavigationProperties = false)
        {
            try
            {
                IQueryable<User> query = context.Users.AsNoTrackingWithIdentityResolution();

                if (useNavigationProperties)
                {
                    query = query.Include(c => c.Folders);
                }

                User item = await query.SingleOrDefaultAsync(c => c.ID == key);

                if (item == null)
                {
                    throw new ArgumentException("There is no item with that key!");
                }

                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<User>> ReadAll(bool useNavigationProperties = false)
        {
            try
            {
                IQueryable<User> query = context.Users.AsNoTrackingWithIdentityResolution();

                if (useNavigationProperties)
                {
                    query = query.Include(c => c.Folders);
                }

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async void Update(User item, bool useNavigationProperties = false)
        {
            try
            {
                User itemFromDB = await Read(item.ID, useNavigationProperties);

                if (useNavigationProperties)
                {
                    ICollection<Folder> items = new List<Folder>(item.Folders.Count);

                    foreach (Folder folder in item.Folders)
                    {
                        Folder _itemFromDB = await context.Folders.FindAsync(folder.ID);

                        if (_itemFromDB != null)
                        {
                            items.Add(_itemFromDB);
                        }
                        else
                        {
                            items.Add(folder);
                        }
                    }

                    itemFromDB.Folders = items;
                }

                context.Entry(itemFromDB).CurrentValues.SetValues(item);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async void Delete(int key)
        {
            try
            {
                User itemFromDB = await Read(key);

                context.Users.Remove(itemFromDB);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<User>> Get(int skip, int take, bool useNavigationProperties = false)
        {
            try
            {
                IQueryable<User> query = context.Users.AsNoTrackingWithIdentityResolution();

                if (useNavigationProperties)
                {
                    query = query.Include(c => c.Folders);
                }

                return await query.Skip(skip).Take(take).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<User>> Find(object[] args, bool useNavigationProperties = false)
        {
            throw new NotImplementedException();
        }
    }
}
