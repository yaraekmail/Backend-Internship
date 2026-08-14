// يمثل مهارة يمكن ربطها بمساق أو تدريب.
public class Skill
{
    // المفتاح الأساسي للمهارة.
    public int Id { get; set; }

    // اسم المهارة.
    public string Name { get; set; }

    // وصف المهارة، وهو اختياري.
    public string? Description { get; set; }

    // المهارة يمكن أن ترتبط بعدة مساقات.
    public ICollection<CourseSkill> CourseSkills { get; set; }
        = new List<CourseSkill>();

    // المهارة يمكن أن ترتبط بعدة تدريبات.
    public ICollection<TrainingSkill> TrainingSkills { get; set; }
        = new List<TrainingSkill>();
}