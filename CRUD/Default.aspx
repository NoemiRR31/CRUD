<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CRUD.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Login</title>
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
    <script src="Scripts/angular.js"></script>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" integrity="sha384-wvfXpqpZZVQGK6TAh5PVlGOfQNHSoD2xbE+QkPxCAFlNEevoEH3Sl0sibVcOQVnN" crossorigin="anonymous" />
   
    <!-- --------------------- Funcion que nos permite agregar un modulo de angular para la visualizacion de password    ----------------------->
    <script>
        var app = angular.module('myapp', []);
        app.controller('Mycontroller', function ($scope) {
            $scope.showPassword = false;
            $scope.toggleShowPassword = function () {
                $scope.showPassword = !$scope.showPassword;
            }

        });
    </script>

    <script type="text/javascript"> 

        //Cuando pulse el boton de atras
        //javascript: window.history.forward(1);
        //cuando pulse el boton de adelante
        javascript: window.history.back(1);
        history.forward();
    </script>

    <link rel="shortcut icon" href="images/log.png" />
    <meta name="viewport" content="width=device-width, user-scalable=yes, initial-scale=1.0, maximum-scale=3.0, minimum-scale=1.0" />
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.6.3/css/all.css" />
    <link rel="stylesheet" href="css/estilos.css" />
</head>
<body>
    <form id="form1" class="formulario" runat="server">
        <!-- --------------------- Diseño  de login y validaciones   ----------------------->
        <h1>AUDIT LPA´S</h1>
        <div class="contenedor">
            <div class="input-contenedor">
                <i class="fas fa-envelope icon"></i>
                <asp:TextBox ID="txtUser" runat="server" placeholder="User" MaxLength="25"></asp:TextBox>
            </div>         
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUser" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="Validatorlogin">*Enter user</asp:RequiredFieldValidator>
            <div class="input-contenedor">

                <!-- --------------------- Nos permite  colocar un boton para la vista de la contrseña  ----------------------->
                <div ng-app="myapp" ng-controller="Mycontroller">
                    <i class="fas fa-key icon"></i>
                    <asp:TextBox ID="txtPassword" runat="server" type="password" ng-attr-type="{{showPassword ? 'text':'password'}}" placeholder="Password" MaxLength="18"></asp:TextBox>

                    <div id="showhidendiv" ng-click="toggleShowPassword()" ng-class="{'fa fa-eye':showPassword, 'fa fa-eye-slash':!showPassword}"></div>
                </div>
            </div>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="Validatorlogin">*Enter password </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPassword" ErrorMessage="RegularExpressionValidator" ForeColor="Red" ValidationExpression="^[0-9]*$" ValidationGroup="Validatorlogin">Only numbers</asp:RegularExpressionValidator>


            <!-- --------------------- Diseño de boton login y validaciones----------------------->

            <asp:Button ID="btnLogin" runat="server" Text="Login" class="button" OnClick="btnLogin_Click" ValidationGroup="Validatorlogin" />
            <asp:Label ID="LabelError" runat="server" ForeColor="#ff0000" Text="* Incorrect User Credentials"></asp:Label>
        </div>
    </form>

</body>
</html>
