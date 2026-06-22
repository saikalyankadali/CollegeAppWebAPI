namespace CollegeApp.Data
{
    public class UserType
    {
        public int Id { get; set; }
        public string UserTypeName { get; set; }
        public string Description { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
