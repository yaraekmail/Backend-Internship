// يمثل علاقة Many-to-Many بين Training و Skill.
// هذا الجدول يعمل كـ Join Entity.
public class TrainingSkill
{
    // المفتاح الأجنبي للتدريب.
    public int TrainingId { get; set; }

    // المفتاح الأجنبي للمهارة.
    public int SkillId { get; set; }

    // Navigation Property للتدريب.
    public Training Training { get; set; } = null!;

    // Navigation Property للمهارة.
    public Skill Skill { get; set; } = null!;

    // TrainingId + SkillId يشكلان Composite Primary Key.
    // سيتم تحديده داخل DbContext.
}