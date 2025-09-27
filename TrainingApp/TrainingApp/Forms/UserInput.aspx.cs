using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrainingApp.Library;
using TrainingApp.Models;
using TrainingApp.Repository;

namespace TrainingApp
{
    public partial class UserInput : System.Web.UI.Page
    {
        private readonly UserRepository _userRepository = new UserRepository();
        private readonly DivisiRepository _divisiRepository = new DivisiRepository();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDivisiDropDown();

                if (Request.QueryString["id"] != null)
                {
                    int userId = Convert.ToInt32(Request.QueryString["id"]);
                    ViewState["UserID"] = userId; 

                    UserDto user = _userRepository.GetUserById(userId);
                    if (user != null)
                    {
                        PopulateForm(user); 
                    }

                    this.Title = "Edit User";
                    btnSave.Text = "Update";
                }
                else
                {
                    this.Title = "Add New User";
                    btnSave.Text = "Save";
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            try
            {
                UserDto user = new UserDto
                {
                    Nama = txtNama.Text,
                    Gender = rblGender.SelectedValue == "1",
                    DivisiId = Convert.ToInt32(ddlDivisi.SelectedValue),
                    Note = txtNote.Text,
                    Tanggal = Convert.ToDateTime(txtTanggal.Text)
                };

                if (ViewState["UserID"] != null)
                {
                    user.Id = (int)ViewState["UserID"];
                    _userRepository.UpdateUser(user); 
                }
                else
                {
                    _userRepository.InsertUser(user); 
                }

                Response.Redirect("~/User.aspx");
            }
            catch (Exception ex)
            {
                Util.CreateLog(Util.getDetail(ex));
                lblStatus.Text = "Error saving data. See log for details.";
                lblStatus.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void LoadDivisiDropDown()
        {
            try
            {
                ddlDivisi.DataSource = _divisiRepository.GetAllDivisi();
                ddlDivisi.DataTextField = "nama";
                ddlDivisi.DataValueField = "id";
                ddlDivisi.DataBind();
                ddlDivisi.Items.Insert(0, new ListItem("-- Select Division --", ""));
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Error loading divisions: " + ex.Message;
                lblStatus.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void PopulateForm(UserDto user)
        {
            txtNama.Text = user.Nama;
            rblGender.SelectedValue = user.Gender ? "1" : "0";
            ddlDivisi.SelectedValue = user.DivisiId.ToString();
            txtNote.Text = user.Note;
            txtTanggal.Text = user.Tanggal.ToString("yyyy-MM-dd");
        }
    }
}