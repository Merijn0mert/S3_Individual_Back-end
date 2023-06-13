using Interface.DTO;
using Interface.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Npgsql;

namespace DataAccess.DAL
{
    public class ReviewDAL : IReviewContainerDAL
    {
        private string _connectionString = "Server=mssqlstud.fhict.local;Database=dbi432217_kaarsen;User Id=dbi432217_kaarsen;Password=kaarsen;";

        public ReviewDTO CreateReview(ReviewDTO dto)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
                try
                {
                    conn.Open();
                    string query = "INSERT INTO [productreview](productid, userid, reviewscore, reviewtext, reviewdate)" +
                                   "VALUES (@productid, @userid, @reviewscore, @reviewtext, getdate());" +
                                   "SELECT SCOPE_IDENTITY();";
                    NpgsqlCommand command = new NpgsqlCommand(query, conn);

                    command.Parameters.AddWithValue("@productid", dto.ProductID);
                    command.Parameters.AddWithValue("@userid", dto.UserID);
                    command.Parameters.AddWithValue("@reviewscore", dto.ReviewScore);
                    command.Parameters.AddWithValue("@reviewtext", (dto.ReviewText == null ? "" : dto.ReviewText));


                    int g = Convert.ToInt32(command.ExecuteScalar());
                    ReviewDTO review = new ReviewDTO();
                    review.ReviewID = g;

                    return review;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
        }

        public List<ReviewDTO> GetAllProductReviews(int id)
        {
            List<ReviewDTO> reviews = new List<ReviewDTO>();

            try
            {
                const string sql = "SELECT * FROM [productreview] WHERE productid = @ID";


                using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
                {
                    using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@ID", id);

                        NpgsqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            ReviewDTO review = new ReviewDTO()
                            {
                                ReviewID = (int)reader["reviewid"],
                                ProductID = (int)reader["productid"],
                                UserID = (int)reader["userid"],
                                ReviewScore = (int)reader["reviewscore"],
                                ReviewText = (string)reader["reviewtext"],
                                ReviewDate = (DateTime)reader["reviewdate"]
                            };

                            reviews.Add(review);
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                throw new Exception("There was an SQL error during GetAll() product.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("There was an issue retrieving all product.", ex);
            }

            return reviews;
        }
    }
}
