using System.Data;
using System.Data.SqlClient;
using TrainingApp.Library;
using TrainingApp.Models;

namespace TrainingApp.Repository
{
    public class UserRepository
    {
        private static readonly string connectionString = Connection.ConnectionStringMsSQL;

        public DataTable GetAllUsers()
        {
            DataTable dt = new DataTable();
            // Query diperbarui dengan JOIN untuk mendapatkan nama divisi
            string query = @"
                SELECT u.id, u.nama, u.gender, d.nama as divisi_nama, u.note, u.tanggal 
                FROM [user] u
                LEFT JOIN divisi d ON u.divisi_id = d.id";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                    }
                }
            }
            return dt;
        }

        // *** METHOD BARU UNTUK PAGING DAN SEARCH ***
        public DataTable GetUserPaged(string searchTerm, int pageNumber, int pageSize, out int totalRows)
        {
            DataTable dt = new DataTable();
            totalRows = 0;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_User_GetPaged", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SearchTerm", searchTerm);
                    cmd.Parameters.AddWithValue("@PageNumber", pageNumber);
                    cmd.Parameters.AddWithValue("@PageSize", pageSize);

                    SqlParameter outParam = new SqlParameter("@TotalRows", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outParam);

                    try
                    {
                        con.Open();
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(dt);
                        }
                        totalRows = (int)cmd.Parameters["@TotalRows"].Value;
                    }
                    catch (System.Exception ex)
                    {
                        Util.CreateLog(Util.getDetail(ex));
                    }
                }
            }
            return dt;
        }

        public UserDto GetUserById(int userId)
        {
            UserDto user = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT id, nama, gender, divisi_id, note, tanggal FROM [user] WHERE id = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", userId);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        user = new UserDto
                        {
                            Id = (int)reader["id"],
                            Nama = reader["nama"].ToString(),
                            Gender = (bool)reader["gender"],
                            DivisiId = (int)reader["divisi_id"],
                            Note = reader["note"].ToString(),
                            Tanggal = (System.DateTime)reader["tanggal"]
                        };
                    }
                }
            }
            return user;
        }

        public void InsertUser(UserDto user)
        {
            string procedureName = "sp_UserInsert";
            SqlParameter[] parameters = {
                new SqlParameter("@nama", user.Nama),
                new SqlParameter("@gender", user.Gender),
                new SqlParameter("@divisi_id", user.DivisiId),
                new SqlParameter("@note", user.Note),
                new SqlParameter("@tanggal", user.Tanggal)
            };
            Command.ExecuteSp(procedureName, parameters);
        }

        public void UpdateUser(UserDto user)
        {
            string procedureName = "sp_UserUpdate";
            SqlParameter[] parameters = {
                new SqlParameter("@id", user.Id),
                new SqlParameter("@nama", user.Nama),
                new SqlParameter("@gender", user.Gender),
                new SqlParameter("@divisi_id", user.DivisiId),
                new SqlParameter("@note", user.Note),
                new SqlParameter("@tanggal", user.Tanggal)
            };
            Command.ExecuteSp(procedureName, parameters);
        }

        public void DeleteUser(int userId)
        {
            string procedureName = "sp_UserDelete";
            SqlParameter[] parameters = { new SqlParameter("@id", userId) };
            Command.ExecuteSp(procedureName, parameters);
        }
    }
}