using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TotalCommanderNew
{
    public partial class PathManager
    {
        /* Properties */
        public string Path { get; set; }
        public DirectoryInfo[] Directories { get; set; }
        public FileInfo[] Files { get; set; }

        public ListBox ListBox { get; set; }
        public List<ListItem> Items { get; set; }

        private static int currentUniqueID = 0;
        private readonly string IdleColor = "Black";

        public bool JustRefresh { get; set; }

        /* Constructors */
        public PathManager(ListBox listBox, string path)
        {
            this.Items = new List<ListItem>();
            this.ListBox = listBox;
            this.ListBox.ItemsSource = this.Items;
            this.Path = path;

            this.PopulateListBox();
        }

        /* Useful Functions */
        public static int GetNextID()
        {
            return currentUniqueID++;
        }

        public void RefreshList()
        {
            if (JustRefresh)
            {
                JustRefresh = false;
                this.ListBox.Items.Refresh();
            }
            else
            {
                PopulateListBox();
            }
        }

        public void PopulateListBox()
        {
            /* If the Items list contains any value then Clear the data */
            if (this.Items.Count > 0)
            {
                this.Items.Clear();
            }

            DirectoryInfo directoryInfo = new DirectoryInfo(this.Path);

            if (directoryInfo.Extension != "")
            {
                return;
            }

            /* Don't forget to add the Back Button */
            this.Items.Add(new ListItem { Name = MainWindow.BackSymbol, ImgPath = MainWindow.DataPath + "back.png"});

            /* Populate the list with the Directories from the corresponding path folder */
            this.Directories = directoryInfo.GetDirectories("*");
            foreach (DirectoryInfo directorie in this.Directories)
            {
                this.Items.Add(new ListItem { Name = directorie.Name,
                                              ImgPath = MainWindow.DataPath + "folder.png",
                                              Ext = directorie.Extension,
                                              Size = "<DIR>",
                                              Date = directorie.CreationTime.ToString(),
                                              Color = IdleColor });
            }
            //Icon iconForFile = SystemIcons.WinLogo;

            /* Populate the list with the Files from the corresponding path folder */
            this.Files = directoryInfo.GetFiles("*");
            foreach (FileInfo file in this.Files)
            {
                if (file.Extension == ".txt")
                {
                    this.Items.Add(new ListItem { Name = file.Name,
                                                  ImgPath = MainWindow.DataPath + "text.png",
                                                  Ext = file.Extension,
                                                  Size = file.Length.ToString(),
                                                  Date = file.CreationTime.ToString(),
                                                  Color = IdleColor });
                }
                else
                {
                    this.Items.Add(new ListItem { Name = file.Name,
                                                  ImgPath = MainWindow.DataPath + "file.png",
                                                  Ext = file.Extension,
                                                  Size = file.Length.ToString(),
                                                  Date = file.CreationTime.ToString(),
                                                  Color = IdleColor });
                }
            }

            this.ListBox.Items.Refresh();
        }

        public void NavigateTo(string directoryName)
        {
            string currentPath;
            if (this.Path.EndsWith(@":\"))
            {
                currentPath = this.Path + directoryName;
            }
            else
            {
                currentPath = this.Path + @"\" + directoryName;
            }

            DirectoryInfo directory = new DirectoryInfo(currentPath);
            if (directory.Extension == String.Empty)
            {
                this.Path = currentPath;
                this.PopulateListBox();
            }
        }

        public void NavigateBack()
        {
            if (this.Path.Length > 0)
            {
                if (this.Path.EndsWith(":\\") || this.Path.EndsWith(@":\"))
                {
                    return;
                }

                string currentPath = this.Path.Substring(0, this.Path.LastIndexOf(@"\"));

                DirectoryInfo directory = new DirectoryInfo(currentPath);
                if (directory.Extension == String.Empty)
                {
                    this.Path = currentPath;
                    this.PopulateListBox();
                }
            }
        }
    }
}
