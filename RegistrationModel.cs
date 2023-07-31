using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InternProjectV2.Models
{
    public class RegistrationModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string DOB { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int RegistrationTypeID { get; set; }
        public IList<RegistrationTypeListModel> RegistrationType { get; set; }
        public string ClientName { get; set; }
    }

    public class RegistrationTypeListModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}


