// يمثل المشارك في نظام إدارة التدريبات.
public class Participant
{
    // المفتاح الأساسي للمشارك.
    public int Id { get; set; }

    // اسم المشارك.
    public string Name { get; set; }

    // البريد الإلكتروني للمشارك.
    // سيتم جعله Unique داخل DbContext.
    public string Email { get; set; }

    // العلاقة: المشارك يمكن أن يسجل في عدة تدريبات.
    public ICollection<TrainingParticipant> TrainingParticipants { get; set; }
        = new List<TrainingParticipant>();
}