<%@ Control Language="C#" EnableTheming="false"  Inherits="Subtext.Web.UI.Controls.MyLinks" %>
<div id="myLinks">
	<div class="title">
		Site Sections
	</div>
	<div class="links">
		<ul class="tab">
			<li>
				<st:NavigationLink ActiveCssClass="active" cssclass="Home" runat="server" navigateurl="~/Default.aspx" text="Home" id="HomeLink" /></li>
			<li>
				<st:NavigationLink ActiveCssClass="active" cssclass="archives" runat="server" navigateurl="~/Archives.aspx" text="Archives"	id="Archives" visible="False" />
			</li>
			<li>
				<st:NavigationLink ActiveCssClass="active" cssclass="Contact" runat="server" navigateurl="~/Contact.aspx" text="Contact" id="ContactLink" /></li>
			<li>
				<asp:HyperLink cssclass="Syndication" runat="server" text="Syndication"
					id="Syndication" />
			</li>
			<li>
				<asp:HyperLink cssclass="Admin" runat="server" text="Admin" id="Admin" />
			</li>
		</ul>
	</div>
	<asp:HyperLink runat="server" Visible="False" id="XMLLink" />
	
</div>
