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
    public class ProductOrderContainer
    {
        private readonly string outlookAddress = "kaarsenwinkel@outlook.com";
        private readonly string outlookPassword = "kaarswinkel1234";
        public IProductOrderContainerDAL prodordDAL { get; }

        public ProductOrderContainer(IProductOrderContainerDAL dal)
        {
            this.prodordDAL = dal;
        }

        public bool AddProductToOrder(List<ProductOrder> prodorder, User user)
        {
            int id = 0;
            foreach (ProductOrder prodor in prodorder)
            {
                prodordDAL.AddProductToOrder(prodor.toDTO());
                id = prodor.OrderID;
            }
            SendConfirmationMail(user.Email , user.Name, id);


            return true;
            

        }
        public List<Product> GetAllOrderProduct(int id)
        {
            List<Product> products = new List<Product>();


            foreach (ProductDTO productdto in prodordDAL.GetAllOrderProduct(id))
            {
                products.Add(productdto.toModel());
            }
            
            return products;
        }

        private void SendConfirmationMail(string email, string name, int orderid)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Je hebt de volgende producten besteld <br/>");
            foreach (Product product in GetAllOrderProduct(orderid))
            {               

                sb.AppendFormat("<br/>{0}  &nbsp;&nbsp; €{1}", product.ProductName, product.Price);
            }

            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();

            message.From = new MailAddress(outlookAddress);
            message.To.Add(new MailAddress(email));
            message.Subject = $"Bevestiging bestelling: {orderid}";
            message.IsBodyHtml = true;
            message.Body =
                $"<div><h3>Bedankt voor de bestelling op onze website, {name}!</h3></div>" +
                $"<div><p>Je bestelnummer is: {orderid}!</p>" +
                $"<div><p>{sb}</p></div>" +
                "<div><p>Groetjes, Team Kaarsenwinkel. </p></div>";


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
