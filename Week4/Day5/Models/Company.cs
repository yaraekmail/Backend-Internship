// يمثل الشركة المرتبطة بالتدريبات.
public class Company
{
    // المفتاح الأساسي للشركة.
    public int Id { get; set; }

    // اسم الشركة.
    public string Name { get; set; }

    // الشركة يمكن أن تكون مرتبطة بعدة تدريبات.
    public ICollection<Training> Trainings { get; set; }
        = new List<Training>();
}