using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interface.IDAL;
using Interface.DTO;
using BusinessLogic.Classes;
using BusinessLogic.Converters;
using System.Net.Mail;
using System.Net;

namespace BusinessLogic.Containers
{
    public class UserContainer
    {
        // TODO: Vind een veiligere manier dan dit
        private readonly string outlookAddress = "kaarsenwinkel@outlook.com";
        private readonly string outlookPassword = "kaarswinkel1234";

        public IUserContainerDAL UserContainerDal { get; }

        public UserContainer(IUserContainerDAL dal)
        {
            this.UserContainerDal = dal;
        }

        // Get all users
        public List<User> GetAll()
        {
            List<User> users = new List<User>();

            foreach (UserDTO dto in UserContainerDal.GetAll())
            {
                users.Add(GetNewUser(dto));
            }

            return users;
        }

        // Get user by UserID
        public User GetByID(int id)
        {
            User users = UserContainerDal.GetByID(id).toModel();

            return users;
        }

        // Maak een nieuwe gebruiker aan
        private User GetNewUser(UserDTO userDTO)
        {
            if (userDTO == null) return new User();
            return new User(userDTO.UserID, userDTO.SurName, userDTO.Name, userDTO.Email, userDTO.Password, userDTO.Rolid);
        }
        private User GetNewUsernullpass(UserDTO userDTO)
        {
            if (userDTO == null) return new User();
            return new User(userDTO.UserID, userDTO.SurName, userDTO.Name, userDTO.Email, userDTO.Rolid);
        }

        // Als User bestaat doe AttemptLogin()
        public User AttemptLogin(string email, string password)
        {
            User user = GetNewUsernullpass(this.UserContainerDal.AttemptLogin(email, password));

            return user;
        }

        // Stuur nieuw wachtwoord
        public bool ForgotPassword(string email)
        {
            string generatedPassword = UpdateGetRandomPassword();
            bool changepass = UserContainerDal.ForgotPassword(email, generatedPassword);
            if (changepass)
            {
                SendLostPasswordMail(email, generatedPassword);
                return true;
            }
            return false;
        }

        public string ChangePassword(string Password, int userID)
        {
            bool succes = UserContainerDal.ChangePassword(Password, userID);

            // TODO: ???
            if (succes) 
            { 
                return null; 
            }
            else return "Couldn't change Password";
        }

        // Genereer een nieuw wachtwoord van 20 tekens
        private string UpdateGetRandomPassword()
        {
            Random rnd = new Random();
            string characters = "abcdefghijklmnopqrstuvwxyz1234567890!@#$%&*+=ABCDEFGHIJKLMNOPQRSTUVW";
            string psw = "";

            for (int i = 0; i < 20; i++)
            {
                int index = rnd.Next(characters.Length);
                psw += characters[index];

            }
            return psw.Trim();
        }

        public bool RegisterAdminUser(User user)
        {
            
            string randomPassword = UpdateGetRandomPassword();
            UserDTO userdto = user.toDTO();
            userdto.Password = randomPassword;
            
            if (UserContainerDal.CreateUser(userdto))
            {
                SendAdminRegistrationMail(userdto.Email, randomPassword, userdto.Name);
                return true;
            }
            return false;
        }

        public bool CreateUser(User user)
        {
            if (UserContainerDal.CreateUser(user.toDTO()))
            {
                SendRegistrationMail(user.Email, user.Name);
                return true;
            }
            return false;
        }
        private void SendAdminRegistrationMail(string email, string generatedPassword, string name)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();

            message.From = new MailAddress(outlookAddress);
            message.To.Add(new MailAddress(email));
            message.Subject = "Kaarsenwinkel - Welkom";
            message.IsBodyHtml = true;
            message.Body =
                $"<div><h3>Welkom bij Kaarsenwinkel, {name}!</h3></div>" +
                "<div><p>We hebben een account voor je aangemaakt bij laarsenwinkel.nl</p>" +
                "<p>Hier is je tijdelijke wachtwoord. We verzoeken je deze zo snel mogelijk te veranderen</p></div>" +
                $"<div><p>Username: {email} </p> <p>Password: {generatedPassword}</p></div>" +
                "<div><h5>Groetjes, Team Kaarsenwinkel.</h5></div>";

            smtp.Port = 587;
            smtp.Host = "smtp.office365.com";
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(outlookAddress, outlookPassword); // CREDENTIALS
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

            try
            {
                smtp.Send(message);
            }
            catch { };
        }

        private void SendRegistrationMail(string email, string name)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();

            message.From = new MailAddress(outlookAddress);
            message.To.Add(new MailAddress(email));
            message.Subject = "Kaarsenwinkel - Welkom";
            message.IsBodyHtml = true;
            message.Body =
                $"<div><h3>Welkom bij Kaarsenwinkel, {name}!</h3></div>" +
                "<div><p>Je hebt je geregistreerd. Je kan vanaf nu inloggen.</p>" +
                "<p>Ik wens je veel winkelplezier.</p></div>" +
                "<div><h5>Groetjes, Team Kaarsenwinkel.</h5></div>";

            smtp.Port = 587;
            smtp.Host = "smtp.office365.com";
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(outlookAddress, outlookPassword); // CREDENTIALS
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

            try
            {
                smtp.Send(message);
            }
            catch { };
        }

        private void SendLostPasswordMail(string email, string generatedPassword)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            message.From = new MailAddress(outlookAddress);
            message.To.Add(new MailAddress(email));
            message.Subject = "Kaarsenwinkel - Wachtwoord vergeten";
            message.IsBodyHtml = true;

            message.Body =
                $"<div><h3>Kaarsenwinkel</h3></div>" +
                "<div><p>Hierbij uw tijdelijke wachtwoord aangezien de vorige zoekgeraakt is.</p>" +
                "<p>Na het inloggen vragen wij je om je wachtwoord te veranderen.</p></div>" +
                $"<div><p>Email: {email}</p> <p>Wachtwoord: {generatedPassword} (tijdelijk)</p></div>" +
                "<div><h5>Groetjes, Team Kaarsenwinkel.</h5></div>";


            smtp.Port = 587;
            smtp.Host = "smtp.office365.com";
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(outlookAddress, outlookPassword); // CREDENTIALS
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            try
            {
                smtp.Send(message);
            }
            catch { };
        }
    }
}