using System;
using WindEditor.Editor.Managers;

namespace WindEditor.ViewModel
{
    /// <summary>
    /// Data part 
    /// </summary>
    public partial class KeysMenuViewModel 
    {
        private void GetInputProfile()
        {
            if (windEditorInstance != null && inputProfileManager != null)
            {
                /*if (inputProfileManager.InputProfileContainer.KeyInputProfileActive != null) 
                {
                    KeyInputProfile input;
                    bool isConverted = Enum.TryParse(inputProfileManager.InputProfileContainer.KeyInputProfileActive, out input);
                    if (isConverted)
                    {
                        keyInputProfileActive = input;
                        OnPropertyChanged("InputProfile");
                    }
                }*/

                keyInputProfileActive = inputProfileManager.InputProfileContainer.KeyInputProfileActive;
                OnPropertyChanged("InputProfile");
                accelerationKey = inputProfileManager.InputProfileContainer.AccelerationKey;
                OnPropertyChanged("AccelerationKey");
            }
        }

        private void SetInputProfile()
        {
            if (windEditorInstance != null && inputProfileManager != null)
            {
                // InputProfileContainer is a struct
                InputProfileContainer inputProfile = inputProfileManager.InputProfileContainer;

                inputProfile.KeyInputProfileActive = keyInputProfileActive;
                inputProfile.AccelerationKey = accelerationKey;

                // Set struct back
                inputProfileManager.InputProfileContainer = inputProfile;
            }
        }
    }
}
