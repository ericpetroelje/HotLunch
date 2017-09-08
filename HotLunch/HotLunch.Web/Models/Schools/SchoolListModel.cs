using HotLunch.Domain.Library.Schools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotLunch.Web.Models.Schools
{
    public class SchoolListModel
    {
        public SchoolListModel()
        {
            Schools = new List<School>();
        }

        public IList<School> Schools { get; set; }
    }
}