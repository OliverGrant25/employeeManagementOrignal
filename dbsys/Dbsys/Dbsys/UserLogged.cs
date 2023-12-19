using Dbsys.AppData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dbsys
{
    public class UserLogged
    {
        private static UserLogged _instance;

        public userAccount UserAccount { get; set; }

        private UserLogged() { }

        public static UserLogged GetInstance()
        {
            if (_instance == null)
                _instance = new UserLogged();
            return _instance;
        }
    }
}
