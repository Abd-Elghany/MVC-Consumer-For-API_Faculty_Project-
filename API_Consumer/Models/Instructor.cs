namespace API_Consumer.Models
{
    public class InstructorDTO
    {
        public int Id { get; set; }                     
        public string Name { get; set; }
        public List<string> Courses { get; set; } = new();
    }

    public class InstructorCreateDTO
    {
        public string Name { get; set; }
    }
}
