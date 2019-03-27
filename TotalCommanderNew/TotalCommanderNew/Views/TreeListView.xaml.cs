using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TotalCommanderNew.Views
{
    /// <summary>
    /// Interaction logic for TreeListView.xaml
    /// </summary>
    public partial class TreeListView : UserControl
    {
        public List<TreeViewItem> Items { get; set; }

        public string Path { get; set; }
        public string Driver { get; set; }

        public TreeViewItem selectedItem = null;

        public string DestinationPath { get; set; }
        public List<TreeViewItem> SelectedItems { get; set; }

        public bool needRefresh = false;

        public TreeListView()
        {
            InitializeComponent();
            this.FolderView.ItemsSource = this.Items;
        }

        public TreeListView(string driver)
        {
            InitializeComponent();

            this.FolderView.ItemsSource = this.Items;
            LoadFromPath(driver);
            this.Path = driver;
            this.Driver = driver;
            SelectedItems = new List<TreeViewItem>();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //LoadFromDrivers();
        }

        private void LoadFromDrivers()
        {
            //Get every logical drive on the pc
            foreach (var drive in System.IO.Directory.GetLogicalDrives())
            {
                // Create a new item for it
                var item = new TreeViewItem()
                {
                    // Set the header
                    Header = drive,
                    // And the full path
                    Tag = drive
                };

                // Add a dummy item
                item.Items.Add(null);

                item.Expanded += Folder_Expanded;
                //Add to the main tree view
                FolderView.Items.Add(item);
            }
        }

        private void Folder_Expanded(object sender, RoutedEventArgs e)
        {
            var item = (TreeViewItem)sender;

            /* If the item only contains the dummy data */
            if ((item.Items.Count != 1 || item.Items[0] != null) && !needRefresh)
            {
                needRefresh = false;
                return;
            }

            /* Clear dummy data */
            item.Items.Clear();

            /* Get full path */
            var fullPath = (string)item.Tag;

            /* Create a blank list for directories */
            var directories = new List<string>();

            /* Try and get directories from the folder
               ignoring any issues doing so */
            try
            {
                var dirs = System.IO.Directory.GetDirectories(fullPath);

                if (dirs.Length > 0)
                    directories.AddRange(dirs);
            }
            catch { }

            /* For each directory... */
            directories.ForEach(directoryPath =>
            {
                /* Create directory item */
                var subItem = new TreeViewItem()
                {
                    /* Set header as folder name */
                    Header = GetFolderName(directoryPath),
                    /* And tag as full path */
                    Tag = directoryPath
                };

                /* Add dummy item so we can expand folder */
                subItem.Items.Add(null);

                /* Handle expanding */
                subItem.Expanded += Folder_Expanded;

                /* Add this item to the parent */
                item.Items.Add(subItem);
            });

            /* Create a blank list for files */
            var files = new List<string>();

            /* Try and get files from the folder
               ignoring any issues doing so */
            try
            {
                var fs = System.IO.Directory.GetFiles(fullPath);

                if (fs.Length > 0)
                    files.AddRange(fs);
            }
            catch { }

            /* For each file... */
            files.ForEach(filePath =>
            {
                /* Create file item */
                var subItem = new TreeViewItem()
                {
                    /* Set header as file name */
                    Header = GetFolderName(filePath),
                    /* And tag as full path */
                    Tag = filePath
                };

                /* Add this item to the parent */
                item.Items.Add(subItem);
            });
        }

        public void LoadFromPath(string path)
        {
            /* Clear dummy data */
            FolderView.Items.Clear();

            /* Get full path */
            var fullPath = path;

            /* Create a blank list for directories */
            var directories = new List<string>();

            /* Try and get directories from the folder
               ignoring any issues doing so */
            try
            {
                var dirs = System.IO.Directory.GetDirectories(fullPath);

                if (dirs.Length > 0)
                    directories.AddRange(dirs);
            }
            catch { }

            /* For each directory... */
            directories.ForEach(directoryPath =>
            {
                /* Create directory item */
                var subItem = new TreeViewItem()
                {
                    /* Set header as folder name */
                    Header = GetFolderName(directoryPath),
                    /* And tag as full path */
                    Tag = directoryPath
                };

                /* Add dummy item so we can expand folder */
                subItem.Items.Add(null);

                /* Handle expanding */
                subItem.Expanded += Folder_Expanded;

                /* Add this item to the parent */
                FolderView.Items.Add(subItem);
            });

            /* Create a blank list for files */
            var files = new List<string>();

            /* Try and get files from the folder
               ignoring any issues doing so */
            try
            {
                var fs = System.IO.Directory.GetFiles(fullPath);

                if (fs.Length > 0)
                    files.AddRange(fs);
            }
            catch { }

            /* For each file... */
            files.ForEach(filePath =>
            {
                /* Create file item */
                var subItem = new TreeViewItem()
                {
                    /* Set header as file name */
                    Header = GetFolderName(filePath),
                    /* And tag as full path */
                    Tag = filePath
                };

                /* Add this item to the parent */
                FolderView.Items.Add(subItem);
            });

            //FolderView.Items.Add(item);
        }

        public static string GetFolderName(string path)
        {
            // If we have no path, return empty    
            if (string.IsNullOrEmpty(path))
                return string.Empty;

            // Make all slashes back slashes
            var normalizedPath = path.Replace('/', '\\');

            // Find the last backslash in the path
            var lastIndex = normalizedPath.LastIndexOf('\\');

            // If we don't find a backslash, return the path itself
            if (lastIndex <= 0)
                return path;

            // Return the name after the last back slash
            return path.Substring(lastIndex + 1);
        }

        private void FolderView_MouseUp(object sender, MouseButtonEventArgs e)
        {
            TreeView treeView = (TreeView)sender;

            if (treeView != null)
            {
                TreeViewItem treeviewItem = treeView.SelectedItem as TreeViewItem;

                if (treeviewItem != null)
                {
                    if (e.ChangedButton == MouseButton.Right)
                    {
                        if (SelectedItems.Contains(treeviewItem))
                        {                            
                            SelectedItems.Remove(treeviewItem);
                        }
                        else
                        {
                            SelectedItems.Add(treeviewItem);
                        }
                    }
                    selectedItem = treeviewItem;
                    this.Path = GetPathFromItem(selectedItem);
                }
            }
        }

        private string GetPathFromItem(TreeViewItem item)
        {
            string path = "";
            while(true)
            {
                path = item.Header + "\\" + path;

                item = item.Parent as TreeViewItem;                
                if (item == null)
                {
                    break;
                }
            }
            path = this.Driver + path.Substring(0, path.Length-1);
            return path;
        }

        public void Edit()
        {
            string sourceFile = System.IO.Path.Combine(GetPathFromItem(selectedItem), (selectedItem as TreeViewItem).Tag.ToString());
            if (selectedItem.Tag.ToString().Contains("."))
            {
                Process.Start("notepad", sourceFile);
            }
        }

        public void Copy()
        {
            string sourceFile = System.IO.Path.Combine(GetPathFromItem(selectedItem), (selectedItem as TreeViewItem).Tag.ToString());
            string destFile = null;
            if (DestinationPath != null &&
                DestinationPath != "")
            {
                destFile = System.IO.Path.Combine(DestinationPath, selectedItem.Header.ToString());
            }

            if (DestinationPath != "" && DestinationPath != null)
            {
                if (SelectedItems.Count != 0)
                {
                    foreach (var item in SelectedItems)
                    {
                        sourceFile = System.IO.Path.Combine(GetPathFromItem(item), (item as TreeViewItem).Tag.ToString());
                        destFile = System.IO.Path.Combine(DestinationPath, item.Name);
                        System.IO.File.Copy(sourceFile, destFile, true);
                    }
                    SelectedItems.Clear();
                }
                else
                {
                    System.IO.File.Copy(sourceFile, destFile, true);
                }
            }
        }

        public void Move()
        {
            string sourceFile = System.IO.Path.Combine(GetPathFromItem(selectedItem), (selectedItem as TreeViewItem).Tag.ToString());
            string destFile = null;
            if (DestinationPath != null &&
                DestinationPath != "")
            {
                destFile = System.IO.Path.Combine(DestinationPath, selectedItem.Header.ToString());
            }

            if (DestinationPath != "" && DestinationPath != null)
            {
                if (SelectedItems.Count != 0)
                {
                    foreach (var item in SelectedItems)
                    {
                        sourceFile = System.IO.Path.Combine(GetPathFromItem(item), (item as TreeViewItem).Tag.ToString());
                        destFile = System.IO.Path.Combine(DestinationPath, item.Name);
                        System.IO.File.Move(sourceFile, destFile);
                    }
                    SelectedItems.Clear();
                }
                else
                {
                    System.IO.File.Move(sourceFile, destFile);
                }
                needRefresh = true;
                RefreshSelectedItem();
            }
        }

        public void Delete()
        {
            string sourceFile = System.IO.Path.Combine(GetPathFromItem(selectedItem), (selectedItem as TreeViewItem).Tag.ToString());
            string destFile = null;
            if (DestinationPath != null &&
                DestinationPath != "")
            {
                destFile = System.IO.Path.Combine(DestinationPath, selectedItem.Header.ToString());
            }

            if (SelectedItems.Count != 0)
            {
                foreach (var item in SelectedItems)
                {
                    sourceFile = System.IO.Path.Combine(GetPathFromItem(item), item.Tag.ToString());
                    if (item.Tag.ToString().Contains("."))
                    {
                        System.IO.File.Delete(sourceFile);
                    }
                    else
                    {
                        System.IO.Directory.Delete(sourceFile);
                    }
                }
                SelectedItems.Clear();
            }
            else
            {
                if (selectedItem.Tag.ToString().Contains("."))
                {
                    System.IO.File.Delete(sourceFile);
                }
                else
                {
                    System.IO.Directory.Delete(sourceFile);
                }
            }
            needRefresh = true;
            RefreshSelectedItem();
        }

        
        private void RefreshList()
        {
            
        }

        public void RefreshSelectedItem()
        {
            if (selectedItem != null)
            {
                TreeViewItem treeListView = selectedItem.Parent as TreeViewItem;
                TreeViewItem childTreeViewItem = selectedItem as TreeViewItem;

                if (treeListView != null && needRefresh)
                {
                    Folder_Expanded(treeListView, null);
                }
                if (childTreeViewItem != null)
                {
                    Folder_Expanded(childTreeViewItem, null);
                }
            }
        }

        private void FolderView_KeyDown(object sender, KeyEventArgs e)
        {
            TreeView treelistView = sender as TreeView;

            if (treelistView != null)
            {
                string sourceFile = System.IO.Path.Combine(GetPathFromItem(selectedItem), (selectedItem as TreeViewItem).Tag.ToString());
                string destFile = null;
                if (DestinationPath != null &&
                    DestinationPath != "")
                {
                    destFile = System.IO.Path.Combine(DestinationPath, selectedItem.Header.ToString());
                }

                switch (e.Key)
                {
                    case Key.F4: // Edit
                        {
                            Edit();
                            break;
                        }
                    case Key.F5: // Copy
                        {
                            Copy();
                            break;
                        }
                    case Key.F6: // Move
                        {
                            Move();
                            break;
                        }
                    case Key.F8: // Delete
                        {
                            Delete();
                            break;
                        }

                    default:
                        break;
                }
            }
        }
    }
}