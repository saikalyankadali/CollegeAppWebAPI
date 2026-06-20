
using CollegeApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CollegeApp.Data.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly CollegeDBContext _dbContext;

        public StudentRepository(CollegeDBContext dBContext)
        {
            this._dbContext = dBContext;
        }
        public async Task<int> CreateStudentAsync(Student student)
        {
            await _dbContext.Students.AddAsync(student);
            await _dbContext.SaveChangesAsync();
            return student.Id;
        }

        public async Task<bool> DeleteStudentAsync(Student student)
        {
            if (student == null)
            {
                throw new ArgumentNullException($"No Student found with id {student.Id}");
            }
            _dbContext.Students.Remove(student);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Student>> GetAllStudentsAsync()
        {
            return await _dbContext.Students.ToListAsync();
        }

        public async Task<Student> GetStudentByIdAsync(int id, bool useNoTracking = false)
        {
            if (useNoTracking)
            {
                return await _dbContext.Students.AsNoTracking().Where(s => s.Id == id).FirstOrDefaultAsync();
            }
            return await _dbContext.Students.Where(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> UpdateStudentAsync(Student student)
        {
            var existingRecord = await _dbContext.Students.AsNoTracking().Where(stu => stu.Id == student.Id).FirstOrDefaultAsync();
            if (existingRecord == null)
            {
                throw new ArgumentNullException($"No Student found with id {student.Id}");
            }
            existingRecord.Id = student.Id;
            existingRecord.Name = student.Name;
            existingRecord.Email = student.Email;
            existingRecord.Phone = student.Phone;
            existingRecord.DOB = student.DOB;
            _dbContext.Students.Update(student);
            await _dbContext.SaveChangesAsync();
            return student.Id;
        }
    }
}
