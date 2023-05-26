using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Classes
{
    public class Review
    {
        public int ReviewID { get; }
        public int ProductID { get; }
        public int UserID { get; }
        public int ReviewScore { get; set; }
        public string ReviewText { get; set; }
        public DateTime ReviewDate { get; set; }

        public Review(int reviewid, int productid, int userid, int score, string text, DateTime date)
        {
            this.ReviewID = reviewid;
            this.ProductID = productid;
            this.UserID = userid;
            this.ReviewScore = score;
            this.ReviewText = text;
            this.ReviewDate = date;
        }
    }
}
