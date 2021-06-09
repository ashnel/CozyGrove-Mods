using System;
using MelonLoader;
using MelonLoader.Tomlyn.Model;
using UnityEngine;

namespace Ashnel.CozyGrove.CritterRepellant
{
    internal static class TomlKeyCodeSupport
    {
        public static void Install()
        {
            try
            {
                MelonPreferences.Mapper.RegisterMapper(FromToml, ToToml);
            }
            catch
            {
            }
        }

        private static KeyCode FromToml(TomlObject value)
        {
            var s = (value as TomlString)?.Value;
            return string.IsNullOrEmpty(s) == false && Enum.TryParse<KeyCode>(s, out var keycode) != false
                ? keycode
                : KeyCode.None;
        }

        private static TomlObject ToToml(KeyCode value)
        {
            return MelonPreferences.Mapper.ToToml(value.ToString());
        }
    }
}