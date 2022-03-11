using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEditor.ViewModel.CustomEvents
{
    public class CloseWindowEvent<T> : CustomEvent<T>
    {
        public void SubscribeCloseEvent(EventHandler<T> handlerObject)
        {
            Subscribe(handlerObject); 
        }

        public void UnSubscribeCloseEvent(EventHandler<T> handlerObject) 
        {
            UnSubscribe(handlerObject);
        }
    }
}
