// يمثل هدفًا إسلاميًا في النظام.
public class IslamicGoal
{
    // المفتاح الأساسي للهدف.
    public int Id { get; set; }

    // اسم الهدف.
    public string Name { get; set; }

    // وصف الهدف، وهو اختياري.
    public string? Description { get; set; }

    // تاريخ بداية الهدف.
    public DateTime StartDate { get; set; }

    // التاريخ المستهدف لإنهاء الهدف، وهو اختياري.
    public DateTime? TargetDate { get; set; }

    // حالة الهدف.
    public string Status { get; set; }

    // الهدف يمكن أن يرتبط بعدة Tasks.
    public ICollection<Task> Tasks { get; set; }
        = new List<Task>();
}