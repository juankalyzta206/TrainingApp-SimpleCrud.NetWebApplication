<%@ Page Title="Input Divisi" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DivisiInput.aspx.cs" Inherits="TrainingApp.Forms.DivisiInput" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="card shadow-sm" style="max-width: 700px;">
        <div class="card-header">
            <h5 class="mb-0"><asp:Literal ID="litPageTitle" runat="server">Add New Divisi</asp:Literal></h5>
        </div>
        <div class="card-body">
            <p>Please fill in the division name below.</p>
            <div class="form-group" style="margin-bottom: 15px;">
                <label for="txtNama">Nama Divisi</label>
                <asp:TextBox ID="txtNama" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txtNama" 
                    ErrorMessage="Nama divisi wajib diisi." 
                    ForeColor="Red" Display="Dynamic">
                </asp:RequiredFieldValidator>
            </div>
            <asp:Label ID="lblStatus" runat="server" Font-Bold="true"></asp:Label>
        </div>
        <div class="card-footer text-right">
            <a href="../Divisi.aspx" class="btn btn-secondary">
                <i class="fas fa-times mr-2"></i>Cancel
            </a>
            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success" OnClick="btnSave_Click" />
        </div>
    </div>

</asp:Content>