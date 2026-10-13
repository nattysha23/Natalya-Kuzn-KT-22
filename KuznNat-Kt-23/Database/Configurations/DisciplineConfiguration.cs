using KuznNat_Kt_23.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using KuznNat_Kt_23.Database.Helpers; // Подключаем ColumnType
namespace KuznNat_Kt_23.Database.Configurations
{
    public class DisciplineConfiguration: IEntityTypeConfiguration<Discipline>
    {

        //Название таблицы, которое будет отображаться в БД
        private const string TableName = "cd_discipline";

        // Весь код пишем внутри этого метода
        public void Configure(EntityTypeBuilder<Discipline> builder)
        {
            //Задаем первичный ключ, это поле автоматически будет обязательным
            builder
                .HasKey(p => p.DisciplineId)
                .HasName($"pk_{TableName}_discipline_id");

            //Расписываем как будут называться колонки в БД, а так же их обязательность и тд
            builder.Property(p => p.DisciplineId)
              .HasColumnName("discipline_id")
              .HasComment("Идентификатор записи дисциплины")
              .ValueGeneratedOnAdd();//автогенерация ключа

            builder.Property(p => p.Name)
              .IsRequired()
              .HasColumnName("c_name")
              .HasComment("Название дисциплины");

            builder.Property(p => p.IsDeleted)
              .HasColumnName("f_is_deleted")
              .HasColumnType(ColumnType.Bool)
              .HasDefaultValue(false) // По умолчанию активен
              .HasComment("Флаг мягкого удаления");

            builder.ToTable(TableName);

        }
    }
}
