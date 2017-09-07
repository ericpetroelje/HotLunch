using HotLunch.Domain.Library.Schools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotLunch.Domain.Library.Common;

namespace HotLunch.Domain.Services.Schools
{
    public class SchoolService : ISchoolService
    {
        private readonly ISchoolRepository _schoolRepository;

        public SchoolService(ISchoolRepository schoolRepository)
        {
            _schoolRepository = schoolRepository;
        }

        #region ISchoolService
        public CodedResult<SchoolValidationError> CreateSchool(School school)
        {
            var errors = ValidateSchool(school, false);

            if (!errors.Any())
            {
                _schoolRepository.CreateSchool(school);
            }

            return new CodedResult<SchoolValidationError>(errors);
        }

        public IList<School> GetAllSchools()
        {
            return _schoolRepository.GetAllSchools();
        }

        public School GetSchool(int id)
        {
            return _schoolRepository.GetSchool(id);
        }

        public CodedResult<SchoolValidationError> UpdateSchool(School school)
        {
            var errors = ValidateSchool(school, true);

            if (!errors.Any())
            {
                _schoolRepository.UpdateSchool(school);
            }

            return new CodedResult<SchoolValidationError>(errors);
        }
        #endregion

        #region Validation
        protected IList<SchoolValidationError> ValidateSchool(School school, bool isForUpdate)
        {
            var rval = new List<SchoolValidationError>();

            if (string.IsNullOrWhiteSpace(school.SchoolName))
            {
                rval.Add(SchoolValidationError.NameIsRequired);
            }

            var schoolWithSameName = GetAllSchools().FirstOrDefault(s => s.SchoolName.Equals(school.SchoolName, StringComparison.InvariantCultureIgnoreCase) && s.SchoolID != school.SchoolID);
            if (schoolWithSameName != null)
            {
                rval.Add(SchoolValidationError.DuplicateSchoolName);
            }

            if (isForUpdate)
            {
                var existing = GetSchool(school.SchoolID);
                if (existing == null)
                {
                    rval.Add(SchoolValidationError.SchoolDoesNotExist);
                }
            }

            return rval;
        }
        #endregion
    }
}
