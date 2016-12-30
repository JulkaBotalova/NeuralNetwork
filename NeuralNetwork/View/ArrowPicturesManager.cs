using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace NeuronNetwork2Layers.View
{
    public class ArrowPicturesManager
    {
        public string Name { get; }
        public IEnumerable<IEnumerable<bool>> Pictures { get { return pictures; } }
        private string fileName;
        private Grid mainGrid;
        private int marginLeft;
        private int marginTop;
        private string rootFolder = "TeacherPictures/";
        private Label header;
        //private bool drawLine;

        private List<List<bool>> pictures;
        private List<ShowingGridManager> teacherPictures = new List<ShowingGridManager>();
        private List<Deletor> deletors = new List<Deletor>();

        public ArrowPicturesManager(string name, Grid mainGrid, int marginLeft, int marginTop/*, bool drawLine = true*/)
        {
            Name = name;
            fileName = Name + ".txt";
            this.mainGrid = mainGrid;
            this.marginLeft = marginLeft;
            this.marginTop = marginTop + 35;
            //this.drawLine = drawLine;

            header = new Label();
            header.Content = Name;
            header.HorizontalAlignment = HorizontalAlignment.Left;
            header.VerticalAlignment = VerticalAlignment.Top;
            header.Margin = new Thickness(marginLeft, marginTop, 0, 0);
            mainGrid.Children.Add(header);

            RefreshPictures();
        }

        public void AddPicture(List<bool> picture)
        {
            pictures.Insert(0, picture);
            SaveToFile();
            RefreshPictures();
        }

        private void RefreshPictures()
        {
            StreamReader sr = new StreamReader(rootFolder + fileName);
            int i = 0;
            pictures = new List<List<bool>>();

            deleteExistingElements();

            while (sr.Peek() >= 0)
            {
                string[] symbolsOfPicture = sr.ReadLine().Split(new char[] { ',' });
                List<bool> pictureBoolData = new List<bool>();
                foreach (string s in symbolsOfPicture)
                {
                    if (s == "1")
                    {
                        pictureBoolData.Add(true);
                    }
                    else
                    {
                        pictureBoolData.Add(false);
                    }
                }
                pictures.Add(pictureBoolData);

                Grid grid = new Grid();
                grid.Width = 150;
                grid.Height = 150;
                grid.Margin = new Thickness(marginLeft, marginTop + i * 230, 0, 0);
                grid.HorizontalAlignment = HorizontalAlignment.Left;
                grid.VerticalAlignment = VerticalAlignment.Top;

                mainGrid.Children.Add(grid);
                ShowingGridManager sgm = new ShowingGridManager(grid);
                sgm.Show(pictureBoolData);
                teacherPictures.Add(sgm);

                /*if (drawLine)
                {
                    Line line = new Line();
                    line.Stroke = Brushes.LightSteelBlue;
                    line.X1 = marginLeft + 150 + 10;
                    line.Y1 = marginTop + i * 230 - 10;
                    line.X2 = marginLeft + 150 + 10;
                    line.Y2 = marginTop + i * 230 + 170;
                    line.HorizontalAlignment = HorizontalAlignment.Left;
                    line.VerticalAlignment = VerticalAlignment.Center;
                    line.StrokeThickness = 2;
                    mainGrid.Children.Add(line);
                }*/

                Deletor deletor = new Deletor(pictureBoolData);
                deletor.Margin = new Thickness(marginLeft + 37, marginTop + i * 230 + 155, 0, 0);
                deletor.HorizontalAlignment = HorizontalAlignment.Left;
                deletor.VerticalAlignment = VerticalAlignment.Top;
                deletor.Width = 75;
                deletor.Height = 25;
                deletor.Content = "Удалить";
                deletor.Click += RemoveButtonClick;
                mainGrid.Children.Add(deletor as Button);
                deletors.Add(deletor);

                i++;
            }

            sr.Close();
        }

        private void deleteExistingElements()
        {
            foreach (ShowingGridManager sgm in teacherPictures)
            {
                mainGrid.Children.Remove(sgm.ManagedGrid);
            }

            foreach (Deletor deletor in deletors)
            {
                mainGrid.Children.Remove(deletor as Button);
            }
        }

        private void SaveToFile()
        {
            StreamWriter sw = new StreamWriter(rootFolder + fileName, false);
            foreach (List<bool> picture in pictures)
            {
                string line = "";
                for (int i = 0; i < picture.Count; i++)
                {
                    string sign = "1";
                    if (!picture[i])
                        sign = "0";
                    line += sign;

                    if (i != picture.Count - 1)
                        line += ",";
                }
                sw.WriteLine(line);
            }
            sw.Close();
        }

        private void RemoveButtonClick(object sender, RoutedEventArgs e)
        {
            Deletor deletor = (sender as Deletor);
            pictures.Remove(deletor.picture as List<bool>);
            SaveToFile();
            //mainGrid.Children.Remove(deletor.GridToRemove);
            //mainGrid.Children.Remove((Button)sender);
            RefreshPictures();
        }
    }
}

