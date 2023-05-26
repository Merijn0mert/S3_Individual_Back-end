using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface.DTO
{
    public class ReviewDTO
    {
        public int ReviewID { get; set; }
        public int ProductID { get; set; }
        public int UserID { get; set; }
        public int ReviewScore { get; set; }
        public string ReviewText { get; set; }
        public DateTime ReviewDate { get; set; }

        public ReviewDTO(int reviewid, int productid, int userid, int score, string text, DateTime date)
        {
            this.ReviewID = reviewid;
            this.ProductID = productid;
            this.UserID = userid;
            this.ReviewScore = score;
            this.ReviewText = text;
            this.ReviewDate = date;
        }

        public ReviewDTO()
        {

        }

    }
}
