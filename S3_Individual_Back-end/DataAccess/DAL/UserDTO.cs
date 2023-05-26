﻿using Interface.DTO;
using Interface.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataAccess.DAL
{
    // Een record heeft geen individueel object en constructors nodig.
    // Bij aanmaken hoeven de waardes niet meer veranderd te worden, goed voor een SQL verwerking.
    public class UserDTO
    {
        public int UserID { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }       
        public string Password { get; set; }      
        public int Rolid { get; set; }

        public UserDTO(int userid, string surname, string name, string email, int rolid)
        {
            this.UserID = userid;
            this.SurName = surname;
            this.Name = name;
            this.Email = email;
            this.Rolid = rolid;
        }

        public UserDTO(int userID, string email, string name, string surname, string password, int rolid)
        {
            UserID = userID;
            Email = email;          
            Name = name;
            SurName = surname;
            Password = password;          
            Rolid = rolid;
        }
        public UserDTO()
        {

        }
    }
}

