using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;
using Cql.FluentMigrator.Extensions;

namespace HotLunch.Database.Migrations.Migrations._2017._09
{
	[Migration(20170907022440)]
	public class _20170907022440_alter_school : Migration
	{
		public override void Up()
		{
            Alter.Table("School").InSchema("dbo").AddColumn("IsActive").AsBoolean().WithDefaultValue(true);
		}

		public override void Down()
		{
			
		}
	}
}
