namespace KuznNat_Kt_23.Models
{
    public class Discipline
    {
      public int DisciplineId { get; set; }
      public string Name { get; set; }
      public bool IsDeleted { get; set; }

      // Связь один-ко-многим: одна дисциплина -> много оценок
      public ICollection<Grade> Grades { get; set; } = new List<Grade>();
    }
}
