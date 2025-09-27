using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrainingApp.Library;
using TrainingApp.Repository;

namespace TrainingApp
{
    public partial class Divisi : Page
    {
        private readonly DivisiRepository _divisiRepository = new DivisiRepository();

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
            int pageNumber = GridViewDivisi.PageIndex + 1;
            int pageSize = GridViewDivisi.PageSize;
            string searchTerm = txtSearch.Text;

            DataTable dt = _divisiRepository.GetDivisiPaged(searchTerm, pageNumber, pageSize, out totalRows);

            GridViewDivisi.DataSource = dt;
            GridViewDivisi.VirtualItemCount = totalRows;
            GridViewDivisi.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GridViewDivisi.PageIndex = 0;
            BindGrid();
        }

        protected void GridViewDivisi_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewDivisi.PageIndex = e.NewPageIndex;
            BindGrid();
        }



        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                GridViewRow row = (GridViewRow)btn.NamingContainer;
                int divisiId = Convert.ToInt32(GridViewDivisi.DataKeys[row.RowIndex].Value);

                _divisiRepository.DeleteDivisi(divisiId);

                Response.Redirect(Request.RawUrl);
            }
            catch (Exception ex)
            {
                Util.CreateLog(Util.getDetail(ex));
                lblError.Text = "Error: This division cannot be deleted because it is currently assigned to one or more users.";
            }
        }
    }
}