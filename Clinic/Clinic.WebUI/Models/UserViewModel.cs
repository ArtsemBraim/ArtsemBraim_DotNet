using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Clinic.WebUI.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public List<string> RolesNames { get; set; }

        public List<SelectListItem> Roles { get; set; }
    }
}
