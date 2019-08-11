using System.Collections.Generic;
using System.ComponentModel;

namespace WindEditor
{
    public class WEditorSelectionAggregate : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string FourCC
        {
            get
            {
                string fourCC = string.Empty;
                if (m_selectionList.Count > 0)
                    fourCC = m_selectionList[0].FourCC;

                for(int i = 0; i < m_selectionList.Count; i++)
                {
                    if(m_selectionList[i].FourCC != fourCC)
                    {
                        fourCC = "--";
                        break;
                    }
                }

                return fourCC;
            }
        }

        public MapLayer? Layer
        {
            get
            {
                MapLayer layer = MapLayer.Default;
                if (m_selectionList.Count > 0)
                    layer = m_selectionList[0].Layer;

                for (int i = 0; i < m_selectionList.Count; i++)
                {
                    if (m_selectionList[i].Layer != layer)
                    {
                        return null;
                    }
                }

                return layer;
            }
            set
            {
                if (value == null)
                    return;

                for (int i = 0; i < m_selectionList.Count; i++)
                    m_selectionList[i].Layer = (MapLayer)value;
            }
        }
        public BindingList<IPropertyValueAggregate> Values
        {
            get
            {
                List<List<IPropertyValue>> values = GetUniquePropertyValuesFromSelection();

                // Now we have a list of all of the field types and their associated fields we can make Aggregates for them.
                BindingList<IPropertyValueAggregate> properties = new BindingList<IPropertyValueAggregate>();
                foreach (var list in values)
                {
                    properties.Add(list[0].GetValueAggregateInstance(list[0].Name, list));
                }
                return properties;
            }
        }


        private BindingList<WActorNode> m_selectionList;

        public WEditorSelectionAggregate(BindingList<WActorNode> selectionList)
        {
            m_selectionList = selectionList;
            selectionList.ListChanged += SelectionList_ListChanged;
        }

        public void Clear()
        {
            m_selectionList.Clear();
            OnPropertyChanged("Values");
            OnPropertyChanged("FourCC");
            OnPropertyChanged("Layer");
        }

        private void SelectionList_ListChanged(object sender, ListChangedEventArgs e)
        {
            if(e.ListChangedType == ListChangedType.ItemAdded || e.ListChangedType == ListChangedType.ItemDeleted || e.ListChangedType == ListChangedType.Reset)
            {
                OnPropertyChanged("Values");
                OnPropertyChanged("FourCC");
                OnPropertyChanged("Layer");
            }
        }

        private List<List<IPropertyValue>> GetUniquePropertyValuesFromSelection()
        {
            var values = new List<List<IPropertyValue>>();
            string selectionType = null;

            if (m_selectionList.Count > 0)
            {
                // Copy all of the values from the first selected actor and then compare them against the rest.
                foreach (var field in m_selectionList[0].Properties)
                {
                    values.Add(new List<IPropertyValue>());
                }

                foreach (var mapActor in m_selectionList)
                {
                    if (selectionType == null)
                    {
                        selectionType = mapActor.FourCC;
                    }
                    else
                    {
                        // We're trying to select two different objects, which isn't supported!
                        if (string.Compare(mapActor.FourCC, selectionType) != 0)
                        {
                            values.Clear();
                            break;
                        }
                    }

                    for (int i = 0; i < mapActor.Properties.Count; i++)
                    {
                        values[i].Add(mapActor.Properties[i]);
                    }
                }
            }

            return values;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
