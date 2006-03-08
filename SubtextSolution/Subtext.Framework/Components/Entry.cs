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
using System.Xml.Serialization;
using Subtext.Extensibility;
using Subtext.Framework.Configuration;

namespace Subtext.Framework.Components
{
	/// <summary>
	/// Summary description for Entry.
	/// </summary>
	[Serializable]
	public class Entry
	{
		/// <summary>
		/// Creates a new <see cref="Entry"/> instance.
		/// </summary>
		/// <param name="ptype">Ptype.</param>
		public Entry(PostType ptype)
		{
			this.PostType = ptype;
		}

		private int _blogID;
		/// <summary>
		/// Gets or sets the blog ID.
		/// </summary>
		/// <value>The blog ID.</value>
		public int BlogId
		{
			get {return this._blogID;}
			set {this._blogID = value;}
		}

		private int _entryid = NullValue.NullInt32;
		/// <summary>
		/// Gets or sets the entry ID.
		/// </summary>
		/// <value>The entry ID.</value>
		[XmlAttribute]
		public int EntryID
		{
			get{return _entryid;}
			set{_entryid = value;}
		}

		private int _parentID = NullValue.NullInt32;		
		/// <summary>
		/// Gets or sets the parent ID.
		/// </summary>
		/// <value>The parent ID.</value>
		public int ParentID
		{
			get
			{
				return this._parentID;
			}
			set
			{
				this._parentID = value;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance is updated.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is updated; otherwise, <c>false</c>.
		/// </value>
		public bool IsUpdated
		{
			get
			{
				return DateCreated != DateUpdated;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance has description.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance has description; otherwise, <c>false</c>.
		/// </value>
		public bool HasDescription
		{
			get
			{
				return ((Description != null) && (Description.Trim().Length > 0));
			}
		}

		/// <summary>
		/// Gets a value indicating whether this entry 
		/// has an actual Title URL that's different from 
		/// the Link (meaning the user overrode the title 
		/// url).
		/// </summary>
		/// <value>
		/// 	<c>true</c> if [has title URL]; otherwise, <c>false</c>.
		/// </value>
		public bool HasTitleUrl
		{
			get
			{
				return ((TitleUrl != null) && (TitleUrl != Link));
			}
		}

		private PostType _postType = PostType.None;
		/// <summary>
		/// Gets or sets the type of the post.
		/// </summary>
		/// <value>The type of the post.</value>
		public virtual PostType PostType
		{
			get
			{
				return _postType;
			}
			set
			{
				_postType = value;
			}
		}

		private string _description;
		public string Description
		{
			get{return _description;}
			set{_description = value;}
		}

		private string _entryName;
		public string EntryName
		{
			get{return _entryName;}
			set{_entryName = value;}
		}

		public bool HasEntryName
		{
			get
			{
				return this.EntryName != null && this.EntryName.Trim().Length >0;
			}
		}

		private string _title;
		public string Title
		{
			get{return _title;}
			set{_title = value;}
		}

		private string _titleurl;
		/// <summary>
		/// Gets or sets the URL the Title of an entry will 
		/// link to.  For comments, this is the URL the commenter 
		/// specifies.
		/// </summary>
		/// <value></value>
		public string TitleUrl
		{
			get
			{
				if(_titleurl == null)
				{
					return _link;
				}
				return _titleurl;
			}
			set
			{
				_titleurl = value;
			}
		}

		private string _body;
		/// <summary>
		/// Gets or sets the body of the Entry.  This is the 
		/// main content of the entry.
		/// </summary>
		/// <value></value>
		public virtual string Body
		{
			get
			{
				return _body;
			}
			set
			{
				_body = value;
				this._contentChecksumHash = string.Empty;
			}
		}

		private string _sourceurl;
		/// <summary>
		/// Gets or sets the source URL.  For comments, this is the URL 
		/// to the comment form used if any. For trackbacks, this is the 
		/// url of the site making the trackback.
		/// the 
		/// </summary>
		/// <value>The source URL.</value>
		public string SourceUrl
		{
			get{return _sourceurl;}
			set{_sourceurl= value;}
		}

		private string _sourcename;
		/// <summary>
		/// Gets or sets the name of the source.  For comments this is the 
		/// IP address of the commenter.
		/// </summary>
		/// <value>The name of the source.</value>
		public string SourceName
		{
			get{return _sourcename;}
			set{_sourcename= value;}
		}

		private string _author;
		/// <summary>
		/// Gets or sets the author name of the entry.  
		/// For comments, this is the name given by the commenter. 
		/// </summary>
		/// <value>The author.</value>
		public string Author
		{
			get{return _author;}
			set{_author = value;}
		}

		private string _email;
		public string Email
		{
			get{return _email;}
			set{_email = value;}
		}

		private DateTime _datecreated = NullValue.NullDateTime;
		/// <summary>
		/// Gets or sets the date this item was created.
		/// </summary>
		/// <value></value>
		public DateTime DateCreated
		{
			get
			{
				return _datecreated;
			}
			set{_datecreated = value;}
		}

		private DateTime _dateupated = NullValue.NullDateTime;
		/// <summary>
		/// Gets or sets the date this entry was last updated.
		/// </summary>
		/// <value></value>
		public DateTime DateUpdated
		{
			get
			{
				return _dateupated;
			}
			set{_dateupated = value;}
		}

		/// <summary>
		/// Gets or sets the date the item was published.
		/// </summary>
		/// <value></value>
		public DateTime DateSyndicated
		{
			get { return _dateSyndicated; }
			set { _dateSyndicated = value; }
		}

		DateTime _dateSyndicated = NullValue.NullDateTime;

		public bool IsActive
		{
			get{return EntryPropertyCheck(PostConfig.IsActive);}
			set{PostConfigSetter(PostConfig.IsActive,value);}
		}

		public bool IsXHMTL
		{
			get{return EntryPropertyCheck(PostConfig.IsXHTML);}
			set{PostConfigSetter(PostConfig.IsXHTML,value);}
		}

		public bool AllowComments
		{
			get{return EntryPropertyCheck(PostConfig.AllowComments);}
			set{PostConfigSetter(PostConfig.AllowComments,value);}
		}

		public bool DisplayOnHomePage
		{
			get{return EntryPropertyCheck(PostConfig.DisplayOnHomePage);}
			set{PostConfigSetter(PostConfig.DisplayOnHomePage,value);}
		}

		public bool SyndicateDescriptionOnly
		{
			get{return EntryPropertyCheck(PostConfig.SyndicateDescriptionOnly);}
			set{PostConfigSetter(PostConfig.SyndicateDescriptionOnly,value);}
		}

		public bool IncludeInMainSyndication
		{
			get{return EntryPropertyCheck(PostConfig.IncludeInMainSyndication);}
			set{PostConfigSetter(PostConfig.IncludeInMainSyndication,value);}
		}

		public bool IsAggregated
		{
			get{return EntryPropertyCheck(PostConfig.IsAggregated);}
			set{PostConfigSetter(PostConfig.IsAggregated,value);}
		}

		/// <summary>
		/// True if comments have been closed. Otherwise false.  Comments are closed 
		/// after a certain number of days.
		/// </summary>
		public bool CommentingClosed
		{
			get
			{
				if(Config.CurrentBlog.DaysTillCommentsClose == int.MaxValue)
					return false;

				return DateTime.Now > this.DateCreated.AddDays(Config.CurrentBlog.DaysTillCommentsClose);
			}
		}

		private string _link;
		public virtual string Link
		{
			get
			{
				return _link;
			}
			set
			{
				_link = value;
			}
		}

		/// <summary>
		/// This is a checksum of the entry text combined with 
		/// a hash of the text like so "####.HASH". 
		/// </summary>
		/// <value></value>
		public string ContentChecksumHash
		{
			get
			{
				if(_contentChecksumHash.Length == 0)
				{
					_contentChecksumHash = CalculateChecksum(this.Body) + "." + Security.HashPassword(this.Body);
				}
				return _contentChecksumHash;
			}
			set { _contentChecksumHash = value; }
		}

		string _contentChecksumHash = string.Empty;

		private int _feedBackCount = 0;
		public int FeedBackCount
		{
			get{return _feedBackCount;}
			set{_feedBackCount = value;}
		}

		private PostConfig _PostConfig = PostConfig.None;
		
		public PostConfig PostConfig
		{
			get {return this._PostConfig;}
			set {this._PostConfig = value;}
		}

		protected bool EntryPropertyCheck(PostConfig ep)
		{
			return (this.PostConfig & ep) == ep;
		}

		protected void PostConfigSetter(PostConfig ep, bool select)
		{
			if(select)
			{
				this.PostConfig = PostConfig | ep;
			}
			else
			{
				this.PostConfig = PostConfig & ~ep;
			}
		}

		/// <summary>
		/// Calculates a simple checksum of the specified text.  
		/// This is used for comment filtering purposes. 
		/// Once deployed, this algorithm shouldn't change.
		/// </summary>
		/// <param name="text">Text.</param>
		/// <returns></returns>
		public static int CalculateChecksum(string text)
		{
			int checksum = 0;
			foreach(char c in text)
			{
				checksum += (int)c;
			}
			return checksum;
		}

	}
}
