using System;
using System.Data;
using System.Data.SqlClient;
using TrainingApp.Library;
using TrainingApp.Models;

namespace TrainingApp.Repository
{
    public class DivisiRepository
    {
        private static readonly string connectionString = Connection.ConnectionStringMsSQL;

        public DataTable GetAllDivisi()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT id, nama FROM Divisi", con))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                    }
                }
            }
            return dt;
        }

        public DivisiDto GetDivisiById(int divisiId)
        {
            DivisiDto divisi = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT id, nama FROM Divisi WHERE id = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", divisiId);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        divisi = new DivisiDto
                        {
                            Id = (int)reader["id"],
                            Nama = reader["nama"].ToString()
                        };
                    }
                }
            }
            return divisi;
        }

        public void InsertDivisi(DivisiDto divisi)
        {
            string procedureName = "sp_DivisiInsert";
            SqlParameter[] parameters = { new SqlParameter("@nama", divisi.Nama) };
            Command.ExecuteSp(procedureName, parameters);
        }

        public void UpdateDivisi(DivisiDto divisi)
        {
            string procedureName = "sp_DivisiUpdate";
            SqlParameter[] parameters = {
                new SqlParameter("@id", divisi.Id),
                new SqlParameter("@nama", divisi.Nama)
            };
            Command.ExecuteSp(procedureName, parameters);
        }

        public void DeleteDivisi(int divisiId)
        {
            string procedureName = "sp_DivisiDelete";
            SqlParameter[] parameters = { new SqlParameter("@id", divisiId) };
            Command.ExecuteSp(procedureName, parameters);
        }

        public DataTable GetDivisiPaged(string searchTerm, int pageNumber, int pageSize, out int totalRows)
        {
            DataTable dt = new DataTable();
            totalRows = 0; 

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Divisi_GetPaged", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SearchTerm", searchTerm);
                    cmd.Parameters.AddWithValue("@PageNumber", pageNumber);
                    cmd.Parameters.AddWithValue("@PageSize", pageSize);

                    SqlParameter outParam = new SqlParameter("@TotalRows", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
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
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine("Error in GetDivisiPaged: " + ex.Message);
                    }
                }
            }
            return dt;
        }
    }
}