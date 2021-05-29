using System;
using MelonLoader;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ashnel.CozyGrove.PreventHUDFade
{
    public class MyMod : MelonMod
    {
        private const string PreferenceCategoryId = "Ashnel.CozyGrove.PreventHUDFade";
        private const string PreferenceHotkeyId = "Hotkey";

        private KeyCode _HotKey = KeyCode.F10;

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
            if (SceneManager.GetActiveScene().name == "Game" || (this._HotKey != KeyCode.None && Input.GetKeyDown(this._HotKey) == true))
            {
                // prevent HUD fade
                var instance = GameUI.Instance;
                if (instance != null)
                {
                    instance.SetHUDElementVisibility(true, GameUI.HideableHUDElements.QuestLog, 0);
                    instance.SetHUDElementVisibility(true, GameUI.HideableHUDElements.DateAndTime, 0);
                    instance.SetHUDElementVisibility(true, GameUI.HideableHUDElements.BottomLeftGroup, 0);
                }
            }
        }
    }
}