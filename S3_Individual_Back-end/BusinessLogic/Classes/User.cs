using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Classes
{
        public class User
        {
            public int UserID { get;}
            public string Email { get; set; }        
            public string Name { get; set; }
            public string SurName { get; set; }
            public string Password { get; }         
            public int Rolid { get; }

            public User(int userId, string email, string name, string surname, string password, int rolid)
            {
                this.UserID = userId; 
                this.Email = email;
                this.Name = name;
                this.SurName = surname;
                this.Password = password;
                this.Rolid = rolid;
            }
            public User(int userid, string surname, string name, string email, int rolid)
            {
                this.UserID = userid;
                this.SurName = surname;
                this.Name = name;
                this.Email = email;
                this.Rolid = rolid;
            }
            public User( string surname, string name, string email, int rolid)
            {
                this.SurName = surname;
                this.Name = name;
                this.Email = email;
                this.Rolid = rolid;
            }

            public User(int id)
            {
                UserID = id;
            }
            public User()
            {

            }
        }
}
