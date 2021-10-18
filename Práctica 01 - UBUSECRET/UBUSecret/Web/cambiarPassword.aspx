<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="cambiarPassword.aspx.cs" Inherits="Web.WebForm2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="flex flex-row justify-around">
        <h1>
            <asp:Label ID="LblTitle" runat="server" Text="UBUSECRET"></asp:Label></h1>
    </section>


    <div class="flex flex-col items-center justify-center w-screen " style="height: 60vh">

        <div class="flex flex-col items-center space-y-3 p-4 shadow-lg" style="width: 400px">

            <section class="flex flex-row justify-around">
                <h3>
                    <asp:Label ID="Label2" runat="server" Text="Cambiar Contraseña"></asp:Label></h3>
            </section>



            <section class="w-full flex flex-col justify-start">
                <label class="form-label">
                    <asp:Label ID="LblMail" runat="server" Text="Dirección Email:"></asp:Label></label>
                <asp:TextBox ID="BoxMail" runat="server" class="form-control" autofocus></asp:TextBox>

                <div class="text-danger">
                    <asp:Label ID="ErrMail" runat="server" Text="Email Mal"></asp:Label>
                </div>

            </section>




            <section class="w-full flex flex-col justify-start">
                <label class="form-label">
                    <asp:Label ID="LblPassw" runat="server" Text="Contraseña Antigua"></asp:Label></label>
                <asp:TextBox ID="BoxPassw" runat="server" type="password" class="form-control"></asp:TextBox>
                <div class="text-danger">
                    <asp:Label ID="ErrPassw" runat="server" Text="Contraseña Mal"></asp:Label>
                </div>
            </section>


            <section class="w-full flex flex-col justify-start">
                <asp:HyperLink ID="HyperLink1" runat="server" class="link-primary" href="#">Cambiar la contraseña</asp:HyperLink>
            </section>



            <section class="flex justify-end w-full">
                <asp:Button ID="Button1" runat="server" Text="Iniciar Sesión" class="btn btn-success" />
            </section>
        </div>
</asp:Content>
