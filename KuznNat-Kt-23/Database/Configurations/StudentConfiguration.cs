using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using KuznNat_Kt_23.Models;
using KuznNat_Kt_23.Database.Helpers; // Подключаем ColumnType


namespace KuznNat_Kt_23.Database.Configurations
{
    public class StudentConfiguration: IEntityTypeConfiguration<Student>
    {
        //Название таблицы, которое будет отображаться в БД
        private const string TableName = "cd_student";
        
        // Весь код пишем внутри этого метода
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            //Задаем первичный ключ
            builder
                  .HasKey(p => p.StudentId)
                 .HasName($"pk_{TableName}_student_id"); // Имя ключа в БД для единообразия

            //Для целочисленного первичного ключа задаем автогенерацию (к каждой новой записи будет добавлять +1)
            builder
                 .Property(p => p.StudentId)
                 .ValueGeneratedOnAdd(); // ключ генерируется только при новой записи, при восстановлении студена (смене флага IsDeleted) ID не увеличивается
                                         //Расписываем как будут называться колонки в БД, а так же их обязательность и тд
            builder.Property(p => p.StudentId)
                .HasColumnName("student_id")
                .HasComment("Идентификатор записи студента");  //HasComment добавит комментарий, который будет отображаться в СУБД (добавлять по желанию)

            builder.Property(p => p.FirstName)
                .IsRequired() // NOT NULL
                .HasColumnName("c_student_firstname") // Название колонки в БД
                .HasColumnType(ColumnType.String).HasMaxLength(100) // Используем наш хелпер, и еще добавляем ограничение длины
                .HasComment("Имя студента"); //комментарий к колонке в СУБД

            builder.Property(p => p.LastName)
                .IsRequired()
                .HasColumnName("c_student_lastname")
                .HasColumnType(ColumnType.String).HasMaxLength(100)
                .HasComment("Фамилия студента");
            //про это поле нет в методичке, добавим сами
            builder.Property(p => p.IsDeleted)
              .HasColumnName("f_is_deleted")
              .HasColumnType(ColumnType.Bool)
              .HasDefaultValue(false) // По умолчанию студент активен
              .HasComment("Флаг мягкого удаления");

            //поле-связь с группой
            builder.Property(p => p.GroupId)
                 .IsRequired()
                 .HasColumnName("f_group_id")
                 .HasColumnType(ColumnType.Int)
                 .HasComment("Идентификатор группы");
            // создание самой связи
            builder
                .HasOne(p => p.Group) // У одного студента есть одна группа
                .WithMany(g => g.Students)    // А у группы много студентов   
                .HasForeignKey(p => p.GroupId)  // Через внешний ключ GroupId
                .HasConstraintName("fk_f_group_id")
                // .OnDelete(DeleteBehavior.Cascade); - если нужно каскадное удаление всех студентов при удалении группы, так в методичке
                .OnDelete(DeleteBehavior.Restrict); // Запрет на физическое удаление группы -  мы делаем так, потому что есть поле isDeleted
                                                    // Создаём индекс по полю внешнего ключа (GroupId) на стороне таблицы Students
            builder
                 .HasIndex(p => p.GroupId, $"idx_{TableName}_fk_f_group_id");


            //Добавим явную автоподгрузку связанной сущности
            builder
                  .Navigation(p => p.Group).AutoInclude();

            // Назначение имени таблицы (один раз за весь блок)
            builder.ToTable(TableName);

        }


    }
}
