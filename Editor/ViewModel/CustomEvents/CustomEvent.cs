using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEditor.ViewModel.CustomEvents
{
    public abstract class CustomEvent<T>
    {
        protected event EventHandler<T> ThisEvent;

        protected void Subscribe(EventHandler<T> handlerObject)
        {
            if (handlerObject != null)
            {
                ThisEvent += handlerObject;
            }
        }

        protected void UnSubscribe(EventHandler<T> handlerObject)
        {
            if (handlerObject != null)
            {
                ThisEvent -= handlerObject;
            }
        }

        public void OnEventRaised(object sender, T argument)
        {
            if (ThisEvent != null)
            {
                ThisEvent(sender, argument);
            }
        }
    }
}
