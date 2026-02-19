<%@ Page Title="Products" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductsOld.aspx.cs" Inherits="WebForms.Products" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <section class="row" aria-labelledby="aspnetTitle">
            <h1>Products</h1>
            <p class="lead">Description of the page</p>
            <div class=" d-flex flex-column">
                <asp:Repeater ID="rProducts" runat="server">
                    <ItemTemplate>
                        <div class="col-md-4">
                            <h4><%# Eval("Id") + ". " + Eval("Name") %></h4>
                            <p><%# Eval("Category") %></p>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </section>
    </main>

</asp:Content>

