<%@ Page Title="Input User" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserInput.aspx.cs" Inherits="TrainingApp.UserInput" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="card shadow-sm" style="max-width: 700px;">
        <div class="card-header">
            <h5 class="mb-0"><asp:Literal ID="litPageTitle" runat="server">Add New User</asp:Literal></h5>
        </div>
        <div class="card-body">
            <p>Please fill in the details below and click Save.</p>
    
            <div class="form-group" style="margin-bottom: 15px;">
                <label for="txtNama">Nama</label>
                <asp:TextBox ID="txtNama" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="form-group" style="margin-bottom: 15px;">
                <label for="rblGender">Gender</label>
                <asp:RadioButtonList ID="rblGender" runat="server" RepeatDirection="Horizontal" CssClass="ml-2">
                    <asp:ListItem Value="1" Selected="True">Male</asp:ListItem>
                    <asp:ListItem Value="0">Female</asp:ListItem>
                </asp:RadioButtonList>
            </div>

            <div class="form-group" style="margin-bottom: 15px;">
                <label for="ddlDivisi">Divisi</label>
                <asp:DropDownList ID="ddlDivisi" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>

            <div class="form-group" style="margin-bottom: 15px;">
                <label for="txtNote">Note</label>
                <asp:TextBox ID="txtNote" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
            </div>

            <div class="form-group" style="margin-bottom: 15px;">
                <label for="txtTanggal">Tanggal</label>
                <asp:TextBox ID="txtTanggal" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txtTanggal" 
                    ErrorMessage="Tanggal wajib diisi." 
                    ForeColor="Red" Display="Dynamic">
                </asp:RequiredFieldValidator>
            </div>
            <asp:Label ID="lblStatus" runat="server" Font-Bold="true"></asp:Label>
        </div>
        <div class="card-footer text-right">
             <a href="../User.aspx" class="btn btn-secondary">
                <i class="fas fa-times mr-2"></i>Cancel
             </a>
             <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success" OnClick="btnSave_Click" />
        </div>
    </div>

</asp:Content>