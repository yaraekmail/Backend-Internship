// يمثل علاقة Many-to-Many بين Course و Skill.
// هذا الجدول يعمل كـ Join Entity.
public class CourseSkill
{
    // المفتاح الأجنبي للمساق.
    public int CourseId { get; set; }

    // المفتاح الأجنبي للمهارة.
    public int SkillId { get; set; }

    // Navigation Property للمساق.
    public Course Course { get; set; } = null!;

    // Navigation Property للمهارة.
    public Skill Skill { get; set; } = null!;

    // CourseId + SkillId يشكلان Composite Primary Key.
    // سيتم تحديده داخل DbContext.
}