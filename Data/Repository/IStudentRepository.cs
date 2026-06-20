namespace CollegeApp.Data.Repository
{
    public interface IStudentRepository : ICollegeRepository<Student>
    {
        Task<Student> GetStudentByFeeStatusAsync(int id);
    }
}
