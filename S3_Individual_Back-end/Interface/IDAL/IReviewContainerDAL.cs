using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interface.DTO;


namespace Interface.IDAL
{
    public interface IReviewContainerDAL
    {
        public ReviewDTO CreateReview(ReviewDTO dto);
        public List<ReviewDTO> GetAllProductReviews(int id);
    }
}
