using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace skybeat.Models
{
    public class UserSetting
    {
        public int id_user { get; set; }
        public int username { get; set; }
        public int password { get; set; }
        public int email { get; set; }
        public int add_date { get; set; }
        public int id_role { get; set; }

    }
}
