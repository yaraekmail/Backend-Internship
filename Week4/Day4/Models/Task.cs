// يمثل مهمة في النظام.
// يمكن أن تكون مرتبطة بمساق أو تدريب أو هدف إسلامي.
public class Task
{
    // المفتاح الأساسي للمهمة.
    public int Id { get; set; }

    // اسم المهمة.
    public string Name { get; set; }

    // وصف المهمة، وهو اختياري.
    public string? Description { get; set; }

    // تاريخ بداية المهمة، وهو اختياري.
    public DateTime? StartDate { get; set; }

    // تاريخ نهاية المهمة، وهو اختياري.
    public DateTime? EndDate { get; set; }

    // حالة المهمة.
    public string Status { get; set; }

    // Foreign Key اختياري للمساق.
    public int? CourseId { get; set; }

    // Foreign Key اختياري للتدريب.
    public int? TrainingId { get; set; }

    // Foreign Key اختياري للهدف الإسلامي.
    public int? IslamicGoalId { get; set; }

    // Navigation Property للمساق.
    public Course? Course { get; set; }

    // Navigation Property للتدريب.
    public Training? Training { get; set; }

    // Navigation Property للهدف الإسلامي.
    public IslamicGoal? IslamicGoal { get; set; }
}