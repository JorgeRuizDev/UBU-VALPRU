<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="secreto.aspx.cs" Inherits="Web.WebForm3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <nav aria-label="breadcrumb">
        <ol class="breadcrumb">
            <li class="breadcrumb-item"><a href="./">
                <asp:Label ID="Label1" runat="server" Text="Secretos"></asp:Label>

            </a></li>
            <li class="breadcrumb-item active" aria-current="page">
                <asp:Label ID="BreadcrumSecret" runat="server" Text="Título del Secreto Aquí"></asp:Label>
            </li>
        </ol>
    </nav>

    <asp:Label ID="SecretId" runat="server" Text="Label"></asp:Label>
</asp:Content>
