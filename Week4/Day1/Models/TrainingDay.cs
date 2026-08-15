// يمثل يومًا من أيام التدريب.
public class TrainingDay
{
    // المفتاح الأساسي ليوم التدريب.
    public int Id { get; set; }

    // المفتاح الأجنبي للتدريب.
    public int TrainingId { get; set; }

    // تاريخ يوم التدريب.
    public DateTime Date { get; set; }

    // وقت بداية التدريب.
    public TimeSpan StartTime { get; set; }

    // وقت نهاية التدريب.
    public TimeSpan EndTime { get; set; }

    // Navigation Property للتدريب.
    public Training Training { get; set; } = null!;
}