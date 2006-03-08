#region Disclaimer/Info
///////////////////////////////////////////////////////////////////////////////////////////////////
// Subtext WebLog
// 
// Subtext is an open source weblog system that is a fork of the .TEXT
// weblog system.
//
// For updated news and information please visit http://subtextproject.com/
// Subtext is hosted at SourceForge at http://sourceforge.net/projects/subtext
// The development mailing list is at subtext-devs@lists.sourceforge.net 
//
// This project is licensed under the BSD license.  See the License.txt file for more information.
///////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Subtext.Framework.Data;
using Subtext.Framework.Logging;
using Subtext.Web.Admin.WebUI;
using Subtext.Web.Controls;

namespace Subtext.Web.Admin.Pages
{
	public class ErrorLog : AdminPage
	{
		protected AdvancedPanel Log;
		protected Page PageContainer;
		protected Repeater LogPage;
		protected HtmlGenericControl NoMessagesLabel;
		protected Pager LogPager;
		protected Button btnExportToExcel;
		protected Button btnClearLog;

		private int _logPageNumber;
	
		protected override void OnLoad(EventArgs e)
		{
			LoadPage();
			base.OnLoad (e);
		}


		private void LoadPage()
		{
			if (null != Request.QueryString[Keys.QRYSTR_PAGEINDEX])
				_logPageNumber = Convert.ToInt32(Request.QueryString[Keys.QRYSTR_PAGEINDEX]);

			LogPager.PageSize = Preferences.ListingItemCount;
			LogPager.PageIndex = _logPageNumber;

			BindLocalUI();
			BindList();
		}

		private void BindList()
		{
			PagedLogEntryCollection logEntries = LoggingProvider.Instance().GetPagedLogEntries(LogPager.PageIndex, LogPager.PageSize, SortDirection.Descending);
			LogPager.ItemCount = logEntries.MaxItems;
			LogPage.DataSource = logEntries;
			LogPage.DataBind();		
		}

		
		private void BindLocalUI()
		{
			HyperLink lnkReferrals = Utilities.CreateHyperLink("Referrals", "Referrers.aspx");
			HyperLink lnkViews		= Utilities.CreateHyperLink("Views", "StatsView.aspx");
			HyperLink lnkErrorLog	= Utilities.CreateHyperLink("Error Log", "ErrorLog.aspx");


			// Add the buttons to the PageContainer.
			PageContainer.AddToActions(lnkReferrals);
			PageContainer.AddToActions(lnkViews);
			PageContainer.AddToActions(lnkErrorLog);

		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.btnClearLog.Click += new EventHandler(this.btnClearLog_Click);
			this.btnExportToExcel.Click +=new EventHandler(btnExportToExcel_Click);
		}
		#endregion

		private void btnClearLog_Click(object sender, EventArgs e)
		{
			LoggingProvider.Instance().ClearLog();
			LogPager.PageIndex = 0; //Back to first page.
			BindList();
		}

		private void BindListForExcel()
		{
			PagedLogEntryCollection logEntries = LoggingProvider.Instance().GetPagedLogEntries(1, int.MaxValue - 1, SortDirection.Descending);
			LogPage.DataSource = logEntries;
			LogPage.DataBind();
		}

		private void btnExportToExcel_Click(object sender, EventArgs e)
		{
			BindListForExcel();
			ControlHelper.ExportToExcel(this.LogPage, "SubtextErrorLog.xls");
		}
	}
}
