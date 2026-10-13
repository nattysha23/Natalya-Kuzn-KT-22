using KuznNat_Kt_23.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using KuznNat_Kt_23.Database.Helpers; // Подключаем ColumnType
namespace KuznNat_Kt_23.Database.Configurations
{
    public class GradeConfiguration: IEntityTypeConfiguration<Grade>
    {
        //Название таблицы, которое будет отображаться в БД
        private const string TableName = "cd_grade";

        // Весь код пишем внутри этого метода
        public void Configure(EntityTypeBuilder<Grade> builder)
        {
            //Задаем первичный ключ, это поле автоматически будет обязательным
            builder
                .HasKey(p => p.GradeId)
                .HasName($"pk_{TableName}_grade_id");

            //Расписываем как будут называться колонки в БД, а так же их обязательность и тд
            builder.Property(p => p.GradeId)
              .HasColumnName("grade_id")
              .HasComment("Идентификатор записи оценки")
              .ValueGeneratedOnAdd();//автогенерация ключа

            builder.Property(p => p.Value)
              .IsRequired()
              .HasColumnName("c_value")
              .HasComment("оценка");

            //поле-связь со студентами
            builder.Property(p => p.StudentId)
                 .IsRequired()
                 .HasColumnName("f_student_id")
                 .HasColumnType(ColumnType.Int)
                 .HasComment("Идентификатор студента");
            // создание самой связи
            builder
                .HasOne(p => p.Student) // У одного студента
                .WithMany(g => g.Grades)   // много оценок   
                .HasForeignKey(p => p.StudentId)  // через внешний ключ StudentId
                .HasConstraintName("fk_f_student_id")
                // .OnDelete(DeleteBehavior.Cascade); - если нужно каскадное удаление всех студентов при удалении группы, так в методичке
                .OnDelete(DeleteBehavior.Restrict); // Запрет на физическое удаление группы -  мы делаем так, потому что есть поле isDeleted
                                                    // Создаём индекс по полю внешнего ключа (GroupId) на стороне таблицы Students
            builder
                 .HasIndex(p => p.StudentId, $"idx_{TableName}_fk_f_student_id");

            //Добавим явную автоподгрузку связанной сущности
            builder
                  .Navigation(p => p.Student).AutoInclude();

            //поле-связь с дисциплинами
            builder.Property(p => p.DisciplineId)
                 .IsRequired()
                 .HasColumnName("f_discipline_id")
                 .HasColumnType(ColumnType.Int)
                 .HasComment("Идентификатор дисциплины");
            // создание самой связи
            builder
                .HasOne(p => p.Discipline) // У одной дисциплины
                .WithMany(g => g.Grades)   // много оценок   
                .HasForeignKey(p => p.DisciplineId)  // через внешний ключ DisciplineId
                .HasConstraintName("fk_f_discipline_id")
                // .OnDelete(DeleteBehavior.Cascade); - если нужно каскадное удаление всех студентов при удалении группы, так в методичке
                .OnDelete(DeleteBehavior.Restrict); // Запрет на физическое удаление группы -  мы делаем так, потому что есть поле isDeleted
                                                    // Создаём индекс по полю внешнего ключа (GroupId) на стороне таблицы Students
            builder
                 .HasIndex(p => p.DisciplineId, $"idx_{TableName}_fk_f_discipline_id");

            //Добавим явную автоподгрузку связанной сущности
            builder
                  .Navigation(p => p.Discipline).AutoInclude();


            builder.ToTable(TableName);

        }
    }
}
