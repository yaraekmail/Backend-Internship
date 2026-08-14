using Microsoft.EntityFrameworkCore;

// هذا الـ DbContext هو حلقة الوصل بين تطبيق C# وقاعدة بيانات SQL Server.
public class TrainingManagementDbContext : DbContext
{
    // Constructor يستقبل إعدادات الاتصال بقاعدة البيانات.
    public TrainingManagementDbContext(
        DbContextOptions<TrainingManagementDbContext> options)
        : base(options)
    {
    }

    // كل DbSet يمثل جدولًا في قاعدة البيانات.
    public DbSet<Participant> Participants { get; set; }
    public DbSet<Training> Trainings { get; set; }
    public DbSet<TrainingParticipant> TrainingParticipants { get; set; }
    public DbSet<TrainingDay> TrainingDays { get; set; }
    public DbSet<Trainer> Trainers { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Instructor> Instructors { get; set; }
    public DbSet<University> Universities { get; set; }
    public DbSet<Semester> Semesters { get; set; }
    public DbSet<IslamicGoal> IslamicGoals { get; set; }
    public DbSet<Task> Tasks { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<CourseSkill> CourseSkills { get; set; }
    public DbSet<TrainingSkill> TrainingSkills { get; set; }

    // هنا نحدد إعدادات الجداول والأعمدة والمفاتيح والعلاقات
    // حتى تطابق الـ DBML الخاص بالمشروع.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // =========================
        // Composite Primary Keys
        // =========================

        // المفتاح الأساسي المركب لـ TrainingParticipants.
        modelBuilder.Entity<TrainingParticipant>()
            .HasKey(tp => new { tp.TrainingId, tp.ParticipantId });

        // المفتاح الأساسي المركب لـ CourseSkills.
        modelBuilder.Entity<CourseSkill>()
            .HasKey(cs => new { cs.CourseId, cs.SkillId });

        // المفتاح الأساسي المركب لـ TrainingSkills.
        modelBuilder.Entity<TrainingSkill>()
            .HasKey(ts => new { ts.TrainingId, ts.SkillId });


        // =========================
        // Participants
        // =========================

        // Name = nvarchar(100) NOT NULL
        modelBuilder.Entity<Participant>()
            .Property(p => p.Name)
            .HasMaxLength(100)
            .IsRequired();

        // Email = nvarchar(255) NOT NULL
        modelBuilder.Entity<Participant>()
            .Property(p => p.Email)
            .HasMaxLength(255)
            .IsRequired();

        // Email يجب أن يكون Unique.
        modelBuilder.Entity<Participant>()
            .HasIndex(p => p.Email)
            .IsUnique();


        // =========================
        // Trainings
        // =========================

        // Topic = nvarchar(200) NOT NULL
        modelBuilder.Entity<Training>()
            .Property(t => t.Topic)
            .HasMaxLength(200)
            .IsRequired();


        // =========================
        // Trainers
        // =========================

        // Name = nvarchar(100) NOT NULL
        modelBuilder.Entity<Trainer>()
            .Property(t => t.Name)
            .HasMaxLength(100)
            .IsRequired();


        // =========================
        // Companies
        // =========================

        // Name = nvarchar(150) NOT NULL
        modelBuilder.Entity<Company>()
            .Property(c => c.Name)
            .HasMaxLength(150)
            .IsRequired();


        // =========================
        // Courses
        // =========================

        // CourseCode = nvarchar(50) NOT NULL
        modelBuilder.Entity<Course>()
            .Property(c => c.CourseCode)
            .HasMaxLength(50)
            .IsRequired();

        // CourseCode يجب أن يكون Unique.
        modelBuilder.Entity<Course>()
            .HasIndex(c => c.CourseCode)
            .IsUnique();

        // Name = nvarchar(150) NOT NULL
        modelBuilder.Entity<Course>()
            .Property(c => c.Name)
            .HasMaxLength(150)
            .IsRequired();

        // Description = nvarchar(500) ويمكن أن تكون NULL.
        modelBuilder.Entity<Course>()
            .Property(c => c.Description)
            .HasMaxLength(500);


        // =========================
        // Instructors
        // =========================

        // Name = nvarchar(100) NOT NULL
        modelBuilder.Entity<Instructor>()
            .Property(i => i.Name)
            .HasMaxLength(100)
            .IsRequired();

        // Email = nvarchar(255) NOT NULL
        modelBuilder.Entity<Instructor>()
            .Property(i => i.Email)
            .HasMaxLength(255)
            .IsRequired();

        // Email يجب أن يكون Unique.
        modelBuilder.Entity<Instructor>()
            .HasIndex(i => i.Email)
            .IsUnique();


        // =========================
        // Universities
        // =========================

        // Name = nvarchar(150) NOT NULL
        modelBuilder.Entity<University>()
            .Property(u => u.Name)
            .HasMaxLength(150)
            .IsRequired();


        // =========================
        // Semesters
        // =========================

        // Name = nvarchar(50) NOT NULL
        modelBuilder.Entity<Semester>()
            .Property(s => s.Name)
            .HasMaxLength(50)
            .IsRequired();


        // =========================
        // IslamicGoals
        // =========================

        // Name = nvarchar(150) NOT NULL
        modelBuilder.Entity<IslamicGoal>()
            .Property(g => g.Name)
            .HasMaxLength(150)
            .IsRequired();

        // Description = nvarchar(500) ويمكن أن تكون NULL.
        modelBuilder.Entity<IslamicGoal>()
            .Property(g => g.Description)
            .HasMaxLength(500);

        // StartDate = date.
        modelBuilder.Entity<IslamicGoal>()
            .Property(g => g.StartDate)
            .HasColumnType("date");

        // TargetDate = date ويمكن أن تكون NULL.
        modelBuilder.Entity<IslamicGoal>()
            .Property(g => g.TargetDate)
            .HasColumnType("date");

        // Status = nvarchar(50) NOT NULL
        modelBuilder.Entity<IslamicGoal>()
            .Property(g => g.Status)
            .HasMaxLength(50)
            .IsRequired();


        // =========================
        // Tasks
        // =========================

        // Name = nvarchar(150) NOT NULL
        modelBuilder.Entity<Task>()
            .Property(t => t.Name)
            .HasMaxLength(150)
            .IsRequired();

        // Description = nvarchar(500) ويمكن أن تكون NULL.
        modelBuilder.Entity<Task>()
            .Property(t => t.Description)
            .HasMaxLength(500);

        // StartDate = date ويمكن أن تكون NULL.
        modelBuilder.Entity<Task>()
            .Property(t => t.StartDate)
            .HasColumnType("date");

        // EndDate = date ويمكن أن تكون NULL.
        modelBuilder.Entity<Task>()
            .Property(t => t.EndDate)
            .HasColumnType("date");

        // Status = nvarchar(50) NOT NULL
        modelBuilder.Entity<Task>()
            .Property(t => t.Status)
            .HasMaxLength(50)
            .IsRequired();


        // =========================
        // Skills
        // =========================

        // Name = nvarchar(100) NOT NULL
        modelBuilder.Entity<Skill>()
            .Property(s => s.Name)
            .HasMaxLength(100)
            .IsRequired();

        // Description = nvarchar(500) ويمكن أن تكون NULL.
        modelBuilder.Entity<Skill>()
            .Property(s => s.Description)
            .HasMaxLength(500);


        // =========================
        // TrainingParticipant
        // =========================

        // RegistrationDate = date NOT NULL
        modelBuilder.Entity<TrainingParticipant>()
            .Property(tp => tp.RegistrationDate)
            .HasColumnType("date");

        // Completed = bit NOT NULL DEFAULT 0
        modelBuilder.Entity<TrainingParticipant>()
            .Property(tp => tp.Completed)
            .HasDefaultValue(false);


        // =========================
        // TrainingDay
        // =========================

        // Date = date NOT NULL
        modelBuilder.Entity<TrainingDay>()
            .Property(td => td.Date)
            .HasColumnType("date");

        // StartTime = time NOT NULL
        modelBuilder.Entity<TrainingDay>()
            .Property(td => td.StartTime)
            .HasColumnType("time");

        // EndTime = time NOT NULL
        modelBuilder.Entity<TrainingDay>()
            .Property(td => td.EndTime)
            .HasColumnType("time");
    }
}