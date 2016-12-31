using NeuronNetwork2Layers.Model;
using NeuronNetwork2Layers.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace NeuronNetwork2Layers
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ArrowPicturesManager North;
        private ArrowPicturesManager NorthEast;
        private ArrowPicturesManager East;
        List<ArrowPicturesManager> managers = new List<ArrowPicturesManager>();
        private Network NWork = new Network();

        private PaintingGridManager PaintingGridManager;
        public MainWindow()
        {
            InitializeComponent();

            PaintingGridManager = new PaintingGridManager(PaintingGrid);

            North = new ArrowPicturesManager("Север", MainGrid, 21, 250);
            NorthEast = new ArrowPicturesManager("Северо-восток", MainGrid, 190, 250);
            East = new ArrowPicturesManager("Восток", MainGrid, 360, 250);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            ArrowPicturesManager target = North;
            if (sender == button1)
            {
                target = NorthEast;
            }
            if (sender == button2)
            {
                target = East;
            }
            target.AddPicture(PaintingGridManager.GetData());
            Clear.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            managers.Add(North);
            managers.Add(NorthEast);
            managers.Add(East);

            for (int i = 1; i <= 100; i++)
                foreach (ArrowPicturesManager mngr in managers)
                    foreach (List<bool> picture in mngr.Pictures)
                        NWork.Train(picture, mngr.Name);

            Wire1.Visibility = Visibility.Visible;
            Wire2.Visibility = Visibility.Visible;
            Wire3.Visibility = Visibility.Visible;
            Recognize.Visibility = Visibility.Visible;
            ResultsGrid.Visibility = Visibility.Visible;
            button3.Visibility = Visibility.Collapsed;
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            managers.Add(North);
            managers.Add(NorthEast);
            managers.Add(East);

            for (int i = 1; i <= 100; i++)
                foreach (ArrowPicturesManager mngr in managers)
                    foreach (List<bool> picture in mngr.Pictures)
                        NWork.Train(picture, mngr.Name);

            PaintingGridManager.Clear();
            textBox.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }

        private void Recognize_Click(object sender, RoutedEventArgs e)
        {
            NWork.Evaluate(PaintingGridManager.GetData());

            List<double> axons = new List<double>();
            axons.Add(NWork.LastLayer.Neurons.ToList()[0].AxonValue);
            axons.Add(NWork.LastLayer.Neurons.ToList()[1].AxonValue);
            axons.Add(NWork.LastLayer.Neurons.ToList()[2].AxonValue);

            textBox.Text = axons[0].ToString();
            textBox1.Text = axons[1].ToString();
            textBox2.Text = axons[2].ToString();

            int index = axons.IndexOf(axons.Max());
            textBox3.Text = managers[index].Name;
        }
    }
}

