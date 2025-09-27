<%@ Page 
    Title="Home Page" 
    Language="C#" 
    MasterPageFile="~/Site.master" 
    AutoEventWireup="true" 
    CodeBehind="Default.aspx.cs" 
    Inherits="TrainingApp._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="card shadow-sm">
        <div class="card-header">
            <h5 class="mb-0">Menu Data Structure</h5>
        </div>
        <div class="card-body">
            <asp:GridView ID="GridViewMenu" runat="server" CssClass="table table-bordered table-hover"></asp:GridView>
        </div>
    </div>

</asp:Content>