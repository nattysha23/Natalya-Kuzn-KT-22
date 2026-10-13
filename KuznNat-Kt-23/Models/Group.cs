namespace KuznNat_Kt_23.Models
{
    public class Group
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
        public int Course { get; set; }  // Курс
        public int SpecialityId { get; set; }  // внешний ключ
        public Speciality Speciality { get; set; } // навигационное свойство, через это свойство мы можем написать group.Speciality.Title и сразу получить названия
        public bool IsDeleted { get; set; }

        // Связь один-ко-многим: одна группа -> много студентов
        public ICollection<Student> Students { get; set; } = new List<Student>();


    }
}
