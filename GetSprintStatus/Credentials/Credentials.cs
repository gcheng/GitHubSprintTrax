﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GetSprintStatus.Credentials
{
    class Credentials
    {
        public string Username { get; private set; }
        public string Password { get; private set; }

        public Credentials(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
