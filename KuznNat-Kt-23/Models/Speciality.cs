namespace KuznNat_Kt_23.Models
{
    public class Speciality
    {
        public int SpecialityId { get; set; }
        public string Title { get; set; }   
        public string Code { get; set; }

        // Связь один-ко-многим: одна специальность -> много групп
        public ICollection<Group> Groups { get; set; } = new List<Group>();

    }
}
