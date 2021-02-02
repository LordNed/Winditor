using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpFont;
using System.ComponentModel;

namespace WindEditor.Minitors.Text
{
    public class TextboxRendererViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Interface
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private Face RockNRoll;

        public TextboxRendererViewModel()
        {
            RockNRoll = new Face(new Library(), "resources/font/RocknRollOne-Regular.ttf");
        }
    }
}
