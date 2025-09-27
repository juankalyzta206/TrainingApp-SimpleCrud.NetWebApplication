using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrainingApp.Repository;
using TrainingApp.Library;

namespace TrainingApp
{
    public partial class User : Page
    {
        private readonly UserRepository _userRepository = new UserRepository();

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        private void BindGrid()
        {
            int totalRows;
            int pageNumber = GridViewUser.PageIndex + 1;
            int pageSize = GridViewUser.PageSize;
            string searchTerm = txtSearch.Text;

            DataTable dt = _userRepository.GetUserPaged(searchTerm, pageNumber, pageSize, out totalRows);

            GridViewUser.DataSource = dt;
            GridViewUser.VirtualItemCount = totalRows;
            GridViewUser.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GridViewUser.PageIndex = 0;
            BindGrid();
        }

        protected void GridViewUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewUser.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                GridViewRow row = (GridViewRow)btn.NamingContainer;
                int userId = Convert.ToInt32(GridViewUser.DataKeys[row.RowIndex].Value);

                _userRepository.DeleteUser(userId);

                Response.Redirect(Request.RawUrl);
            }
            catch (Exception ex)
            {
                Util.CreateLog(Util.getDetail(ex));
                lblError.Text = "Error deleting data. See log for details.";
            }
        }
    }
}