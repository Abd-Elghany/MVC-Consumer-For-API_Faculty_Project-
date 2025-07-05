namespace API_Consumer.Models
{
    public class CourseDTO
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Instructor { get; set; }
    }

    public class CourseCreateDTO
    {
        public string Name { get; set; }
        public int InstructorId { get; set; }
    }
}
