using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TotalCommanderNew.Windows;

namespace TotalCommanderNew.Views
{
    /// <summary>
    /// Interaction logic for ListViewView.xaml
    /// </summary>
    public partial class ListViewView : UserControl
    {
        /* Variables and Propreties */
        public string DestinationPath { get; set; }

        private PathManager pathManager;
        public PathManager PathManager { get => pathManager; set => pathManager = value; }

        private List<ListItem> selectedItems;
        public List<ListItem> SelectedItems { get => selectedItems; set => selectedItems = value; }

        private string newFolderName;
        public string NewFolderName
        {
            get { return newFolderName; }
            set
            {
                newFolderName = value;
                CreateNewFolder(System.IO.Path.Combine(PathManager.Path, value));
                PathManager.RefreshList();
            }
        }

        private string newArchiveName;
        public string NewArchiveName
        {
            get { return newArchiveName; }
            set
            {
                newArchiveName = value;
                CreateNewArchive(System.IO.Path.Combine(DestinationPath, value));
                PathManager.RefreshList();
            }
        }

        /* Constructor */
        public ListViewView()
        {
            InitializeComponent();
            SelectedItems = new List<ListItem>();
        }

        public ListViewView(string Path)
        {
            InitializeComponent();
            PathManager = new PathManager(MainListView, Path);
            SelectedItems = new List<ListItem>();
        }

        /* Useful functions */
        public void UpdateCurrentPath(string path)
        {
            PathManager.Path = path;
            PathManager.PopulateListBox();
        }

