namespace KuznNat_Kt_23.Models
{
    public class Grade
    {
        public int GradeId { get; set; }
        public int Value { get; set; }
        public int StudentId { get; set; }// внешний ключ
        public Student Student { get; set; } //навигация,сможем написать, например, Grade.Student.FirstName 
        public int DisciplineId { get; set; }// внешний ключ
        public Discipline Discipline { get; set; } //навигация,сможем написать, например, Grade.Discipline.Name 

    }
}
