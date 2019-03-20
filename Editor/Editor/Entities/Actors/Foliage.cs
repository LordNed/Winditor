using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
    public partial class grass
    {
        [WProperty("Foliage", "Type", true)]
        public FoliageType Type
        {
            get { return (FoliageType)Unknown_2; }
            set
            {
                if ((int)value != Unknown_2)
                {
                    Unknown_2 = (int)value;
                    OnPropertyChanged("Type");

                    UpdateModel();
                }
            }
        }

        public override void PostLoad()
        {
            UpdateModel();
        }

        public override void PreSave()
        {

        }

        private void UpdateModel()
        {
            switch (Type)
            {
                case FoliageType.White_Flower:
                    m_objRender = WResourceManager.LoadObjResource("resources/models/flowers/flower.obj", new OpenTK.Vector4(1, 1, 1, 1));
                    break;
                case FoliageType.Pink_Flower:
                    m_objRender = WResourceManager.LoadObjResource("resources/models/flowers/pflower_pink.obj", new OpenTK.Vector4(1, 1, 1, 1));
                    break;
                case FoliageType.Grass:
                case FoliageType.Tree:
                    m_objRender = WResourceManager.LoadObjResource("resources/models/flowers/pflower_pink.obj", new OpenTK.Vector4(1, 1, 1, 1));
                    break;
            }
        }
    }
}
