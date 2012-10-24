using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace FaceDetection.Website
{
	/// <summary>
	/// 	Summary description for image
	/// </summary>
	public class image : IHttpHandler
	{
		private HttpContext _httpContext;

		public void ProcessRequest(HttpContext context)
		{
			_httpContext = context;
			var imageName = context.Request.QueryString["image"];
			var filePath = context.Server.MapPath("upload") + "\\" + imageName;
			var bitmapFile = new Bitmap(filePath);

			AddFace(bitmapFile);
		}

		public void AddFace(Bitmap image)
		{
			var faceImage = DetectFace(image);
			if (faceImage != null)
			{
				var stream = new MemoryStream();
				faceImage.Save(stream, ImageFormat.Bmp);
				stream.Position = 0;
				byte[] data = new byte[stream.Length];
				stream.Read(data, 0, (int) stream.Length);

				_httpContext.Response.Clear();
				_httpContext.Response.ContentType = "image/jpeg";
				_httpContext.Response.BinaryWrite(data);
			}
		}

		private Bitmap DetectFace(Bitmap faceImage)
		{
			var image = new Image<Bgr, byte>(faceImage);
			var gray = image.Convert<Gray, Byte>();
			var haarCascadeFilePath = _httpContext.Server.MapPath("haarcascade_frontalface_default.xml");
			var face = new HaarCascade(haarCascadeFilePath);
			MCvAvgComp[][] facesDetected = gray.DetectHaarCascade(face, 1.1, 10, HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, new Size(20, 20));
			Image<Gray, byte> result = null;

			foreach (MCvAvgComp f in facesDetected[0])
			{
				//draw the face detected in the 0th (gray) channel with blue color
				image.Draw(f.rect, new Bgr(Color.Blue), 2);
				result = image.Copy(f.rect).Convert<Gray, byte>();
				break;
			}

			if (result != null)
			{
				result = result.Resize(150, 150, INTER.CV_INTER_CUBIC);

				return result.Bitmap;
			}

			return null;
		}

		public bool IsReusable
		{
			get { return false; }
		}
	}
}