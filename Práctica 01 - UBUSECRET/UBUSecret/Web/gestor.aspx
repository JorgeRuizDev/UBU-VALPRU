<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="gestor.aspx.cs" Inherits="Web.WebForm4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="w-full flex flex-col items-center space-y-5">
        <h1>
            <asp:Label ID="Label1" runat="server" Text="Panel de Admisitrador" class="text-center"></asp:Label>
        </h1>
        <div runat="server" id="Deshabilitados" style="min-width: 800px; max-width: 1200px" class="flex flex-col space-y-5">
            <section class="alert alert-info flex flex-row overflow-hidden" role="alert">
                <div style="width: 33%" class="overflow-hidden">
                    Usuario
                </div>
                <div style="width: 33%" class="overflow-hidden">
                    Email
                </div>

                <div style="width: 33%" class="overflow-hidden">
                    Rol
                </div>
            </section>
            <asp:Label ID="Test" runat="server" Text=""></asp:Label>


        </div>
        <hr class="w-full h-1" />
        <h3>Log del Sistema:</h3>
        <section>
            <asp:Label ID="LogBody" runat="server" Text=""></asp:Label>
        </section>
    </div>
</asp:Content>
