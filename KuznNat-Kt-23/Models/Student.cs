namespace KuznNat_Kt_23.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }  // Имя
        public string LastName { get; set; }  // Фамилия
        public int GroupId { get; set; } //внешний ключ
        public Group Group { get; set; } //навигация к группе, мы сможем написать Student.Group.Name и сразу получить название
        public bool IsDeleted { get; set; }

        // Связь один-ко-многим: один студент -> много оценок
        public ICollection<Grade> Grades { get; set; } = new List<Grade>();


    }
}
