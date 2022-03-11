using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WindEditor.ViewModel;

namespace WindEditor.Editor.Managers
{
    public class InputProfileManager
    {
        private InputProfileContainer inputProfileContainer;

        public InputProfileContainer InputProfileContainer
        {
            get { return inputProfileContainer; }
            set { inputProfileContainer = value; }
        }

        public InputProfileManager() 
        {
            inputProfileContainer = new InputProfileContainer(KeyInputProfile.NormalInputProfile);
            LoadSettings();
        }

        public void LoadSettings()
        {
            string fileLocation = @"ProfileSettings.json";

            try
            {
                inputProfileContainer = JsonConvert.DeserializeObject<InputProfileContainer>(File.ReadAllText(fileLocation));
            }
            catch (Exception)
            {
                MessageBox.Show("Can't find the ProfileSettings.json file in root folder.");
            }
        }

        public void SaveSettings()
        {
            try
            {
                string serialized_settings = JsonConvert.SerializeObject(inputProfileContainer, Formatting.Indented);
                File.WriteAllText("ProfileSettings.json", serialized_settings);
            }
            catch (Exception)
            {
                MessageBox.Show("Can't save to the ProfileSettings.json file in root folder.");
            }
        }
    }

    #region DataStruct
    public struct InputProfileContainer
    {
        private KeyInputProfile inputProfileActive;
        private Key accelerationKey;

        public InputProfileContainer(KeyInputProfile key)
        {
            this.inputProfileActive = KeyInputProfile.NormalInputProfile;
            this.accelerationKey = Key.RightShift;
        }

        public KeyInputProfile KeyInputProfileActive
        {
            get { return inputProfileActive; }
            set
            {
                inputProfileActive = value;
            }
        }

        public Key AccelerationKey
        {
            get { return accelerationKey; }
            set
            {
                accelerationKey = value;
            }
        }
    }
    #endregion
}