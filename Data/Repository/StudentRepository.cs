
using CollegeApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CollegeApp.Data.Repository
{
    public class StudentRepository : CollegeRepository<Student>, IStudentRepository
    {
        private readonly CollegeDBContext _dbContext;

        public StudentRepository(CollegeDBContext dBContext) : base(dBContext)
        {
            this._dbContext = dBContext;
        }

        public async Task<Student> GetStudentByFeeStatusAsync(int id)
        {
            //Write code to get the students filtered by fee status
            return null;
        }
    }
}
