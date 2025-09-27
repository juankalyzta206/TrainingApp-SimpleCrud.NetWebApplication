<%@ Page Title="User Management" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="TrainingApp.User" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="card shadow-sm">
        <div class="card-header d-flex justify-content-between align-items-center">
            <h5 class="mb-0">User Management</h5>
            <a href="Forms/UserInput.aspx" class="btn btn-primary">
                <i class="fas fa-user-plus mr-2"></i>Add New User
            </a>
        </div>
        <div class="card-body">

            <div class="row mb-3">
                <div class="col-md-4">
                    <div class="input-group">
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Cari nama user..."></asp:TextBox>
                        <div class="input-group-append">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-outline-secondary" OnClick="btnSearch_Click" />
                        </div>
                    </div>
                </div>
            </div>

            <asp:Label ID="lblError" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
            
            <asp:GridView ID="GridViewUser" runat="server" 
                CssClass="table table-bordered table-hover" 
                AutoGenerateColumns="False"  
                DataKeyNames="id"
                AllowPaging="True"
                AllowCustomPaging="True"
                PageSize="5"
                OnPageIndexChanging="GridViewUser_PageIndexChanging">
                <Columns>
                    <asp:BoundField DataField="id" HeaderText="ID" />
                    <asp:BoundField DataField="nama" HeaderText="Nama" />
                    <asp:TemplateField HeaderText="Gender">
                        <ItemTemplate>
                            <%# Convert.ToBoolean(Eval("gender")) ? "Male" : "Female" %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="divisi_nama" HeaderText="Divisi" /> <%-- Diubah dari divisi_id --%>
                    <asp:BoundField DataField="note" HeaderText="Note" />
                    <asp:BoundField DataField="tanggal" HeaderText="Tanggal" DataFormatString="{0:dd-MM-yyyy}" />
                    <asp:TemplateField HeaderText="Actions">
                        <ItemTemplate>
                            <a href='<%# "Forms/UserInput.aspx?id=" + Eval("id") %>' class="btn btn-sm btn-warning">
                                <i class="fas fa-edit"></i> Edit
                            </a>
                            <asp:Button ID="btnDelete" runat="server"
                                Text="Delete" 
                                CssClass="btn btn-sm btn-danger"
                                OnClick="btnDelete_Click"
                                OnClientClick="return confirm('Are you sure you want to delete this user?');" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerSettings Mode="NumericFirstLast" Position="Bottom" Visible="True" PageButtonCount="5" FirstPageText="&laquo; First" LastPageText="Last &raquo;" NextPageText="&rsaquo;" PreviousPageText="&lsaquo;" />
                <PagerStyle CssClass="pagination-bs" />
            </asp:GridView>
        </div>
    </div>

</asp:Content>