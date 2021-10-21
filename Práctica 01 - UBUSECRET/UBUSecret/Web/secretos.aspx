<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="secretos.aspx.cs" Inherits="Web.secretos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="w-full flex flex-col items-center space-y-5">
        <h1>
            <asp:Label ID="Label1" runat="server" Text="Tus Secretos" class="text-center"></asp:Label>
        </h1>
        <div runat="server" id="Secretos" style="min-width: 800px; max-width: 1200px" class="flex flex-col space-y-5">
            <section class="alert alert-info flex flex-row overflow-hidden" role="alert">
                <div style="width: 60%" class="overflow-hidden">
                    Nombre
                </div>
                <div style="width: 20%" class="overflow-hidden text-center" >
                    Autor
                </div>
                <div style="width: 20%" class="overflow-hidden text-center">
                    Fecha
                </div>
            </section>
            <section class="alert alert-primary" role="alert" class="flex flex-row ">
                <div style="width: 60%">
                    Nombre
                </div>
                <div style="width: 20%">
                    Autor
                </div>
                <div style="width: 20%">
                    Fecha
                </div>
            </section>
        </div>
    </div>
</asp:Content>
