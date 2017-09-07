using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotLunch.Domain.Library.Schools
{
    public interface ISchoolRepository
    {
        void CreateSchool(School school);

        void UpdateSchool(School school);

        IList<School> GetAllSchools();

        School GetSchool(int id);
    }
}
