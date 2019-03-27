using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using TotalCommanderNew.Views;
using Ionic.Zip;
using TotalCommanderNew.Windows;

namespace TotalCommanderNew
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static readonly string BackSymbol = "[..]";
        public static readonly string DataPath = @"D:\Projects\An II - Sem 2\Medii Vizuale de Programare\TotalCommander\TotalCommander\";

        private bool isVerticalArranged;

        private bool isTree;
        public bool IsTree
        {
            get { return isTree; }
            set
            {
                if (isTree != value)
                {
                    isTree = value;
                    UpdateDisplayMode();
                }
            }
        }

        private TabControl focusedTabControl;
        private TabControl FocusedTabControl
        {
            get { return focusedTabControl; }
            set
            {
                TabItem leftCurrentTab = (TabItem)leftTabControl.SelectedItem;
                ListViewView leftListView = null;
                TreeListView leftTreeView = null;
                if (leftCurrentTab != null)
                {
                    leftListView = leftCurrentTab.Content as ListViewView;
                    leftTreeView = leftCurrentTab.Content as TreeListView;
                }

                TabItem rightCurrentTab = (TabItem)rightTabControl.SelectedItem;
                ListViewView rightListView = null;
                TreeListView rightTreeView = null;
                if (rightCurrentTab != null)
                {
                    rightListView = rightCurrentTab.Content as ListViewView;
                    rightTreeView = rightCurrentTab.Content as TreeListView;
                }

                /* LEFT -> LIST LIST */
                if (value == leftTabControl &&
                    leftListView != null &&
                    rightListView != null)
                {
                    leftListView.DestinationPath = rightListView.PathManager.Path;                                            
                }
                /* RIGHT -> LIST LIST */
                else if (value == rightTabControl &&
                        leftListView != null &&
                        rightListView != null)
                {
                    rightListView.DestinationPath = leftListView.PathManager.Path;
                }

                /* LEFT -> TREE TREE */
                else if (value == leftTabControl &&
                        leftTreeView != null &&
                        rightTreeView != null)
                {
                    leftTreeView.DestinationPath = rightTreeView.Path;
                }
                /* RIGHT -> TREE TREE */
                else if (value == rightTabControl &&
                        leftTreeView != null &&
                        rightTreeView != null)
                {
                    rightTreeView.DestinationPath = rightTreeView.Path;
                }

                /* LEFT -> TREE LIST */
                else if (value == leftTabControl &&
                        leftTreeView != null &&
                        rightListView != null)
                {
                    leftTreeView.DestinationPath = rightListView.PathManager.Path;
                }
                /* RIGHT -> TREE LIST */
                else if (value == rightTabControl &&
                        leftListView != null &&
                        rightTreeView != null)
                {
                    rightTreeView.DestinationPath = leftListView.PathManager.Path;
                }

                /* LEFT -> LIST TREE */
                else if (value == leftTabControl &&
                        leftListView != null &&
                        rightTreeView != null)
                {
                    leftListView.DestinationPath = rightTreeView.Path;
                }
                /* RIGHT -> LIST TREE */
                else if (value == rightTabControl &&
                        rightListView != null &&
                        leftTreeView != null)
                {
                    rightListView.DestinationPath = leftTreeView.Path;
                }

                focusedTabControl = value;
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            FocusedTabControl = leftTabControl;
            isVerticalArranged = false;
            IsTree = false;
        }

        /* Useful functions */
        void UpdateDisplayMode()
        {
            if (FocusedTabControl == null)
                return;

            string path = "C:\\";

            if (FocusedTabControl == leftTabControl && leftPartition.SelectedItem != null)
            {
                path = leftPartition.SelectedItem.ToString();
            }
            else if (rightPartition.SelectedItem != null)
            {
                path = rightPartition.SelectedItem.ToString();
            }

            TabItem currentTab = (TabItem)FocusedTabControl.SelectedItem;
            ListViewView listView = currentTab.Content as ListViewView;
            TreeListView treeView = currentTab.Content as TreeListView;
            if (listView != null && IsTree)
            {
                currentTab.Content = null;
                currentTab.Content = new TreeListView(path);
            }
            else if ( treeView != null && !IsTree )
            {
                currentTab.Content = null;
                currentTab.Content = new ListViewView(path);
            }

        }

        /* Menu Tabs */
        private void VerticalArrangement_Click(object sender, RoutedEventArgs e)
        {
            if (!isVerticalArranged)
            {
                isVerticalArranged = true;

                mainGrid.RowDefinitions[4].Height = new GridLength(1, GridUnitType.Star);

                Grid.SetRow(rightTabControl, 4);
                Grid.SetColumn(rightTabControl, 1);
                Grid.SetColumnSpan(rightTabControl, 3);

                Grid.SetColumnSpan(leftTabControl, 3);

                Grid.SetColumn(gridSplitter, 1);
                Grid.SetColumnSpan(gridSplitter, 3);
                Grid.SetRow(gridSplitter, 3);
                gridSplitter.Height = 2;
                gridSplitter.Width = Double.NaN;
                gridSplitter.HorizontalAlignment = HorizontalAlignment.Stretch;
                gridSplitter.VerticalAlignment = VerticalAlignment.Top;
            }
            else
            {
                isVerticalArranged = false;

                mainGrid.RowDefinitions[4].Height = new GridLength(20);

                Grid.SetRow(rightTabControl, 2);
                Grid.SetColumn(rightTabControl, 3);
                Grid.SetColumnSpan(rightTabControl, 1);

                Grid.SetColumnSpan(leftTabControl, 1);

                Grid.SetColumn(gridSplitter, 2);
                Grid.SetColumnSpan(gridSplitter, 1);
                Grid.SetRow(gridSplitter, 2);
                gridSplitter.Height = Double.NaN;
                gridSplitter.Width = 2;
                gridSplitter.HorizontalAlignment = HorizontalAlignment.Left;
                gridSplitter.VerticalAlignment = VerticalAlignment.Stretch;
            }
        }

        private void NewTab_Click(object sender, RoutedEventArgs e)
        {
            if (FocusedTabControl != null)
            {
                TabItem newTabItem = new TabItem();
                newTabItem.IsTabStop = false;

                if (FocusedTabControl == leftTabControl && leftPartition.SelectedItem != null)
                {
                    if ( IsTree )
                    {
                        newTabItem.Content = new TreeListView(leftPartition.SelectedItem.ToString());
                    }
                    else
                    {
                        newTabItem.Content = new ListViewView(leftPartition.SelectedItem.ToString());                        
                    }
                    newTabItem.Header = leftPartition.SelectedItem.ToString();
                    FocusedTabControl.Items.Add(newTabItem);
                }
                else if (rightPartition.SelectedItem != null)
                {
                    if ( IsTree )
                    {
                        newTabItem.Content = new TreeListView(rightPartition.SelectedItem.ToString());                        
                    }
                    else
                    {
                        newTabItem.Content = new ListViewView(rightPartition.SelectedItem.ToString());                        
                    }
                    newTabItem.Header = rightPartition.SelectedItem.ToString();
                    FocusedTabControl.Items.Add(newTabItem);
                }
            }
        }

        /* Partition ComboBox */
        private void PartitionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

            if (comboBox != null)
            {
                string driver = (string)comboBox.SelectedItem;
                TabItem tabItem = (TabItem)leftTabControl.Items.CurrentItem;

                if (tabItem != null)
                {
                    ((ListViewView)tabItem.Content).UpdateCurrentPath(driver);
                }
            }
        }

        private void PartitionComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

            if (comboBox != null)
            {
                DriveInfo[] drivers = DriveInfo.GetDrives().Where(var => var.IsReady).ToArray();
                
                foreach (var driver in drivers)
                {
                    comboBox.Items.Add(driver.Name);
                }
            }
        }

        private void TabControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TabControl control = (TabControl)sender;

            if (control != null)
            {
                ChangeButtonDisplay();
                FocusedTabControl = control;
                UpdatePathDisplays();
            }
        }

        private void TabControl_GotFocus(object sender, RoutedEventArgs e)
        {
            TabControl control = (TabControl)sender;

            if (control != null)
            {
                ChangeButtonDisplay();
                FocusedTabControl = control;
                UpdatePathDisplays();
            }
        }

        private void Tree_Click(object sender, RoutedEventArgs e)
        {
            IsTree = true;
        }

        private void Full_Click(object sender, RoutedEventArgs e)
        {
            IsTree = false;
        }

        private long GetDirectorySize(string p)
        {
            string[] a = Directory.GetFiles(p, "*.*");

            long b = 0;
            foreach (string name in a)
            {
                FileInfo info = new FileInfo(name);
                b += info.Length;
            }

            return b;
        }

        /* Buttons */
        private void ViewButton_Click(object sender, RoutedEventArgs e)
        {
            ListViewView listView = FocusedTabControl.SelectedContent as ListViewView;
            TreeListView treeListView = FocusedTabControl.SelectedContent as TreeListView;

            if (listView != null)
            {
                var currentItem = (listView.MainListView.SelectedItem as ListItem);
                var path = System.IO.Path.Combine(listView.PathManager.Path, currentItem.Name);
                MessageBox.Show(path + "\n" +
                                "Total space occupied:" + GetDirectorySize(path) + "bytes \n" +
                                currentItem.Date + "\n" +
                                currentItem.Size);
            }
            else if (treeListView != null)
            {
                MessageBox.Show(treeListView.Path + "\n" +
                                "Total space occupied:" + GetDirectorySize(treeListView.Path) + "bytes");
            }
            
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            ListViewView listView = FocusedTabControl.SelectedContent as ListViewView;
            TreeListView treeListView = FocusedTabControl.SelectedContent as TreeListView;

            if (listView != null)
            {
                listView.Edit();
            }
            else
            {
                treeListView.Edit();
            }
            RefreshTabs();
        }

        private void CopyButton_Click(object sender, RoutedEventArgs e)
        {
            ListViewView listView = FocusedTabControl.SelectedContent as ListViewView;
            TreeListView treeListView = FocusedTabControl.SelectedContent as TreeListView;

            if (listView != null)
            {
                listView.Copy();
            }
            else
            {
                treeListView.Copy();
            }
            RefreshTabs();
        }

        private void ModeButton_Click(object sender, RoutedEventArgs e)
        {
            ListViewView listView = FocusedTabControl.SelectedContent as ListViewView;
            TreeListView treeListView = FocusedTabControl.SelectedContent as TreeListView;

            if (listView != null)
            {
                listView.Move();                
            }
            else
            {
                treeListView.Move();
                treeListView.RefreshSelectedItem();
            }
            RefreshTabs();
        }

        private void NewFolderButton_Click(object sender, RoutedEventArgs e)
        {
            ListViewView listView = FocusedTabControl.SelectedContent as ListViewView;
            TreeListView treeListView = FocusedTabControl.SelectedContent as TreeListView;

            if (listView != null)
            {
                listView.NewFolder();
            }
            else
            {
                //treeListView.NewFolder();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            ListViewView listView = FocusedTabControl.SelectedContent as ListViewView;
            TreeListView treeListView = FocusedTabControl.SelectedContent as TreeListView;

            if (listView != null)
            {
                listView.Delete();
            }
            else
            {
                treeListView.Delete();
                treeListView.RefreshSelectedItem();
            }
            RefreshTabs();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }


        /* Useful Functions */
        private void RefreshTabs()
        {
            ListViewView leftList = leftTabControl.SelectedContent as ListViewView;
            ListViewView rightList = rightTabControl.SelectedContent as ListViewView;

            TreeListView leftTreeList = leftTabControl.SelectedContent as TreeListView;
            TreeListView rightTreeList = rightTabControl.SelectedContent as TreeListView;


            if (leftList != null)
            {
                leftList.PathManager.RefreshList();
            }
            if (rightList != null)
            {
                rightList.PathManager.RefreshList();
            }
            if (leftTreeList != null)
            {
                leftTreeList.LoadFromPath(leftTreeList.Driver);
            }
            if (rightTreeList != null)
            {
                rightTreeList.LoadFromPath(rightTreeList.Driver);
            }
            UpdatePathDisplays();
        }

        private void UpdatePathDisplays()
        {
            ListViewView leftList = leftTabControl.SelectedContent as ListViewView;
            ListViewView rightList = rightTabControl.SelectedContent as ListViewView;
            TabItem tabItem = FocusedTabControl.SelectedItem as TabItem;

            if (leftList != null)
            {
                LeftPathDisplay.Text = leftList.PathManager.Path;
                tabItem.Header = StringAfter(LeftPathDisplay.Text, @"\");
            }
            if (rightList != null)
            {
                RightPathDisplay.Text = rightList.PathManager.Path;
                tabItem.Header = StringAfter(RightPathDisplay.Text, @"\");
            }            
        }

        private string StringAfter(string value, string a)
        {
            int posA = value.LastIndexOf(a);
            if (posA == -1)
            {
                return value;
            }
            int adjustedPosA = posA + a.Length;
            if (adjustedPosA >= value.Length)
            {
                return value;
            }
            return value.Substring(adjustedPosA);
        }

        private void SetButtonDisplayOnFile()
        {
            ViewButton.IsEnabled = false;
            EditButton.IsEnabled = true;
        }

        private void SetButtonDisplayOnDirectory()
        {
            ViewButton.IsEnabled = true;
            EditButton.IsEnabled = false;
        }

        private void ChangeButtonDisplay()
        {
            ListViewView leftList = leftTabControl.SelectedContent as ListViewView;
            ListViewView rightList = rightTabControl.SelectedContent as ListViewView;

            TreeListView leftTreeList = leftTabControl.SelectedContent as TreeListView;
            TreeListView rightTreeList = rightTabControl.SelectedContent as TreeListView;


            if (leftList != null && leftList == FocusedTabControl.SelectedContent)
            {
                if (leftList.MainListView.SelectedItem != null
                    && (leftList.MainListView.SelectedItem as ListItem).Ext != "")
                {
                    SetButtonDisplayOnFile();
                }
                else
                {
                    SetButtonDisplayOnDirectory();
                }
            }
            if (rightList != null && rightList == FocusedTabControl.SelectedContent)
            {
                if (rightList.MainListView.SelectedItem != null
                    && (rightList.MainListView.SelectedItem as ListItem).Ext != "")
                {
                    SetButtonDisplayOnFile();
                }
                else
                {
                    SetButtonDisplayOnDirectory();
                }
            }
            if (leftTreeList != null && leftTreeList == FocusedTabControl.SelectedContent)
            {
                if (leftTreeList.FolderView.SelectedItem != null
                    && (leftTreeList.FolderView.SelectedItem as TreeViewItem).Header.ToString().Contains("."))
                {
                    SetButtonDisplayOnFile();
                }
                else
                {
                    SetButtonDisplayOnDirectory();
                }
            }
            if (rightTreeList != null && rightTreeList == FocusedTabControl.SelectedContent)
            {
                if (rightTreeList.FolderView.SelectedItem != null &&
                    (rightTreeList.FolderView.SelectedItem as TreeViewItem).Header.ToString().Contains("."))
                {
                    SetButtonDisplayOnFile();
                }
                else
                {
                    SetButtonDisplayOnDirectory();
                }
            }
        }


        private void TabControl_KeyDown(object sender, KeyEventArgs e)
        {
            RefreshTabs();
        }

        private void Pack_Click(object sender, RoutedEventArgs e)
        {
            if (FocusedTabControl != null)
            {
                ListViewView listView = FocusedTabControl.SelectedContent as ListViewView;

                if (listView != null)
                {
                    NewArchiveWindow archiveWindow = new NewArchiveWindow(listView);
                    archiveWindow.ShowDialog();
                    RefreshTabs();
                }
            }
        }

        private void Unpack_Click(object sender, RoutedEventArgs e)
        {
            if (FocusedTabControl != null)
            {
                ListViewView listView = FocusedTabControl.SelectedContent as ListViewView;

                if (listView != null)
                {
                    listView.UnpackArchive();
                }
                RefreshTabs();
            }
        }

        private void CompareByContent_Click(object sender, RoutedEventArgs e)
        {
            ListViewView leftListView = leftTabControl.SelectedContent as ListViewView;
            ListViewView rightListView = rightTabControl.SelectedContent as ListViewView;
            TreeListView leftTreeList = leftTabControl.SelectedContent as TreeListView;
            TreeListView rightTreeList = rightTabControl.SelectedContent as TreeListView;

            string leftPath = null, rightPath = null;
            string file1 = null, file2 = null;


            if (leftListView != null
                && leftListView.SelectedItems.Count == 1 
                && leftListView.SelectedItems.ElementAt(0).Ext == ".txt")
            {
                leftPath = leftListView.SelectedItems.ElementAt(0).Name;
                file1 = System.IO.Path.Combine(leftListView.PathManager.Path, leftPath);
            }
            else if (leftTreeList != null
                && leftTreeList.SelectedItems.Count == 1
                && leftTreeList.SelectedItems.ElementAt(0).Header.ToString().Contains("."))
            {
                leftPath = leftTreeList.SelectedItems.ElementAt(0).Name;
                file1 = System.IO.Path.Combine(leftTreeList.Path, leftPath);
            }

            if (rightListView != null
                && rightListView.SelectedItems.ElementAt(0).Ext == ".txt"
                && rightListView.SelectedItems.Count == 1)
            {
                rightPath = rightListView.SelectedItems.ElementAt(0).Name;
                file2 = System.IO.Path.Combine(rightListView.PathManager.Path, rightPath);
            }
            else if (rightTreeList != null
                && rightTreeList.SelectedItems.Count == 1
                && rightTreeList.SelectedItems.ElementAt(0).Header.ToString().Contains("."))
            {
                rightPath = rightTreeList.SelectedItems.ElementAt(0).Name;
                file2 = System.IO.Path.Combine(rightTreeList.Path, rightPath);
            }

            CompareFilesWindow compareWindow = new CompareFilesWindow(file1, file2);
            compareWindow.ShowDialog();
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Nume: Ilie Florian-Cristian \nGrupa: 10LF372 \nLink e-mail: florian.ilie@student.unitbv.ro");
        }
    }
}
