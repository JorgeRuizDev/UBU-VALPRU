<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="registro.aspx.cs" Inherits="Web.registro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="flex flex-row justify-around">
        <h1>
            <asp:Label ID="LblTitle" runat="server" Text="Registro"></asp:Label>
        </h1>
    </section>

    <div class="flex flex-col items-center justify-center w-screen " style="height: 60vh">

        <div class="flex flex-col items-center space-y-3 p-4 shadow-lg" style="width: 800px">





            <section class="w-full flex flex-col justify-start">
                <label class="form-label">
                    <asp:Label ID="LblMail" runat="server" Text="Dirección Email:"></asp:Label></label>
                <asp:TextBox ID="BoxMail" runat="server" class="form-control" autofocus required></asp:TextBox>

                <div class="text-danger">
                    <asp:Label ID="ErrMail" runat="server" Text="Email Mal"></asp:Label>
                </div>

            </section>




            <section class="w-full flex flex-col justify-start">
                <label class="form-label">
                    <asp:Label ID="LblPassw" runat="server" Text="Contraseña:"></asp:Label></label>
                <asp:TextBox ID="BoxPassw" runat="server" type="password" class="form-control" required></asp:TextBox>
                <div class="text-danger">
                    <asp:Label ID="ErrPassw" runat="server" Text="Contraseña Mal"></asp:Label>
                </div>
            </section>

            <section class="flex justify-end flex-row w-full space-x-4">
                <asp:Button ID="BtnCancel" runat="server" Text="Cancelar" class="btn btn-danger"  OnClick="BtnCancel_Click"   UseSubmitBehavior="False" />
                <asp:Button ID="BtnSend" runat="server" Text="Cambiar" class="btn btn-success" OnClick="BtnSend_Click" />
            </section>
    </div>

</asp:Content>
