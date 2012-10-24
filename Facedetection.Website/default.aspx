<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="FaceDetection.Website._default" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
</head>
<body>	
	<form id="form1" runat="server">
	<div id="images">
		<asp:Repeater runat="server" ID="repeaterImages" OnItemDataBound="RepeaterImagesItemDataBound">
			<ItemTemplate>
				<asp:Image runat="server" ID="imgOriginal"/>
				<asp:Image runat="server" ID="imgFace"/><br/>
				<asp:Literal runat="server" ID="literalFileName"></asp:Literal>
			</ItemTemplate>
			<SeparatorTemplate>
				<br/><br/>
			</SeparatorTemplate>
		</asp:Repeater>
	</div>
	<div id="fileupload">
		<asp:FileUpload runat="server" ID="fileUpload" />
		<asp:Button runat="server" ID="btnUpload" OnClick="btnUploadClick" Text="Upload" />
	</div>
	</form>
</body>
</html>
