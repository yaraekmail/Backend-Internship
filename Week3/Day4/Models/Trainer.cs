// يمثل المدرب في النظام.
public class Trainer
{
    // المفتاح الأساسي للمدرب.
    public int Id { get; set; }

    // اسم المدرب.
    public string Name { get; set; }

    // المدرب يمكن أن يكون مسؤولًا عن عدة تدريبات.
    public ICollection<Training> Trainings { get; set; }
        = new List<Training>();
}