<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="cambiarPassword.aspx.cs" Inherits="Web.WebForm2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="flex flex-row justify-around">
        <h1>
            <asp:Label ID="LblTitle" runat="server" Text="UBUSECRET"></asp:Label></h1>
    </section>


    <div class="flex flex-col items-center justify-center ">

        <div class="flex flex-col items-center space-y-3 p-4 shadow-lg" style="width: 600px">

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

            <hr />

            <section class="w-full flex flex-col justify-start">
                <label class="form-label">
                    <asp:Label ID="LblOld1" runat="server" Text="Contraseña Actual"></asp:Label></label>
                <asp:TextBox ID="BoxOld1" runat="server" type="password" class="form-control"></asp:TextBox>

            </section>

            <section class="w-full flex flex-col justify-start">
                <label class="form-label">
                    <asp:Label ID="LblOld2" runat="server" Text="Repita La Contraseña Actual"></asp:Label></label>
                <asp:TextBox ID="BoxOld2" runat="server" type="password" class="form-control"></asp:TextBox>
                <div class="text-danger">
                    <asp:Label ID="ErrOld" runat="server" Text="Las Contraseñas No Coinciden"></asp:Label>
                </div>
            </section>
            
            <hr class="w-full rouned h-2"/>

            <section class="w-full flex flex-col justify-start">
                <label class="form-label">
                    <asp:Label ID="LblPassw" runat="server" Text="Nueva Contraseña"></asp:Label></label>
                <asp:TextBox ID="BoxPassw" runat="server" type="password" class="form-control"></asp:TextBox>
                <div class="text-danger">
                    <asp:Label ID="ErrPassw" runat="server" Text="Campo Vacío"></asp:Label>
                </div>
            </section>



            <section class="flex justify-end flex-row w-full space-x-4">
                <asp:Button ID="Cancel" runat="server" Text="Cancelar" class="btn btn-danger"     />
                <asp:Button ID="Send" runat="server" Text="Cambiar" class="btn btn-success" />
            </section>
        </div>
</asp:Content>
