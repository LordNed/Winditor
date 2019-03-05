using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
    public partial class Foliage
    {
        [WProperty("Foliage", "Type", true)]
        public FoliageType Type
        {
            get { return (FoliageType)m_Type; }
            set
            {
                if ((int)value != m_Type)
                {
                    m_Type = (int)value;
                    OnPropertyChanged("Type");

                    UpdateModel();
                }
            }
        }

        public override void PostLoad()
        {
            base.PostLoad();

            GetPropertiesFromParameters();

            UpdateModel();
        }

        public override void PreSave()
        {
            base.PreSave();

            SetParametersWithProperties();
        }

        private void UpdateModel()
        {
            switch (Type)
            {
                case FoliageType.White_Flower:
                    m_objRender = WResourceManager.LoadObjResource("resources/models/flowers/flower.obj");
                    break;
                case FoliageType.Pink_Flower:
                    m_objRender = WResourceManager.LoadObjResource("resources/models/flowers/pflower_pink.obj");
                    break;
                case FoliageType.Grass:
                case FoliageType.Tree:
                    m_objRender = WResourceManager.LoadObjResource("resources/models/flowers/pflower_pink.obj");
                    break;
            }
        }
    }
}
