using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using skybeat.Data;

namespace skybeat.Models
{
    public class UserSetting
    {
        private SkybeatContext context;
        public int id_user { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string Verifypassword { get; set; }
        public string email { get; set; }
        public DateTime add_date { get; set; }
        public int id_role { get; set; }

    }
}
