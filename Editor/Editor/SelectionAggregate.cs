using System.Collections.Generic;
using System.ComponentModel;

namespace WindEditor
{
    public class SelectionAggregate : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        public BindingList<IPropertyValueAggregate> Values
        {
            get
            {
                List<List<IPropertyValue>> values = new List<List<IPropertyValue>>();

                if(m_selectionList.Count > 0)
                {
                    // Copy all of the values from the first selected actor and then compare them against the rest.
                    foreach (var field in m_selectionList[0].Properties)
                    {
                        values.Add(new List<IPropertyValue>());
                    }

                    foreach (var mapActor in m_selectionList)
                    {
                        for (int i = 0; i < mapActor.Properties.Count; i++)
                        {
                            values[i].Add(mapActor.Properties[i]);
                        }
                    }
                }

                // Now we have a list of all of the field types and their associated fields we can make Aggregates for them.
                BindingList<IPropertyValueAggregate> properties = new BindingList<IPropertyValueAggregate>();
                foreach(var list in values)
                {
                    properties.Add(list[0].GetValueAggregateInstance(list[0].Name, list));
                }
                return properties;
            }
        }

        private BindingList<WActorNode> m_selectionList;

        public SelectionAggregate(BindingList<WActorNode> selectionList)
        {
            m_selectionList = selectionList;
            selectionList.ListChanged += SelectionList_ListChanged;
        }

        private void SelectionList_ListChanged(object sender, ListChangedEventArgs e)
        {
            if(e.ListChangedType == ListChangedType.ItemAdded || e.ListChangedType == ListChangedType.ItemDeleted || e.ListChangedType == ListChangedType.Reset)
            {
                OnPropertyChanged("Values");
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
