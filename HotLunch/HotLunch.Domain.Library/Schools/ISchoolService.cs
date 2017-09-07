using HotLunch.Domain.Library.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotLunch.Domain.Library.Schools
{
    public enum SchoolValidationError
    {
        NameIsRequired,
        DuplicateSchoolName,
        SchoolDoesNotExist,
    };

    public interface ISchoolService
    {
        CodedResult<SchoolValidationError> CreateSchool(School school);

        CodedResult<SchoolValidationError> UpdateSchool(School school);

        IList<School> GetAllSchools();

        School GetSchool(int id);
    }
}
