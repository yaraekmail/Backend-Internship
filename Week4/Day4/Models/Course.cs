// يمثل المساق الجامعي في النظام.
public class Course
{
    // المفتاح الأساسي للمساق.
    public int Id { get; set; }

    // رمز المساق، ويجب أن يكون Unique.
    public string CourseCode { get; set; }

    // اسم المساق.
    public string Name { get; set; }

    // وصف المساق، وهو اختياري.
    public string? Description { get; set; }

    // عدد الساعات المعتمدة.
    public int CreditHours { get; set; }

    // المفتاح الأجنبي للمحاضر.
    public int InstructorId { get; set; }

    // المفتاح الأجنبي للجامعة.
    public int UniversityId { get; set; }

    // المفتاح الأجنبي للفصل الدراسي.
    public int SemesterId { get; set; }

    // Navigation Property للمحاضر.
    public Instructor Instructor { get; set; } = null!;

    // Navigation Property للجامعة.
    public University University { get; set; } = null!;

    // Navigation Property للفصل الدراسي.
    public Semester Semester { get; set; } = null!;

    // المساق يمكن أن يحتوي على عدة Tasks.
    public ICollection<Task> Tasks { get; set; }
        = new List<Task>();

    // المساق يمكن أن يمتلك عدة مهارات.
    public ICollection<CourseSkill> CourseSkills { get; set; }
        = new List<CourseSkill>();
}