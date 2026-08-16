// يمثل التدريب الموجود في النظام.
public class Training
{
    // المفتاح الأساسي للتدريب.
    public int Id { get; set; }

    // موضوع التدريب.
    public string Topic { get; set; }

    // المفتاح الأجنبي للمدرب المسؤول عن التدريب.
    public int TrainerId { get; set; }

    // المفتاح الأجنبي للشركة المرتبطة بالتدريب.
    public int CompanyId { get; set; }

    // عدد ساعات التدريب.
    public int TotalHours { get; set; }

    // الحد الأقصى لعدد المشاركين.
    public int MaximumParticipants { get; set; }

    // Navigation Property تربط التدريب بالمدرب.
    public Trainer Trainer { get; set; } = null!;

    // Navigation Property تربط التدريب بالشركة.
    public Company Company { get; set; } = null!;

    // التدريب يمكن أن يحتوي على عدة مشاركين.
    public ICollection<TrainingParticipant> TrainingParticipants { get; set; }
        = new List<TrainingParticipant>();

    // التدريب يمكن أن يحتوي على عدة أيام تدريبية.
    public ICollection<TrainingDay> TrainingDays { get; set; }
        = new List<TrainingDay>();

    // التدريب يمكن أن يمتلك عدة مهارات.
    public ICollection<TrainingSkill> TrainingSkills { get; set; }
        = new List<TrainingSkill>();
}