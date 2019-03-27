using DiffPlex;
using DiffPlex.DiffBuilder;
using DiffPlex.DiffBuilder.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TotalCommanderNew.Views;

namespace TotalCommanderNew.Windows
{
    /// <summary>
    /// Interaction logic for CompareFilesWindow.xaml
    /// </summary>
    public partial class CompareFilesWindow : Window
    {
        public CompareFilesWindow(string file1, string file2)
        {
            InitializeComponent();
            CompareByContent(file1, file2);
        }

        private void CompareByContent(string file1, string file2)
        {
            LeftFile.Text = "";
            RightFile.Text = "";

            var file1Lines = File.ReadAllLines(file1);
            var file2Lines = File.ReadAllLines(file2);

            /* Iterate through th First File Lines */
            for (int i = 0; i < file1Lines.Length; i++)
            {
                var line1 = file1Lines[i].Split(' ');
                var line2 = file2Lines[i].Split(' ');

                System.Collections.Generic.List<int> sameIndex = new System.Collections.Generic.List<int>();
                /* For each Word of the First File Line */
                foreach (var word in line1)
                {
                    if (line2.Contains(word))
                    {
                        /* Get the first index of the Ocurred Word */
                        int firstIndex = 0;
                        for (int k = 0; k < line2.Length; k++)
                        {
                            if (line2[k] == word)
                            {
                                firstIndex = k;
                            }
                        }                        
                        sameIndex.Add(firstIndex);
                        LeftFile.Inlines.Add(new Run(word) { Foreground = Brushes.Black });
                    }
                    else
                    {
                        LeftFile.Inlines.Add(new Run(word) { Foreground = Brushes.Red });
                    }
                    LeftFile.Inlines.Add(new Run(" "));
                }
                LeftFile.Inlines.Add(new Run("\n"));

                /* Display Right List */
                for (int j = 0; j < line2.Length; j++)
                {
                    if (sameIndex.Contains(j))
                    {
                        RightFile.Inlines.Add(new Run(line2[j]) { Foreground = Brushes.Black });
                    }
                    else
                    {
                        RightFile.Inlines.Add(new Run(line2[j]) { Foreground = Brushes.Red });
                    }
                    RightFile.Inlines.Add(new Run(" "));
                }
                RightFile.Inlines.Add(new Run("\n"));
            }

        }
    }
}
