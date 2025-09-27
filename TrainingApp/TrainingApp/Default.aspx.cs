using System;
using System.Web.UI;
using TrainingApp.Repository; 

namespace TrainingApp
{
    public partial class _Default : Page
    {
        private readonly MenuRepository _menuRepository = new MenuRepository();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadProductsData();
            }
        }

        private void LoadProductsData()
        {
            GridViewMenu.DataSource = _menuRepository.GetAllMenus();
            GridViewMenu.DataBind();
        }
    }
}