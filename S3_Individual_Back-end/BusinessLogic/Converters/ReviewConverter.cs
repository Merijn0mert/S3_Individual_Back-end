using BusinessLogic.Classes;
using Interface.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Converters
{
    public static class ReviewConverter
    {
        public static ReviewDTO toDTO(this Review Model)
        {
            ReviewDTO reviewdto = new ReviewDTO
            (
                Model.ReviewID,
                Model.ProductID,
                Model.UserID,
                Model.ReviewScore,
                Model.ReviewText,
                Model.ReviewDate
            );

            return reviewdto;
        }

        public static Review toModel(this ReviewDTO DTO)
        {
            Review review = new Review
            (
                DTO.ReviewID,
                DTO.ProductID,
                DTO.UserID,
                DTO.ReviewScore,
                DTO.ReviewText,
                DTO.ReviewDate
            );

            return review;
        }
    }
}
