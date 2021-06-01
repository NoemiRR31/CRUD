

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FullAudit.aspx.cs" Inherits="CRUD.FullAudit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <link href="Content/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

    <!-- --------------------- Funcion que permite mostrar la imagen   ----------------------->
  
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
    <script type="text/javascript"> 

        function showImagePreview(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    document.getElementsByTagName("img")[0].setAttribute("src", e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
            }
        }

    </script>

   <script type="text/javascript"> 

       //Cuando pulse el boton de atras
       //javascript: window.history.forward(1);
       //cuando pulse el boton de adelante
       /javascript: window.history.back(1);/
       history.forward();
    </script>

    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.6.3/css/all.css" />
     <!-- --------------------- Estilo de la pantalla  ----------------------->
    <link rel="stylesheet" href="css/estilosFA.css" />
    <style type="text/css">
        .auto-style1 {
            position: relative;
            width: 100%;
            -ms-flex-preferred-size: 0;
            flex-basis: 0;
            -ms-flex-positive: 1;
            flex-grow: 1;
            max-width: 100%;
            left: 0px;
            top: -90px;
            padding-left: 15px;
            padding-right: 15px;
        }
    </style>
</head>


<body >
    <form id="form1" class="container" runat="server">
        <br />

        <!-- --------------------- Diseño de Labels del usuario y boton refresh  ----------------------->

        <h3 class="display-6">

            <asp:Label ID="LabelPass" runat="server" ForeColor="Black" Font-Bold="True"></asp:Label>
            <asp:Label ID="LabelUsername" runat="server" ForeColor="Black" Font-Bold="True"></asp:Label>
            <asp:Label ID="LabelUser" runat="server" ForeColor="Black" Font-Bold="True"></asp:Label></h3>
        <asp:Button ID="Button3" runat="server" Text="LOGOUT" OnClick="Button3_Click"  OnClientClick="return confirm('Are you sure you want to log out? ?');" class="btn btn-danger" aria-pressed="true" />

        <div class="row">
            <div class="auto-style1"></div>
            <div class="col-xs-3">
                <asp:Button ID="btnREFRESH" runat="server" CssClass="btn btn-info" Text="REFRESH" OnClientClick="return confirm('Do you want to reload the entire page?');" OnClick="btnREFRESH_Click1" />
            </div>
        </div>

        <!-- --------------------- Diaeño y validaciones  de los campos  Auditor, Date, Tail Number y  Line----------------------->

        <div class="row">
            <div class="col">
                <asp:Label ID="LblAuditor" runat="server" Text="Auditor Id:" CssClass="milabel" Font-Bold="True" Font-Italic="True" Font-Size="Large"></asp:Label>
                <br />
                <asp:TextBox ID="TextBoxAudi" CssClass="form-control-sm" runat="server" Enabled="false" Height="34px" Width="248px" MaxLength="11" ForeColor="Black" BorderColor="Black" Font-Size="X-Large"></asp:TextBox>
            </div>


            <div class="col">
                <asp:Label ID="LblDate" runat="server" Text="Date:" CssClass="milabel" Font-Bold="True" Font-Italic="True" Font-Size="Large"></asp:Label>
                <br />
                <asp:TextBox ID="TextBoxDate" CssClass="form-control-sm" OnClientDateSelectionChanged="checkDate" runat="server" Height="34px" Width="248px" TextMode="Date" MaxLength="10" BorderColor="Black" Font-Size="Medium"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="TextBoxDate" ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="grupoValidacion">*Enter date </asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="TextBoxDate" ErrorMessage="CompareValidator" ForeColor="Red" Operator="DataTypeCheck" Type="Date" ValidationGroup="grupoValidacion ">Enter valid format </asp:CompareValidator>
            </div>


            <div class="col">
                <asp:Label ID="LblTN" runat="server" Text="Tail Number:" CssClass="milabel" Font-Bold="True" Font-Italic="True" Font-Size="Large"></asp:Label>
                <br />
                <asp:TextBox ID="TextBoxTN" CssClass=" form-control-sm" runat="server" Height="34px" Width="248px" MaxLength="10" BorderColor="Black" Font-Size="Medium"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBoxTN" ErrorMessage="RequiredFieldValidator" ForeColor="Red" Display="Dynamic" ValidationGroup="grupoValidacion">*Enter Tail number</asp:RequiredFieldValidator>
            </div>


            <div class="col">
                <asp:Label ID="LbLine" runat="server" Text="Line:" CssClass="milabel" Font-Bold="True" Font-Italic="True" Font-Size="Large"></asp:Label>
                <br />
                <asp:DropDownList ID="DropDownLINE" CssClass=" form-control-sm  dropdown-toggle  active" runat="server" BorderColor="Black" Height="34px" Width="248px" DataTextField="DESC_LINE" DataValueField="DESC_LINE" Font-Size="Medium">
                </asp:DropDownList>
            </div>
        </div>

        <!-- --------------------- Diaeño y validaciones  de los campos Question, Aczone y  Shift ----------------------->

        <div class="row">
            <div class="col" style="text-align:justify">
                <asp:Label ID="LblQuestion" runat="server" Text="Questions"  CssClass="milabel" Font-Bold="True" Font-Italic="True" Font-Size="Large"></asp:Label>
                <br />
                <asp:DropDownList ID="DropDownQues" CssClass=" form-control-sm embed-responsive  dropdown-toggle active" runat="server" BorderColor="Black" Height="34px" Width="249px" DataTextField="DESC_QUESTION" DataValueField="DESC_QUESTION" Style="left: 0px; top: 0px" Font-Size="Medium">
                </asp:DropDownList>
            </div>

            <div class="col">
                <asp:Label ID="LblAczone" runat="server" Text="AC Zone:" CssClass="milabel" Font-Bold="True" Font-Italic="True" Font-Size="Large"></asp:Label>
                <br />
                <asp:DropDownList ID="DropDownACZ" CssClass=" form-control-sm  dropdown-toggle active" runat="server" Height="34px" Width="248px" BorderColor="Black" DataTextField="DESC_ZONE" DataValueField="DESC_ZONE" Font-Size="Medium">
                </asp:DropDownList>
            </div>

            <div class="col">
                <asp:Label ID="LblShift" runat="server" Text="Shift:" CssClass="milabel" Font-Bold="True" Font-Italic="True" Font-Size="Large"></asp:Label>
                <br />
                <asp:DropDownList ID="DropDownSHIFT" CssClass=" form-control-sm  dropdown-toggle active" runat="server" Height="34px" Width="248px" BorderColor="Black" DataTextField="SHIFT_D" DataValueField="SHIFT_D" Font-Size="Medium">
                </asp:DropDownList>
            </div>

            <div class="col"></div>
        </div>

        <br />

        <!-- --------------------- Diaeño y validaciones  de los campos  Unit, Quanity y Description----------------------->

        <div class="row">
            <div class="col">
                <asp:Label ID="LblUnit" runat="server" Text="Unit:" CssClass="milabel" Font-Bold="True" Font-Italic="True" Font-Size="Large"></asp:Label>
                <br />
                <asp:DropDownList ID="DropDownUnit" CssClass=" form-control-sm  dropdown-toggle active" runat="server" Height="34px" Width="248px" BorderColor="Black" DataTextField="MEASURED_UNIT" DataValueField="MEASURED_UNIT" Font-Size="Medium">
                </asp:DropDownList>
            </div>

            <div class="col">
                <asp:Label ID="LblQuantity" runat="server" Text="Quantity:" CssClass="milabel" Font-Bold="True" Font-Italic="True" Font-Size="Large"></asp:Label>
                <br />
                <asp:TextBox ID="TextBoxQuantity" CssClass="form-control-sm" runat="server" Height="34px" Width="248px" MaxLength="3" BorderColor="Black" Font-Size="Medium"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TextBoxQuantity" ErrorMessage="RequiredFieldValidator" ForeColor="Red" Display="Dynamic" ValidationGroup="grupoValidacion">*Enter  Quantity</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" placeholder="123" ControlToValidate="TextBoxQuantity" ErrorMessage="RegularExpressionValidator" ForeColor="Red" ValidationExpression="^[0-9]*$" ValidationGroup="grupoValidacion">Only  numbers</asp:RegularExpressionValidator>

            </div>

            <div class="col">
                <asp:Label ID="LblDescription" runat="server" Text="Description:" CssClass="milabel" Font-Bold="True" Font-Italic="True" Font-Size="Large"></asp:Label>
                <br />
                <asp:TextBox ID="TextBoxDesc" TextMode="MultiLine" CssClass="form-control-sm text-justify" runat="server" Height="34px" Width="248px" BorderColor="Black" Font-Size="Medium" MaxLength="500"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="TextBoxDesc" ErrorMessage="RequiredFieldValidator" ForeColor="Red" Display="Dynamic" ValidationGroup="grupoValidacion">*Enter a description </asp:RequiredFieldValidator>
            </div>

            <div class="col"></div>
        </div>

        <br />
        <!-- --------------------- Diseño del campo  imagen ----------------------->

        <asp:Image ID="ImgPreview" Width="200" ImageUrl="https://icons.iconarchive.com/icons/hopstarter/soft-scraps/256/Image-JPEG-icon.png" runat="server" />
        <asp:FileUpload ID="FileUpload1" CssClass="btn btn-secondary" runat="server" onchange="showImagePreview(this)" />
        <asp:Label ID="lblMessage" runat="server" ForeColor="White"></asp:Label>

        <br />
        <br />

        <!-- --------------------- Boton Insert  esta asociado a un grupo de validaciones ----------------------->

        <div class="row">
            <div class="col" style="text-align: right">
                <asp:Button ID="btnINSERT" runat="server" CssClass="btn btn-success" OnClick="btnINSERT_Click"   Text="SAVE" ValidationGroup="grupoValidacion" />
            </div>


            <div class="col-xs-2">
                <asp:Label ID="LblID" runat="server" Text="" ForeColor="White"></asp:Label>
                <asp:DropDownList ID="ddlID" CssClass=" form-control dropdown-toggle active" runat="server" Height="16px" Width="16px" AutoPostBack="True" OnSelectedIndexChanged="ddlID_SelectedIndexChanged" AppendDataBoundItems="True" CausesValidation="True">
                    <asp:ListItem Selected="True">---</asp:ListItem>
                </asp:DropDownList>
            </div>

            <!-- --------------------- Boton Update  esta asociado a un grupo de validaciones ----------------------->

            <div class="col">
                <asp:Button ID="btnUPDATE" runat="server" CssClass="btn btn-outline-warning"  OnClientClick="return confirm('Do you want to update this record?  ');" OnClick="btnUPDATE_Click" Text="..." ForeColor="White" ValidationGroup="grupoValidacion" ToolTip="Update" />
            </div>
        </div>

        <br />
        <br />

        <asp:GridView ID="GridView1" runat="server" align="Center" CssClass="table-responsive" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging1" HorizontalAlign="Center" PageSize="5" ForeColor="#333333" GridLines="None" CellPadding="4">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="ID_LPAFN" HeaderText="ID" />
                <asp:BoundField DataField="_DATE" HeaderText="Date" DataFormatString="{0:d}" />
                <asp:BoundField DataField="TAIL_NUMBER" HeaderText="Tail Number" />
                <asp:BoundField DataField="DESC_LINE" HeaderText="Line" />
                <asp:BoundField DataField="AUDITOR_ID" HeaderText="Auditor ID" />
                <asp:BoundField DataField="DESC_QUESTION" HeaderText="Question" />
                <asp:BoundField DataField="DESC_ZONE" HeaderText="Ac Zone" />
                <asp:BoundField DataField="QUANTITY" HeaderText="Quantity" />
                <asp:BoundField DataField="MEASURED_UNIT" HeaderText="Unit" />
                <asp:BoundField DataField="SHIFT_D" HeaderText="Shift" />
                <asp:BoundField DataField="FINDING_DESCRIPTION" HeaderText="Description" />
                <asp:TemplateField HeaderText="Evidence">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Image ID="Image1" CssClass="table-responsive" runat="server" Height="100px" Width="100px"
                            ImageUrl='<%#"data:Image/png;base64," +Convert.ToBase64String((byte[])Eval("ImageData")) %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />


            <%-- PERMITE UNA VALIDACION AL GRIDVIEW: SI NO CONSULTA NINGUN REGISTRO MUESTRA UN ICONO
            Y UN MENSAJE DE QUE NO EXISTEN REGISTROS CON EL ID DEL AUDITOR QUE 
            INICIO LA SESION.--%>
            <EmptyDataTemplate>
                <img src="https://icons.iconarchive.com/icons/ampeross/qetto-2/72/no-icon.png" alt="No existen registros" />
                <asp:Label ID="LblMessage" runat="server" CssClass="milabel" Text="THERE ARE NO RECORDS " Font-Bold="True" Font-Size="Large"></asp:Label>
            </EmptyDataTemplate>
        </asp:GridView>

    </form>
</body>
</html>
