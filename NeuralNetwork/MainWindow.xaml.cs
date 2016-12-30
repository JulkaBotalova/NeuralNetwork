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
            Wire1.Visibility = Visibility.Visible;
            Wire2.Visibility = Visibility.Visible;
            Wire3.Visibility = Visibility.Visible;
            Recognize.Visibility = Visibility.Visible;
            ResultsGrid.Visibility = Visibility.Visible;
            button3.Visibility = Visibility.Collapsed;
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            PaintingGridManager.Clear();
            textBox.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }

        private void Recognize_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

