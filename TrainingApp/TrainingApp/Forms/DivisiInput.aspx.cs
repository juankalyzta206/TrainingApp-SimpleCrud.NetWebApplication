using System;
using System.Web.UI;
using TrainingApp.Library;
using TrainingApp.Models;
using TrainingApp.Repository;

namespace TrainingApp.Forms
{
    public partial class DivisiInput : System.Web.UI.Page
    {
        private readonly DivisiRepository _divisiRepository = new DivisiRepository();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    int divisiId = Convert.ToInt32(Request.QueryString["id"]);
                    ViewState["DivisiID"] = divisiId;

                    DivisiDto divisi = _divisiRepository.GetDivisiById(divisiId);
                    if (divisi != null)
                    {
                        txtNama.Text = divisi.Nama;
                    }

                    this.Title = "Edit Divisi";
                    btnSave.Text = "Update";
                }
                else
                {
                    this.Title = "Add New Divisi";
                    btnSave.Text = "Save";
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            try
            {
                DivisiDto divisi = new DivisiDto { Nama = txtNama.Text };

                if (ViewState["DivisiID"] != null)
                {
                    divisi.Id = (int)ViewState["DivisiID"];
                    _divisiRepository.UpdateDivisi(divisi);
                }
                else
                {
                    _divisiRepository.InsertDivisi(divisi);
                }
                Response.Redirect("~/Divisi.aspx");
            }
            catch (Exception ex)
            {
                Util.CreateLog(Util.getDetail(ex));
                lblStatus.Text = "Error saving data. See log for details.";
                lblStatus.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}