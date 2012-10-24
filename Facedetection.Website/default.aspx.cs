using System;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FaceDetection.Website
{
	public partial class _default : Page
	{
		private const string UploadDir = "upload";
		private readonly string _uploadDirPath;

		public _default()
		{
			_uploadDirPath = Server.MapPath(UploadDir);
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				BindImages();
			}
		}

		private void BindImages()
		{
			var dirInfo = new DirectoryInfo(_uploadDirPath);
			var files = dirInfo.GetFiles();

			repeaterImages.DataSource = files;
			repeaterImages.DataBind();
		}

		protected void btnUploadClick(object sender, EventArgs e)
		{
			if (!fileUpload.HasFile)
			{
				return;
			}
			var fileUploadPath = _uploadDirPath + "\\" + fileUpload.FileName;
			fileUpload.SaveAs(fileUploadPath);

			BindImages();
		}

		protected void RepeaterImagesItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e == null)
			{
				return;
			}

			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				var item = (FileInfo)e.Item.DataItem;
				var literalFileName = (Literal)e.Item.FindControl("literalFileName");
				var imgFace = (Image)e.Item.FindControl("imgFace");
				var imgOriginal = (Image)e.Item.FindControl("imgOriginal");

				literalFileName.Text = item.Name;
				imgOriginal.ImageUrl = UploadDir + "\\" + item.Name;
				imgFace.ImageUrl = "/image.ashx?image=" + item.Name;
			}
		}
	}
}