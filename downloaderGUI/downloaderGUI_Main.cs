//css_inc downloaderGUI_Form.cs
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net;
using System.IO;
 
namespace downloaderGUIApp
{
	public partial class Form1 : Form
    {
		static DateTime dt;
		static string url = @"https://d3jmhkxtuu3mlt.cloudfront.net/files/share_odb_";
		static string day;
		static string mounth;
        static string year;
		static string FullUrl; 
		static string FileName;
		
        public Form1()
        {
            InitializeComponent();
        }
		
		public void ButtonDownload_click(Object sender, EventArgs e)
		{
			DownloaderAsync();
		}
		
		private void DownloadProgressCallback(object sender, DownloadProgressChangedEventArgs e)
		{
			// Displays the operation identifier, and the transfer progress.
			if (!statusProgressBar.Visible) statusProgressBar.Visible = true;
			statusProgressBar.Value = e.ProgressPercentage;
			statusLabel.Text = String.Format("{0} downloaded {1} of {2} Kb. {3} % complete", FileName, e.BytesReceived/1024, e.TotalBytesToReceive/1024, e.ProgressPercentage);
		}
		private void DownloadProgressComplete(object sender, AsyncCompletedEventArgs e)
		{
			// Displays the operation identifier, and the transfer progress.
			statusProgressBar.Value = 0;
			statusProgressBar.Visible = false;
			statusLabel.Text = FileName + " complete";
		}
		
		public async void DownloaderAsync()
		{
			await Task.Run(new Action(Downloader));
		}
		
		public async void Downloader()
		{
			WebClient client = new WebClient();
			client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressCallback);
			client.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadProgressComplete);
			dt = new DateTime();
			dt = DateTime.Now;
			dt = dt.AddDays(-6);
			
			for (int i=0;i<7;i++)
			{
				day = dt.Day.ToString();
				mounth = dt.Month.ToString();
				year = dt.Year.ToString();
				
				day = (Convert.ToInt32(day) < 10)? "0"+day:day;
				FileName = "share_odb_"+ year + "-" + mounth + "-" + day.ToString() +".jpg";
				FullUrl = url + year + "-" + mounth + "-" + day + ".jpg";
				
				try
				{
					await client.DownloadFileTaskAsync(new Uri(FullUrl) ,FileName );
					await Task.Delay(500);
				}
				catch(Exception ex)
				{
					string s = ex.InnerException + Environment.NewLine + ex.Message + Environment.NewLine + ex.Source;
					MessageBox.Show(s);
					LoggerAsync(ex);
				}
				
				dt = dt.AddDays(1);
			}
		}
		
		static async void  LoggerAsync(Exception ex)
		{
			string writePath = @"log.txt";
            try
            {
                using (StreamWriter sw = new StreamWriter(writePath, true, System.Text.Encoding.Default))
                {
					await sw.WriteLineAsync(DateTime.Now.ToString());
                    await sw.WriteLineAsync(ex.Message.ToString());
					//await sw.WriteLineAsync(ex.InnerException.ToString());
					await sw.WriteLineAsync(ex.Source.ToString());
					await sw.WriteLineAsync(ex.TargetSite.ToString());
					
					await sw.WriteLineAsync(FullUrl);
					await sw.WriteLineAsync(FileName);
					await sw.WriteLineAsync(Environment.NewLine);
                }
            }
            catch (Exception e)
            {
				/*
                Console.WriteLine(e.Message);
				Console.WriteLine(e.TargetSite);
				*/
				string s = e.InnerException + Environment.NewLine + e.Message + Environment.NewLine + e.Source;
				MessageBox.Show(s);
            }
		}
    }
}