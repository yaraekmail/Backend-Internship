// يمثل الجامعة.
public class University
{
    // المفتاح الأساسي للجامعة.
    public int Id { get; set; }

    // اسم الجامعة.
    public string Name { get; set; }

    // الجامعة يمكن أن تحتوي على عدة مساقات.
    public ICollection<Course> Courses { get; set; }
        = new List<Course>();
}