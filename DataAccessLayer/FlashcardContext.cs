using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BusinessLayer;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class FlashcardContext : IDbContext<Flashcard, int>
    {
        private FlashcardsDbContext context;

        public FlashcardContext(FlashcardsDbContext context)
        {
            this.context = context;
        }

        public async void Create(Flashcard item)
        {
            try
            {
                context.Flashcards.Add(item);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Flashcard> Read(int key, bool useNavigationProperties = false)
        {
            try
            {
                IQueryable<Flashcard> query = context.Flashcards.AsNoTrackingWithIdentityResolution();

                Flashcard item = await query.SingleOrDefaultAsync(a => a.ID == key);

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

        public async Task<IEnumerable<Flashcard>> ReadAll(bool useNavigationProperties = false)
        {
            try
            {
                IQueryable<Flashcard> query = context.Flashcards.AsNoTrackingWithIdentityResolution();

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async void Update(Flashcard item, bool useNavigationProperties = false)
        {
            try
            {
                Flashcard itemFromDB = await Read(item.ID);

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
                Flashcard itemFromDB = await Read(key);

                context.Flashcards.Remove(itemFromDB);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Flashcard>> Get(int skip, int take, bool useNavigationProperties = false)
        {
            try
            {
                IQueryable<Flashcard> query = context.Flashcards.AsNoTrackingWithIdentityResolution();

                return await query.Skip(skip).Take(take).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Flashcard>> Find(object[] args, bool useNavigationProperties = false)
        {
            throw new NotImplementedException();
        }
    }
}
