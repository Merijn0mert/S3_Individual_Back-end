﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Classes
{
    public class Login
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public Login(string email, string password)
        {
            this.Email = email;
            this.Password = password;

        }
        public Login()
        {

        }

    }
}
