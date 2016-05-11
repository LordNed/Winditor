using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace WindEditor
{
    public class SelectionAggregate : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        public BindingList<IPropertyValue> Values
        {
            get
            {
                // ... the first one is a field type..., the second is every field that is that.
                Dictionary<System.Type, List<IPropertyValue>> values = new Dictionary<System.Type, List<IPropertyValue>>();


                foreach(var mapActor in m_testList)
                {
                    foreach (var field in mapActor.Properties)
                    {
                        if (field == null)
                            continue;

                        if (!values.ContainsKey(field.GetType()))
                            values[field.GetType()] = new List<IPropertyValue>();

                        values[field.GetType()].Add(field);
                    }
                }

                // Now we have a list of all of the field types and their associated fields we can make Aggregates for them.
                BindingList<IPropertyValue> properties = new BindingList<IPropertyValue>();
                foreach(var kvp in values)
                {
                    if (kvp.Key ==  typeof(TStringPropertyValue))
                        properties.Add(new TStringValueAggregate(kvp.Value));
                    else
                        properties.Add(kvp.Value[0]);
                }

                return properties;
            }
        }

        private BindingList<WMapActor> m_selectionList;
        private ObservableCollection<WMapActor> m_testList;

        public SelectionAggregate(ObservableCollection<WMapActor> selectionList)
        {
            //m_selectionList = selectionList;
            //selectionList.ListChanged += SelectionList_ListChanged;
            m_testList = selectionList;
            m_testList.CollectionChanged += M_testList_CollectionChanged;
        }

        private void M_testList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            System.Console.WriteLine(e.Action);

            OnPropertyChanged("Values");
        }

        private void SelectionList_ListChanged(object sender, ListChangedEventArgs e)
        {
            if(e.ListChangedType == ListChangedType.ItemAdded || e.ListChangedType == ListChangedType.ItemDeleted || e.ListChangedType == ListChangedType.Reset)
            {
                OnPropertyChanged("Values");
            }
            if(e.ListChangedType == ListChangedType.PropertyDescriptorChanged)
            {
                System.Console.WriteLine("Test");
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
