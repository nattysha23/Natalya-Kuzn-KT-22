namespace KuznNat_Kt_23.Database.Helpers
{
    public class ColumnType
    {

        // Типы данных для Microsoft SQL Server (MSSQL)
        public const string Date = "datetime";
        public const string Guid = "uniqueidentifier";  // Уникальный идентификатор
        public const string String = "nvarchar(max)";
        public const string Text = "text";
        public const string Bool = "bit";
        public const string Int = "int";              // Вместо int4 из методички
        public const string Long = "bigint";      // Вместо int8 из методички
        public const string Decimal = "money";
        public const string Double = "numeric(9,2)";

    }
}
