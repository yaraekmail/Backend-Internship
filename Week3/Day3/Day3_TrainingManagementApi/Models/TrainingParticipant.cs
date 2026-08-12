// يمثل علاقة تسجيل مشارك في تدريب.
// هذا الجدول يعمل كـ Join Entity بين Training و Participant.
public class TrainingParticipant
{
    // المفتاح الأجنبي للتدريب.
    public int TrainingId { get; set; }

    // المفتاح الأجنبي للمشارك.
    public int ParticipantId { get; set; }

    // تاريخ تسجيل المشارك في التدريب.
    public DateTime RegistrationDate { get; set; }

    // يحدد هل أكمل المشارك التدريب أم لا.
    public bool Completed { get; set; }

    // Navigation Property للتدريب.
    public Training Training { get; set; } = null!;

    // Navigation Property للمشارك.
    public Participant Participant { get; set; } = null!;

    // TrainingId + ParticipantId يشكلان Composite Primary Key.
    // سيتم تحديده داخل DbContext.
}