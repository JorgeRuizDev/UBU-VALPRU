<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="secretos.aspx.cs" Inherits="Web.secretos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <nav aria-label="breadcrumb">
        <ol class="breadcrumb">
            <li class="breadcrumb-item"><a href="./">
                <asp:Label ID="Label2" runat="server" Text="Secretos"></asp:Label>

            </a></li>

        </ol>
    </nav>

    <div class="w-full flex flex-col items-center space-y-5">
        <h1>
            <asp:Label ID="Label1" runat="server" Text="Tus Secretos" class="text-center"></asp:Label>
        </h1>
        <div runat="server" id="Secretos" style="min-width: 800px; max-width: 1200px" class="flex flex-col space-y-5">
            <section class="alert alert-info flex flex-row overflow-hidden" role="alert">
                <div style="width: 60%" class="overflow-hidden">
                    Nombre
                </div>
                <div style="width: 20%" class="overflow-hidden text-center">
                    Autor
                </div>

                <div style="width: 20%" class="overflow-hidden text-center">
                    Fecha
                </div>
            </section>

        </div>
    </div>
    <div class=" w-full flex justify-center">
        <asp:Button ID="CrearSecreto" runat="server" Text="Crear Secreto" OnClick="CrearSecreto_Click" class="btn btn-success w-3/6" />
    </div>
</asp:Content>
