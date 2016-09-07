using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FCGagarin.WebUI.ViewModels
{
    public class User
    {
        public virtual string Name { get; set; }

        public virtual string Password { get; set; }

        public virtual DateTime Birthday { get; set; }

        public virtual IEnumerable<Role> Roles { get; set; }
    }
}