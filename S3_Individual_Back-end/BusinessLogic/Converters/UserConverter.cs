using BusinessLogic.Classes;
using Interface.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Converters
{
    public static class UserConverter
    {
        public static UserDTO toDTO(this User Model)
        {
            UserDTO userdto = new UserDTO
            (
                Model.UserID,
                Model.Email,
                Model.Name,
                Model.SurName,
                Model.Password,
                Model.Rolid
            );

            return userdto;
        }

        public static User toModel(this UserDTO DTO)
        {
            User user = new User
            (
                DTO.UserID,
                DTO.Email,
                DTO.Name,
                DTO.SurName,
                DTO.Password,              
                DTO.Rolid
            );

            return user;
        }
    }
}
