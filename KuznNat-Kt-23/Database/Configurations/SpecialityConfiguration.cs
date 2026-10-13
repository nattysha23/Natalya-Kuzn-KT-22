using KuznNat_Kt_23.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using KuznNat_Kt_23.Database.Helpers; // Подключаем ColumnType

namespace KuznNat_Kt_23.Database.Configurations
{
    public class SpecialityConfiguration : IEntityTypeConfiguration<Speciality>
    {
        //Название таблицы, которое будет отображаться в БД
        private const string TableName = "cd_speciality";

        // Весь код пишем внутри этого метода
        public void Configure(EntityTypeBuilder<Speciality> builder)
        {
            //Задаем первичный ключ, это поле автоматически будет обязательным
            builder
                .HasKey(p => p.SpecialityId)
                .HasName($"pk_{TableName}_speciality_id");
                       
            //Расписываем как будут называться колонки в БД, а так же их обязательность и тд
            builder.Property(p => p.SpecialityId)
              .HasColumnName("speciality_id")
              .HasComment("Идентификатор записи специальности")
              .ValueGeneratedOnAdd();//автогенерация ключа

            builder.Property(p => p.Title)
              .IsRequired()
              .HasColumnName("c_title")
              .HasComment("Название специальности");
          
            builder.Property(p => p.Code)
              .HasColumnName("c_code")
              .HasComment("Код специальности");

            builder.ToTable(TableName);

        }
    }
}
