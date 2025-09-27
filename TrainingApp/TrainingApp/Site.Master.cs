using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrainingApp.Repository;

namespace TrainingApp
{
    public partial class SiteMaster : MasterPage
    {
        private readonly MenuRepository _menuRepository = new MenuRepository();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateMenu();
            }
        }

        private void PopulateMenu()
        {
            try
            {
                DataTable menuData = _menuRepository.GetAllMenus();

                TreeView1.Nodes.Clear();

                
                var parentRows = menuData.AsEnumerable().Where(row => row.IsNull("parent_id") || row.Field<int>("parent_id") == 0);

                foreach (DataRow row in parentRows)
                {
                    TreeNode parentNode = new TreeNode
                    {
                        Text = row["menu"].ToString(),
                        NavigateUrl = row["url"].ToString(),
                        Value = row["id"].ToString()
                    };

                    TreeView1.Nodes.Add(parentNode);

                    AddChildNodes(parentNode, menuData);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error populating menu: " + ex.Message);
            }
        }

        private void AddChildNodes(TreeNode parentNode, DataTable menuData)
        {
            int parentId = Convert.ToInt32(parentNode.Value);

            var childRows = menuData.AsEnumerable().Where(row => !row.IsNull("parent_id") && row.Field<int>("parent_id") == parentId);

            foreach (DataRow row in childRows)
            {
                TreeNode childNode = new TreeNode
                {
                    Text = row["menu"].ToString(),
                    NavigateUrl = row["url"].ToString(),
                    Value = row["id"].ToString()
                };

                parentNode.ChildNodes.Add(childNode);

                AddChildNodes(childNode, menuData);
            }
        }
    }
}