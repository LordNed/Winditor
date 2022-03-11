using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEditor.Editor.KeyBindings
{
    public class KeyProfilesLibrary
    {
        private KeyCameraProfile cameraProfiles;

        public KeyCameraProfile CameraProfiles 
        {
            get { return cameraProfiles; }
        }

        public KeyProfilesLibrary() 
        {
            this.cameraProfiles = new KeyCameraProfile();
        }
    }
}
