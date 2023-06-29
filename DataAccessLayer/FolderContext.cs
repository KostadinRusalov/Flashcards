using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class FolderContext : IDbContext<Folder, int>
    {
        private FlashcardsDbContext context;

        public FolderContext(FlashcardsDbContext context)
        {
            this.context = context;
        }

        public async void Create(Folder item)
        {
            try
            {
                ICollection<Flashcard> items = new List<Flashcard>(item.Flashcards.Count);

                foreach (Flashcard flashcard in item.Flashcards)
                {
                    Flashcard itemFromDB = await context.Flashcards.FindAsync(flashcard.ID);

                    if (itemFromDB != null)
                    {
                        items.Add(itemFromDB);
                    }
                    else
                    {
                        items.Add(flashcard);
                    }
                }

                item.Flashcards = items;

                context.Folders.Add(item);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Folder> Read(int key, bool useNavigationProperties = false)
        {
            try
            {
                IQueryable<Folder> query = context.Folders.AsNoTrackingWithIdentityResolution();

                if (useNavigationProperties)
                {
                    query = query.Include(c => c.Flashcards);
                }

                Folder item = await query.SingleOrDefaultAsync(c => c.ID == key);

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

        public async Task<IEnumerable<Folder>> ReadAll(bool useNavigationProperties = false)
        {
            try
            {
                IQueryable<Folder> query = context.Folders.AsNoTrackingWithIdentityResolution();

                if (useNavigationProperties)
                {
                    query = query.Include(c => c.Flashcards);
                }

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async void Update(Folder item, bool useNavigationProperties = false)
        {
            try
            {
                Folder itemFromDB = await Read(item.ID, useNavigationProperties);

                if (useNavigationProperties)
                {
                    ICollection<Flashcard> items = new List<Flashcard>(item.Flashcards.Count);

                    foreach (Flashcard flashcard in item.Flashcards)
                    {
                        Flashcard _itemFromDB = await context.Flashcards.FindAsync(flashcard.ID);

                        if (_itemFromDB != null)
                        {
                            items.Add(_itemFromDB);
                        }
                        else
                        {
                            items.Add(flashcard);
                        }
                    }

                    itemFromDB.Flashcards = items;
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
                Folder itemFromDB = await Read(key);

                context.Folders.Remove(itemFromDB);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Folder>> Get(int skip, int take, bool useNavigationProperties = false)
        {
            try
            {
                IQueryable<Folder> query = context.Folders.AsNoTrackingWithIdentityResolution();

                if (useNavigationProperties)
                {
                    query = query.Include(c => c.Flashcards);
                }

                return await query.Skip(skip).Take(take).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Folder>> Find(object[] args, bool useNavigationProperties = false)
        {
            throw new NotImplementedException();
        }
    }
}
