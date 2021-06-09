using MelonLoader;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ashnel.CozyGrove.CritterRepellant
{
    public class MyMod : MelonMod
    {
        private const string PreferenceCategoryId = "Ashnel.CozyGrove.CritterRepellant";
        private const string PreferenceHotkeyId = "Hotkey";

        private KeyCode _HotKey = KeyCode.B;

        public override void OnApplicationStart()
        {
            base.OnApplicationStart();

            TomlKeyCodeSupport.Install();
            MelonPreferences.CreateCategory(PreferenceCategoryId, $"Toggle HUD Settings");
            MelonPreferences.CreateEntry(PreferenceCategoryId, PreferenceHotkeyId, _HotKey, "Hotkey");
            this.ApplyPreferences();
        }

        private void ApplyPreferences()
        {
            _HotKey = MelonPreferences.GetEntryValue<KeyCode>(PreferenceCategoryId, PreferenceHotkeyId);
        }

        public override void OnPreferencesSaved() => this.ApplyPreferences();

        public override void OnUpdate()
        {
            if (this._HotKey != KeyCode.None &&
                SceneManager.GetActiveScene().name == "Game" &&
                Input.GetKeyDown(this._HotKey) == true)
            {
                // toggle inventory visibility.
                var instance = GameUI.Instance;
                if (instance != null)
                {
                    Cheats.CheatCrittersDestroyAll();
                }
            }
        }
    }
}