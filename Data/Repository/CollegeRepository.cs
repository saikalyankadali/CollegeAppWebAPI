using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace CollegeApp.Data.Repository
{
    public class CollegeRepository<T> : ICollegeRepository<T> where T : class
    {
        private readonly CollegeDBContext _dbContext;
        private DbSet<T> dbSet;

        public CollegeRepository(CollegeDBContext dBContext)
        {
            this._dbContext = dBContext;
            this.dbSet = dBContext.Set<T>();
        }
        public async Task<T> CreateStudentAsync(T dbRecord)
        {
            await dbSet.AddAsync(dbRecord);
            await _dbContext.SaveChangesAsync();
            return dbRecord;
        }

        public async Task<bool> DeleteStudentAsync(T dbRecord)
        {
            dbSet.Remove(dbRecord);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<T>> GetAllStudentsAsync()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<T> GetStudentByIdAsync(Expression<Func<T, bool>> filter, bool useNoTracking = false)
        {
            if (useNoTracking)
            {
                return await dbSet.AsNoTracking().Where(filter).FirstOrDefaultAsync();
            }
            return await dbSet.Where(filter).FirstOrDefaultAsync();
        }

        public async Task<T> UpdateStudentAsync(T dbRecord)
        {
            dbSet.Update(dbRecord);
            await _dbContext.SaveChangesAsync();
            return dbRecord;
        }
    }
}
