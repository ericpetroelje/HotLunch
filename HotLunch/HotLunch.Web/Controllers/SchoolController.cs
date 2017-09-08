using HotLunch.Domain.Library.Schools;
using HotLunch.Web.Models.Schools;
using HotLunch.Web.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotLunch.Web.Controllers
{
    public class SchoolController : BaseController
    {
        private readonly ISchoolService _schoolService;

        public SchoolController(ISchoolService schoolService)
        {
            _schoolService = schoolService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var model = new SchoolListModel();
            model.Schools = _schoolService.GetAllSchools();
            return View(model);
        }

        [HttpGet]
        [ImportModelStateFromTempData]
        public ActionResult Create()
        {
            var model = new CreateSchoolModel();
            return View(model);
        }

        [HttpPost]
        [ExportModelStateToTempData]
        public ActionResult Create(CreateSchoolModel model)
        {
            var school = new School()
            {
                SchoolName = model.SchoolName
            };

            var result = _schoolService.CreateSchool(school);
            if (!result.IsSuccess)
            {
                AddModelErrors(result.Errors);
                return RedirectToAction("Create");
            }

            return RedirectToAction("Index");
        }

        #region Helpers
        private void AddModelErrors(IEnumerable<SchoolValidationError> errors)
        {
            foreach( var error in errors)
            {
                switch (error)
                {
                    case SchoolValidationError.DuplicateSchoolName:
                        ModelState.AddModelError("SchoolName", "This school name is already in use.");
                        break;
                    case SchoolValidationError.NameIsRequired:
                        ModelState.AddModelError("SchoolName", "Please enter a name for the school.");
                        break;
                    default:
                        ModelState.AddModelError("", "An unexpected error occurred: " + error.ToString());
                        Logger.ErrorFormat("Unexpected error while creating a school: {0}", error);
                        break;
                }
            }
        }
        #endregion
    }
}