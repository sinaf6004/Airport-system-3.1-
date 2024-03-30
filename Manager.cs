using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport_2
{
    [Serializable]
    class Manager
    {
        string UserName;
        string Password;
        public Manager(string username, string password)
        {
            UserName = username;
            Password = password;
        }
    }
}
