using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interface.DTO;


namespace Interface.IDAL
{
    public interface IUserContainerDAL
    {
        // TODO: Refactor naar UserDTO
        public List<UserDTO> GetAll();
        public UserDTO GetByID(int id);
        public UserDTO AttemptLogin(string email, string password);
        public bool Register(UserDTO userDto);
        //public bool Register(string name, string username, string email, int type, string password);
        public bool ForgotPassword(string email, string password);
        //public bool ForgotPassword(UserDTO userDto);
        public bool ChangePassword(string password, int userID);
        //public bool ChangePassword(UserDTO userDto);
        public bool CreateUser(UserDTO userdto);
    }
}