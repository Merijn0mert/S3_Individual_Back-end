using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interface.IDAL;
using Interface.DTO;
using BusinessLogic.Classes;
using BusinessLogic.Converters;

namespace BusinessLogic.Containers
{
    public class ReviewContainer
    {
        public IReviewContainerDAL reviewcontainerdal { get; }

        public ReviewContainer(IReviewContainerDAL dal)
        {
            this.reviewcontainerdal = dal;
        }
        public Review CreateReview(Review review)
        {
            ReviewDTO reviewdto = reviewcontainerdal.CreateReview(review.toDTO());
            {
                Review reviews = reviewdto.toModel();
                return reviews;
            }
        }

        public List<Review> GetAllProductReviews(int id)
        {
            List<Review> reviews = new List<Review>();
            foreach (ReviewDTO dto in reviewcontainerdal.GetAllProductReviews(id))
            {
                reviews.Add(dto.toModel());
            }
            return reviews;
        }
    }
}
