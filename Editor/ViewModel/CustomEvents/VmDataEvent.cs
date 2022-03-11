using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEditor.ViewModel.CustomEvents
{
    //Singleton
    public class VmDataEvent
    {
        private static VmDataEvent VmInstance;
        private static event EventHandler<WindEditorEventArgs> ThisEvent;

        public static VmDataEvent _VmInstance
        {
            get
            {   if(VmInstance == null) 
                {
                    VmInstance = new VmDataEvent();
                }
                return VmInstance;
            }
        }

        private VmDataEvent()
        {
        }

        public EventHandler<WindEditorEventArgs> _ThisEvent
        {
            get
            {
                return ThisEvent;
            }
            set 
            {
                ThisEvent = value;
            }
        }

        public void Subscribe(EventHandler<WindEditorEventArgs> handlerObject)
        {
            if (handlerObject != null)
            {
                ThisEvent += handlerObject;
            }
        }

        public void UnSubscribe(EventHandler<WindEditorEventArgs> handlerObject)
        {
            if (handlerObject != null)
            {
                ThisEvent -= handlerObject;
            }
        }

        public void Raise(object sender, WindEditorEventArgs eventArgs)
        {
            if(ThisEvent != null)
                ThisEvent(sender, eventArgs);
        }
    }

    public class WindEditorEventArgs : EventArgs
    {
        private object objectToTransport;

        public object ObjectToTransport 
        {
            get { return objectToTransport;  }
        }

        public WindEditorEventArgs(object objectToTransport)
        {
            this.objectToTransport = objectToTransport;
        }
    }
}