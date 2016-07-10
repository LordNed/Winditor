using JStudio.OpenGL;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using WindEditor;

namespace JStudio.J3D
{
    public class TevColorOverride : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<WLinearColor> Colors { get { return m_colors; } }
        public ObservableCollection<WLinearColor> ConstColors { get { return m_kColors; } }
        public ObservableCollection<bool> ColorsEnabled { get { return m_colorsEnabled; } }
        public ObservableCollection<bool> ConstColorsEnabled { get { return m_kColorsEnabled; } }

        private ObservableCollection<WLinearColor> m_colors;
        private ObservableCollection<WLinearColor> m_kColors;
        private ObservableCollection<bool> m_colorsEnabled;
        private ObservableCollection<bool> m_kColorsEnabled;

        public TevColorOverride()
        {
            m_colors = new ObservableCollection<WLinearColor>(new[] { WLinearColor.White, WLinearColor.White, WLinearColor.White, WLinearColor.White });
            m_kColors = new ObservableCollection<WLinearColor>(new[] { WLinearColor.White, WLinearColor.White, WLinearColor.White, WLinearColor.White });
            m_colorsEnabled = new ObservableCollection<bool>(new[] { false, false, false, false });
            m_kColorsEnabled = new ObservableCollection<bool>(new[] { false, false, false, false });
        }

        public void SetTevColorOverride(int index, WLinearColor overrideColor)
        {
            if (index < 0 || index >= 4)
                throw new ArgumentOutOfRangeException("index", "index must be between 0 and 3");

            m_colors[index] = overrideColor;
            m_colorsEnabled[index] = true;
        }

        public void SetTevkColorOverride(int index, WLinearColor overrideColor)
        {
            if (index < 0 || index >= 4)
                throw new ArgumentOutOfRangeException("index", "index must be between 0 and 3");

            m_kColors[index] = overrideColor;
            m_kColorsEnabled[index] = true;
        }

        public void SetPSBlockForMaterial(Material srcMaterial, ref PSBlock psBlock)
        {
            psBlock.Color0 = m_colorsEnabled[0] ? m_colors[0] : srcMaterial.TevColorIndexes[0];
            psBlock.Color1 = m_colorsEnabled[1] ? m_colors[1] : srcMaterial.TevColorIndexes[1];
            psBlock.Color2 = m_colorsEnabled[2] ? m_colors[2] : srcMaterial.TevColorIndexes[2];
            psBlock.Color3 = m_colorsEnabled[3] ? m_colors[3] : srcMaterial.TevColorIndexes[3];

            psBlock.kColor0 = m_kColorsEnabled[0] ? m_kColors[0] : srcMaterial.TevKonstColorIndexes[0];
            psBlock.kColor1 = m_kColorsEnabled[1] ? m_kColors[1] : srcMaterial.TevKonstColorIndexes[1];
            psBlock.kColor2 = m_kColorsEnabled[2] ? m_kColors[2] : srcMaterial.TevKonstColorIndexes[2];
            psBlock.kColor3 = m_kColorsEnabled[3] ? m_kColors[3] : srcMaterial.TevKonstColorIndexes[3];
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
