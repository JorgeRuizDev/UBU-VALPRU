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


        <h4>
            <asp:Label ID="Label2" runat="server" Text="Título:" class="text-center"></asp:Label>
        </h4>
        <p>
            <asp:Label ID="titulo" runat="server" Text="" class="text-center"></asp:Label>
        </p>


        <h4>
            <asp:Label ID="Label3" runat="server" Text="Fecha y Hora:" class="text-center"></asp:Label>
        </h4>
        <p>
            <asp:Label ID="fecha" runat="server" Text="" class="text-center"></asp:Label>
        </p>



        <h4>
            <asp:Label ID="Label4" runat="server" Text="Autor:" class="text-center"></asp:Label>
        </h4>
        <p>
            <asp:Label ID="autor" runat="server" Text="" class="text-center"></asp:Label>
        </p>


        <h4>
            <asp:Label ID="Label5" runat="server" Text="Mensaje:" class="text-center"></asp:Label>
        </h4>
        <p>
            <asp:Label ID="secreo" runat="server" Text="" class="text-center"></asp:Label>
        </p>



    <asp:Label ID="SecretId" runat="server" Text="Label"></asp:Label>
</asp:Content>
