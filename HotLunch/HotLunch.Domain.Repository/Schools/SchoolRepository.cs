using HotLunch.Domain.Library.Schools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotLunch.Domain.Repository.Schools
{
    public class SchoolRepository : RepositoryBase, ISchoolRepository
    {
        public SchoolRepository(string connectionStringName) : base(connectionStringName)
        {

        }

        public void CreateSchool(School school)
        {
            var db = GetDatabase();
            school.SchoolID = (int)db.Insert("School", "SchoolID", true, school);
        }

        public IList<School> GetAllSchools()
        {
            var db = GetDatabase();
            return db.Fetch<School>("SELECT * FROM School");
        }

        public School GetSchool(int id)
        {
            var db = GetDatabase();
            return db.SingleOrDefault<School>("SELECT * FROM School WHERE SchoolID = @0", id);
        }

        public void UpdateSchool(School school)
        {
            var db = GetDatabase();
            db.Update("School", "SchoolID", school);
        }
    }
}
