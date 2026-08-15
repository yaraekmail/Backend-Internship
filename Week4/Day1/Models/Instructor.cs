// يمثل المحاضر المسؤول عن تدريس المساقات.
public class Instructor
{
    // المفتاح الأساسي للمحاضر.
    public int Id { get; set; }

    // اسم المحاضر.
    public string Name { get; set; }

    // البريد الإلكتروني للمحاضر.
    // سيتم جعله Unique داخل DbContext.
    public string Email { get; set; }

    // المحاضر يمكن أن يدرّس عدة مساقات.
    public ICollection<Course> Courses { get; set; }
        = new List<Course>();
}