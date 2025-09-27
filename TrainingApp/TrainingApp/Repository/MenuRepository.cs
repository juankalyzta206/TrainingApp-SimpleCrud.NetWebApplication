using System.Data;
using System.Data.SqlClient;
using TrainingApp.Library;

namespace TrainingApp.Repository
{
    public class MenuRepository
    {
        private static readonly string connectionString = Connection.ConnectionStringMsSQL;

        public DataTable GetAllMenus()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT id, menu, url, level, parent_id FROM menu", con))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                    }
                }
            }
            return dt;
        }
    }
}