        private void ResizeGridViewColumns(ListView listView)
        {
            GridView view = listView.View as GridView;

            if (view != null)
            {
                if (VisualChildrenCount > 0)
                {
                    Decorator border = VisualTreeHelper.GetChild(listView, 0) as Decorator;
                    if (border != null)
                    {
                        ScrollViewer scroller = border.Child as ScrollViewer;
                        if (scroller != null)
                        {
                            ItemsPresenter presenter = scroller.Content as ItemsPresenter;
                            if (presenter != null)
                            {
                                view.Columns[0].Width = presenter.ActualWidth;
                                for (int i = 1; i < view.Columns.Count; i++)
                                {
                                    if (view.Columns[0].Width - view.Columns[i].ActualWidth > 0)
                                    {
                                        view.Columns[0].Width -= view.Columns[i].ActualWidth;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        private bool isChildOf(TabControl control, ListView listView)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(control); i++)
            {
                var child = (ListView)VisualTreeHelper.GetChild(control, i);

                if (child.Equals(listView))
                {
                    return true;
                }
            }
            return false;
        }

        /* ListView */
        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListView listView = (ListView)sender;

            if (listView != null)
            {
                ListItem listItem = (ListItem)listView.SelectedItem;
                if (listItem != null)
                {
                    if (listItem.Name == MainWindow.BackSymbol)
                    {
                        PathManager.NavigateBack();
                        SelectedItems.Clear();
                    }
                    else if (listItem.Ext != "")
                    {
                        string sourceFile = System.IO.Path.Combine(PathManager.Path, listItem.Name);
                        Process.Start(sourceFile);
                    }
                    else
                    {
                        PathManager.NavigateTo(listItem.Name);
                        SelectedItems.Clear();
                    }
                }
            }
        }


        public void Edit()
        {
            ListItem listItem = MainListView.SelectedItem as ListItem;

            if (listItem == null)
            {
                return;
            }

            string sourceFile = System.IO.Path.Combine(PathManager.Path, listItem.Name);

            if (listItem.Ext != "" && listItem.Name != MainWindow.BackSymbol && SelectedItems.Count == 0)
            {
                Process.Start("notepad", sourceFile);
                pathManager.JustRefresh = true;
            }
        }

        public void Copy()
        {
            ListItem listItem = MainListView.SelectedItem as ListItem;

            if (listItem == null)
            {
                return;
            }

            string sourceFile = System.IO.Path.Combine(PathManager.Path, listItem.Name);
            string destFile = null;
            if (DestinationPath != null)
            {
                destFile = System.IO.Path.Combine(DestinationPath, listItem.Name);
            }

            if (DestinationPath != "" && DestinationPath != null)
            {
                if (SelectedItems.Count != 0)
                {
                    foreach (var item in SelectedItems)
                    {
                        sourceFile = System.IO.Path.Combine(PathManager.Path, item.Name);
                        destFile = System.IO.Path.Combine(DestinationPath, item.Name);

                        if (item.Ext != "")
                        {
                            System.IO.File.Copy(sourceFile, destFile, true);
                        }
                        else
                        {
                            System.IO.Directory.CreateDirectory(destFile);
                        }
                    }
                    SelectedItems.Clear();
                }
                else
                {
                    if (listItem.Ext != "")
                    {
                        System.IO.File.Copy(sourceFile, destFile, true);
                    }
                    else
                    {
                        System.IO.Directory.CreateDirectory(destFile); ;
                    }
                }
            }
        }

        public void Move()
        {
            ListItem listItem = MainListView.SelectedItem as ListItem;

            if (listItem == null)
            {
                return;
            }

            string sourceFile = System.IO.Path.Combine(PathManager.Path, listItem.Name);
            string destFile = null;
            if (DestinationPath != null)
            {
                destFile = System.IO.Path.Combine(DestinationPath, listItem.Name);
            }

            if (DestinationPath != "" && DestinationPath != null)
            {
                if (SelectedItems.Count != 0)
                {
                    foreach (var item in SelectedItems)
                    {
                        sourceFile = System.IO.Path.Combine(PathManager.Path, item.Name);
                        destFile = System.IO.Path.Combine(DestinationPath, item.Name);

                        if (item.Ext != "")
                        {
                            System.IO.File.Move(sourceFile, destFile);
                        }
                        else
                        {
                            System.IO.Directory.Move(sourceFile, destFile);
                        }
                    }
                    SelectedItems.Clear();
                }
                else
                {
                    if (listItem.Ext != "")
                    {
                        System.IO.File.Move(sourceFile, destFile);
                    }
                    else
                    {
                        System.IO.Directory.Move(sourceFile, destFile);
                    }
                }
            }
        }

        public void NewFolder()
        {
            NewFolderWindow newFolderWindow = new NewFolderWindow(this);
            newFolderWindow.ShowDialog();

            pathManager.JustRefresh = true;
        }

        public void Delete()
        {
            ListItem listItem = MainListView.SelectedItem as ListItem;

            if (listItem == null)
            {
                return;
            }

            string sourceFile = System.IO.Path.Combine(PathManager.Path, listItem.Name);
            string destFile = null;
            if (DestinationPath != null)
            {
                destFile = System.IO.Path.Combine(DestinationPath, listItem.Name);
            }

            if (SelectedItems.Count != 0)
            {
                foreach (var item in SelectedItems)
                {
                    sourceFile = System.IO.Path.Combine(PathManager.Path, item.Name);
                    if (item.Ext != "")
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
                if (listItem.Ext != "")
                {
                    System.IO.File.Delete(sourceFile);
                }
                else
                {
                    System.IO.Directory.Delete(sourceFile);
                }
            }
        }

        private void ListView_KeyDown(object sender, KeyEventArgs e)
        {
            ListView listView = (ListView)sender;

            if (listView != null)
            {
                ListItem listItem = MainListView.SelectedItem as ListItem;

                if (listItem == null)
                {
                    return;
                }

                string sourceFile = System.IO.Path.Combine(PathManager.Path, listItem.Name);
                string destFile = null;
                if (DestinationPath != null)
                {
                    destFile = System.IO.Path.Combine(DestinationPath, listItem.Name);
                }
                string targetPath = System.IO.Path.Combine(PathManager.Path, "New Folder");

                switch (e.Key)
                {
                    case Key.Enter:
                        {
                            if (listItem.Name == MainWindow.BackSymbol)
                            {
                                PathManager.NavigateBack();
                                SelectedItems.Clear();
                            }
                            else if (listItem.Ext != "")
                            {
                                Process.Start(sourceFile);
                            }
                            else
                            {
                                PathManager.NavigateTo(listItem.Name);
                                SelectedItems.Clear();
                            }
                            break;
                        }
                    case Key.R:
                        {
                            if (listItem.Name != MainWindow.BackSymbol && !SelectedItems.Contains(listItem))
                            {
                                SelectedItems.Add(listItem);
                                listItem.Color = "Red";                                
                            }
                            else if (SelectedItems.Contains(listItem))
                            {
                                SelectedItems.Remove(listItem);
                                listItem.Color = "Black";
                            }
                            pathManager.JustRefresh = true;
                            break;
                        }
                    case Key.Back:
                        {
                            ListItem firstItem = PathManager.Items.ElementAt(0);
                            if (firstItem.Name == MainWindow.BackSymbol)
                            {
                                PathManager.NavigateBack();
                                SelectedItems.Clear();
                            }
                            break;
                        }
                    case Key.F3: // View
                        {
                            break;
                        }
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
                    case Key.F7: // NewFolder
                        {
                            NewFolder();
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

        private void ListView_Loaded(object sender, RoutedEventArgs e)
        {
            ListView listView = (ListView)sender;

            if (listView != null)
            {
                ResizeGridViewColumns(listView);
            }
        }

        private void ListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ListView listView = (ListView)sender;

            if (listView != null)
            {
                ResizeGridViewColumns(listView);
            }
        }

        private void MainListView_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                ListView_KeyDown(sender, e);
            }
        }

        private void MainListView_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Right)
            {
                ListView listView = (ListView)sender;

                if (listView != null)
                {
                    ListItem listItem = listView.SelectedItem as ListItem;

                    if (listItem == null)
                    {
                        return;
                    }

                    if (listItem.Name != MainWindow.BackSymbol && !SelectedItems.Contains(listItem))
                    {
                        SelectedItems.Add(listItem);
                        listItem.Color = "Red";
                    }
                    else if (SelectedItems.Contains(listItem))
                    {
                        SelectedItems.Remove(listItem);
                        listItem.Color = "Black";
                    }
                }
                pathManager.JustRefresh = true;
                pathManager.RefreshList();
            }
        }

        /* Auxiliary Functions */

        private void CreateNewFolder(string targetPath)
        {
            System.IO.Directory.CreateDirectory(targetPath);
        }

        private void CreateNewArchive(string targetPath)
        {
            ZipFile zip = new ZipFile();
            foreach (var item in SelectedItems)
            {
                string path = System.IO.Path.Combine(PathManager.Path, item.Name);
                if (item.Ext != "")
                {
                    zip.AddFile(path);
                }
                else
                {
                    zip.AddDirectory(path);
                }
            }
            string destPath = targetPath;
            zip.Save(destPath);
            SelectedItems.Clear();

            PathManager.RefreshList();
        }

        public void UnpackArchive()
        {
            if (SelectedItems.Count != 0)
            {
                foreach (var item in SelectedItems)
                {
                    string sourceFile = System.IO.Path.Combine(PathManager.Path, item.Name);
                    string destFile = System.IO.Path.Combine(DestinationPath, item.Name.Substring(0, item.Name.Length - 4));

                    ZipFile zip = ZipFile.Read(sourceFile);
                    Directory.CreateDirectory(destFile);
                    zip.ExtractAll(destFile);
                }
            }
            else
            {
                ListItem selectedListItem = MainListView.SelectedItem as ListItem;
                if (selectedListItem == null || DestinationPath == null || DestinationPath == "")
                {
                    return;
                }

                string sourceFile = System.IO.Path.Combine(PathManager.Path, selectedListItem.Name);
                string destFile = System.IO.Path.Combine(DestinationPath, selectedListItem.Name.Substring(0, selectedListItem.Name.Length - 4));

                ZipFile zip = ZipFile.Read(sourceFile);
                Directory.CreateDirectory(destFile);
                zip.ExtractAll(destFile);
            }
        }
    }
}
