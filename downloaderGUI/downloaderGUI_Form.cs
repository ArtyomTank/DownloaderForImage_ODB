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
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
	
	
	partial class Form1 : Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
 
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
 

		public Panel controlPanel;
		public Panel viewImagePanel;

		public Button downloadButton;
		
		public MainMenu mainMenu;
		public MenuItem[] menuItems;
		
		public StatusStrip statusStrip;
		public ToolStripProgressBar statusProgressBar;
		public ToolStripLabel statusLabel;

        private void InitializeComponent()
        {
			//
			//menuItems
			//
			menuItems = new MenuItem[3];
			for (int i = 0;i < menuItems.Length;i++)
			{
				menuItems[i] = new MenuItem();
				menuItems[i].Text = "item"+i;
			}
			
			//
			//mainMenu
			//
			mainMenu = new MainMenu();
			for (int i = 0;i < menuItems.Length;i++)
			{
				mainMenu.MenuItems.Add(menuItems[i]);
			}
			this.Menu = mainMenu;
			
			
			
			//
			//statusProgressBar
			//
			statusProgressBar = new ToolStripProgressBar();
			statusProgressBar.Minimum = 0;
			statusProgressBar.Maximum = 100;
			
			//
			//statusLabel
			//
			statusLabel = new ToolStripLabel();
			statusLabel.Text = "download progress:";
			
			//
			//statusStrip
			//
			statusStrip = new StatusStrip();
			statusStrip.Items.Add(statusLabel);
			statusStrip.Items.Add(statusProgressBar);
			this.Controls.Add(statusStrip);
			
			
			//
			//controlPanel
			//
			controlPanel = new Panel();
			controlPanel.Location = new Point(0,0);
			controlPanel.ClientSize = new Size(100,this.ClientSize.Height);
			controlPanel.BorderStyle = BorderStyle.Fixed3D;
			controlPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom;
			this.Controls.Add(controlPanel);
			
			//
			//downloadButton
			//
			downloadButton = new Button();
			downloadButton.Name = "downloadButton";
			downloadButton.Text = "download";
			downloadButton.Click += ButtonDownload_click;
			controlPanel.Controls.Add(downloadButton);
			
			
			//
			//viewImagePanel
			//
			viewImagePanel = new Panel();
			viewImagePanel.Right = 0;
			viewImagePanel.ClientSize = new Size(controlPanel.ClientSize.Width - controlPanel.Width,controlPanel.ClientSize.Height);
			controlPanel.BorderStyle = BorderStyle.FixedSingle;
			viewImagePanel.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
			this.Controls.Add(viewImagePanel);
			
			
			// 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 500);
            this.Name = "Form1";
            this.Text = "ImageDownloader";
            this.ResumeLayout(false);
        }
    }
	
	class ImageViewerForm : Form
	{
		private List<Image> listImage;
		public ImageViewerForm;
	}
}