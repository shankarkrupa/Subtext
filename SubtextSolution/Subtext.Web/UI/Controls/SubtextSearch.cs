#region Disclaimer/Info

///////////////////////////////////////////////////////////////////////////////////////////////////
// Subtext WebLog
// 
// Subtext is an open source weblog system that is a fork of the .TEXT
// weblog system.
//
// For updated news and information please visit http://subtextproject.com/
// Subtext is hosted at Google Code at http://code.google.com/p/subtext/
// The development mailing list is at subtext@googlegroups.com 
//
// This project is licensed under the BSD license.  See the License.txt file for more information.
///////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Subtext.Framework.Configuration;
using Subtext.Framework.Providers;

namespace Subtext.Web.UI.Controls
{
    /// <summary>
    ///	Implements a search control that can be added to a skin.
    /// </summary>
    public class SubtextSearch : BaseControl
    {
        protected Button btnSearch;
        protected Repeater SearchResults;
        protected TextBox txtSearch;

        private void Page_Load(object sender, EventArgs e)
        {
            if(txtSearch != null)
            {
                txtSearch.ValidationGroup = "SubtextSearch";
            }

            if(btnSearch != null)
            {
                btnSearch.ValidationGroup = "SubtextSearch";
            }
        }

        private void AttachCloseButton()
        {
            var closeLinkButton = FindControl("closeButton") as LinkButton;
            if(closeLinkButton != null)
            {
                closeLinkButton.Click += OnCloseClick;
                return;
            }

            var closeButton = FindControl("closeButton") as Button;
            if(closeButton != null)
            {
                closeButton.Click += OnCloseClick;
                return;
            }
        }

        void OnCloseClick(object sender, EventArgs e)
        {
            if(SearchResults != null)
            {
                SearchResults.Visible = false;
            }
        }

        public void btnSearch_Click(object sender, EventArgs e)
        {
            if(!String.IsNullOrEmpty(txtSearch.Text))
            {
                //fix for the blogs where only one installed
                int blogId = 0;
                if(Blog.Id > 0)
                {
                    blogId = Blog.Id;
                }

                var searchEngine = new SearchEngine(Blog, Url, Config.ConnectionString);
                ICollection<SearchResult> searchResults = searchEngine.Search(blogId, txtSearch.Text);

                SearchResults.DataSource = searchResults;
                SearchResults.DataBind();
            }
        }

        #region Web Form Designer generated code

        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            AttachCloseButton();
            base.OnInit(e);
        }

        /// <summary>
        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Load += this.Page_Load;
            this.btnSearch.Click += this.btnSearch_Click;
        }

        #endregion

        #region Nested type: PositionItems

        public class PositionItems
        {
            public PositionItems(string title, string url)
            {
                Title = title;
                this.url = url;
            }

            public string Title
            {
                get; private set;
            }

            public string url
            {
                get; set;
            }
        }

        #endregion
    }
}