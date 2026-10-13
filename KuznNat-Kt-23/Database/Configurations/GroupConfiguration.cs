using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using KuznNat_Kt_23.Models;
using KuznNat_Kt_23.Database.Helpers; // Подключаем ColumnType

namespace KuznNat_Kt_23.Database.Configurations
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        private const string TableName = "cd_group";

        public void Configure(EntityTypeBuilder<Group> builder)
        {
            //сюда пишем
            //Задаем первичный ключ, это поле автоматически будет обязательным
            builder
                .HasKey(p => p.GroupId)
                .HasName($"pk_{TableName}_group_id");

            //Для целочисленного первичного ключа задаем автогенерацию (к каждой новой записи будет добавлять +1)
            builder.Property(p => p.GroupId)
                    .ValueGeneratedOnAdd();

            //Расписываем как будут называться колонки в БД, а так же их обязательность и тд
            builder.Property(p => p.GroupId)
                .HasColumnName("group_id")
                .HasComment("Идентификатор записи группы"); //HasComment добавит комментарий, который будет отображаться в СУБД (добавлять по желанию)

            builder.Property(p => p.Name)
                .IsRequired() //обязательное поле, Not null
                .HasColumnName("c_group_name")
                .HasColumnType(ColumnType.String).HasMaxLength(100)
                .HasComment("Название группы");

            // Поле Course 
            builder.Property(p => p.Course)
                .HasColumnName("c_group_course")
                .HasColumnType(ColumnType.Int); // Тип Int из нашего хелпера

            //*************
            //поле-связь со специальностью
            builder.Property(p => p.SpecialityId)
                 .IsRequired()
                 .HasColumnName("f_specality_id")
                 .HasColumnType(ColumnType.Int)
                 .HasComment("Идентификатор специальности");
            // создание самой связи
            builder
                .HasOne(p => p.Speciality) // У одной группы есть одна специальность
                .WithMany(g => g.Groups)    // А у специальности много групп   
                .HasForeignKey(p => p.SpecialityId)  // Через внешний ключ SpecialityId
                .HasConstraintName("fk_f_speciality_id")
                 .OnDelete(DeleteBehavior.Cascade); // каскадное удаление всех групп при удалении специальности
                //.OnDelete(DeleteBehavior.Restrict) -  если нужен запрет на удаление

            builder
                 .HasIndex(p => p.SpecialityId, $"idx_{TableName}_fk_f_specality_id"); //для ускорения поиска

            //Добавим явную автоподгрузку связанной сущности
            builder
                  .Navigation(p => p.Speciality).AutoInclude(); // если специальность нужна редко, то можно убрать
            //*********

            // Мягкое удаление
            builder.Property(p => p.IsDeleted)
                .HasColumnName("f_is_deleted") // Префикс f_ для булевых флагов
                .HasColumnType(ColumnType.Bool)
                .HasDefaultValue(false) // По умолчанию группа активна
                .HasComment("Флаг мягкого удаления");
            


            builder.ToTable(TableName);

        }
    }
}
