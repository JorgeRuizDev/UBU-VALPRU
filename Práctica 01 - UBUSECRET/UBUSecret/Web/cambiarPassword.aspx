<%@ Page Title="Cambiar Contraseña" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="cambiarPassword.aspx.cs" Inherits="Web.WebForm2" %>

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
                    <asp:Label ID="LblMail" runat="server" Text="Dirección Email: " type="email"></asp:Label></label>
                <asp:TextBox ID="BoxMail" runat="server" class="form-control" autofocus required placeholder="paco@ubusecret.es"></asp:TextBox>

                <div class="text-danger">
                    <asp:Label ID="ErrMail" runat="server" Text="Email Mal"></asp:Label>
                </div>

            </section>

            <hr class="w-full rouned h-2"/>
            

            <section class="w-full flex flex-col justify-start">
                <label class="form-label">
                    <asp:Label ID="LblOld" runat="server" Text="Contraseña Actual" ></asp:Label></label>
                <asp:TextBox ID="BoxOld" runat="server" type="password" class="form-control" required placeholder="1234"></asp:TextBox>

            </section>

            
            <hr class="w-full rouned h-2"/>

            <section class="w-full flex flex-col justify-start">
                <label class="form-label">
                    <asp:Label ID="LblPassw" runat="server" Text="Nueva Contraseña"></asp:Label></label>
                <asp:TextBox ID="BoxPassw" runat="server" type="password" class="form-control" required placeholder="123456"></asp:TextBox>
                
                
            <section class="w-full flex flex-col justify-start">
                <label class="form-label">
                    <asp:Label ID="LblPassw2" runat="server" Text="Repita La Contraseña"></asp:Label></label>
                <asp:TextBox ID="BoxPassw2" runat="server" type="password" class="form-control"  CausesValidation="True" required placeholder="1234"></asp:TextBox>
                
                <div class="text-danger">
                    <asp:Label ID="ErrOld" runat="server" Text="Las Contraseñas No Coinciden" Visible="False" ></asp:Label>
                </div>
            </section>
                <div class="text-danger">
                    <asp:Label ID="ErrPassw" runat="server" Text="Campo Vacío"></asp:Label>
                </div>
                 <asp:Label class="text-sm text-gray-600" ID="Label1" runat="server" Text="La contraseña debe tener una longitud de 6 carácteres, con al menos una mayúscula, una minúscula y un dígito"></asp:Label></label>

            </section>



            <section class="flex justify-end flex-row w-full space-x-4">

                <asp:Button ID="BtnCancel" runat="server" Text="Cancelar" class="btn btn-danger"  OnClick="BtnCancel_Click"   UseSubmitBehavior="False" />
                <asp:Button ID="BtnSend" runat="server" Text="Cambiar" class="btn btn-success" OnClick="BtnSend_Click" />
            </section>
        </div>
</asp:Content>
