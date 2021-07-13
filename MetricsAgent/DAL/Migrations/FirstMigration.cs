using FluentMigrator;

namespace MetricsAgent.DAL.Migrations
{
    [Migration(1)]
    public class FirstMigration : Migration
    {
        public override void Up()
        {
            CreateTable("cpumetrics");
            CreateTable("hddmetrics");
            CreateTable("dotnetmetrics");
            CreateTable("networkmetrics");
            CreateTable("rammetrics");
        }

        public override void Down()
        {
            Delete.Table("cpumetrics");
            Delete.Table("hddmetrics");
            Delete.Table("dotnetmetrics");
            Delete.Table("networkmetrics");
            Delete.Table("rammetrics");
        }

        private void CreateTable(string tableName)
        {
            Create.Table(tableName)
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Value").AsInt64()
                .WithColumn("Time").AsInt64();
        }
    }
}
