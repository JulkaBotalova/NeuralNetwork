using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NeuronNetwork2Layers.View
{
    public class Deletor : Button
    {
        //public Grid MainGrid { get; }
        //public Grid GridToRemove { get; }
        //private Button button;
        //private ShowPicturesDelegate showPictures;
        public IEnumerable<bool> picture { get; }

        public Deletor(List<bool> picture) : base()
        {
            this.picture = picture;
        }
    }
}
