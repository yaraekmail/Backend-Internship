// يمثل الفصل الدراسي.
public class Semester
{
    // المفتاح الأساسي للفصل الدراسي.
    public int Id { get; set; }

    // اسم الفصل الدراسي.
    public string Name { get; set; }

    // الفصل الدراسي يمكن أن يحتوي على عدة مساقات.
    public ICollection<Course> Courses { get; set; }
        = new List<Course>();
}