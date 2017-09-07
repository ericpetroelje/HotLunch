using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;
using Cql.FluentMigrator.Extensions;

namespace HotLunch.Database.Migrations.Migrations._2017._09
{
	[Migration(20170907015247)]
	public class _20170907015247_create_school : Migration
	{
		public override void Up()
		{
            Create.Table("School").InSchema("dbo")
                .WithColumn("SchoolID").AsInt32().NotNullable().Identity().PrimaryKey("PK_School")
                .WithColumn("SchoolName").AsString().NotNullable();
		}

		public override void Down()
		{
			
		}
	}
}
