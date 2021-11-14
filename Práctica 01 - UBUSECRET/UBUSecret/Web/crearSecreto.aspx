<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="crearSecreto.aspx.cs" Inherits="Web.crearSecreto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <nav aria-label="breadcrumb">
        <ol class="breadcrumb">
            <li class="breadcrumb-item"><a href="./">
                <asp:Label ID="Label1" runat="server" Text="Secretos"></asp:Label>

            </a></li>
            <li class="breadcrumb-item active" aria-current="page">
                <asp:Label ID="BreadcrumSecret" runat="server" Text="Crear un nuevo secreto"></asp:Label>
            </li>
        </ol>
    </nav>

    <div class="w-full flex justify-center">


        <div class="flex flex-col justify-center w-5/6 space-y-5">

            <section>
                <div class="input-group mb-3">
                    <div class="input-group-prepend">
                        <span class="input-group-text" id="titleo">
                            <asp:Label ID="LabTitle" runat="server" Text="Title:"></asp:Label>
                        </span>
                        <asp:TextBox ID="TextTitle" runat="server" class="form-control" Columns="100" placeholder="Secret Title"></asp:TextBox>
                    </div>
                </div>
            </section>

            <section>
                <div class="input-group mb-3">
                    <div class="input-group-prepend">
                        <span class="input-group-text" id="Alias">
                            <asp:Label ID="LabAlias" runat="server" Text="Alias:"></asp:Label>
                        </span>
                        <asp:TextBox ID="TexAlias" runat="server" class="form-control" Columns="100" placeholder="Alias123"></asp:TextBox>
                    </div>
                </div>
            </section>
            
            
            
            <section class="flex flex-col">
                <label>
                    <asp:Label ID="Label2" runat="server" Text="Secreto:"></asp:Label></label>
                <asp:TextBox ID="BoxSecreto" TextMode="multiline" Columns="50" Rows="5" runat="server" class="form-control" placeholder="Escriba su Secreto (Máximo 255 caracteres)" />
            
                <div class="text-danger">
                    <asp:Label ID="ErrSecreto" runat="server" Text="No se puede introducir mas de 250 caracteres o ninguno" Visible="False"></asp:Label>
                </div>
            
            
            </section>

            <section>
                <div class="input-group mb-3">
                    <div class="input-group-prepend">
                        <span class="input-group-text" id="basic-addon1">
                            <asp:Label ID="Label3" runat="server" Text="Give Access To:"></asp:Label>
                        </span>
                        <asp:TextBox ID="BoxMails" runat="server" class="form-control" Columns="100" placeholder="yo@ubusecret.es;tu@ubusecret.es"></asp:TextBox>
                    </div>
                </div>


                <div class="text-danger">
                    <asp:Label ID="ErrMails" runat="server" Text="Email/s no validos" Visible="False"></asp:Label>
                </div>


            </section>


            
            <section class="flex justify-end flex-row w-full space-x-4">
                <asp:Button ID="BtnSend" runat="server" Text="Publish Secret" class="btn btn-success" OnClick="BtnSend_Click" />
            </section>



        </div>
    </div>

</asp:Content>
