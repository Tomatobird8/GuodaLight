using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using LethalCompanyInputUtils.Api;
using LethalCompanyInputUtils.BindingPathEnums;
using UnityEngine.InputSystem;

namespace GuodaLight
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    [BepInDependency("com.rune580.LethalCompanyInputUtils")]
    public class GuodaLight : BaseUnityPlugin
    {
        public static GuodaLight Instance { get; private set; } = null!;
        internal new static ManualLogSource Logger { get; private set; } = null!;
        internal static Harmony? Harmony { get; set; }

        internal static LightKey InputActionInstance = new();

        public static float lightIntensity = 486.8536f;
        public static float lightRange = 55f;
        public static float lightColorTemperature = 8371f;
        public static bool lightOnByDefault = true;
        public static bool silentLight = false;
        public static float clickVolume = 0.5f;

        private void Awake()
        {
            Logger = base.Logger;
            Instance = this;

            lightIntensity = Config.Bind<float>("General", "LightIntensity", 486.8536f, "Intensity of the light").Value;
            lightRange = Config.Bind<float>("General", "LightRange", 55f, "Light reach distance").Value;
            lightColorTemperature = Config.Bind<float>("General", "lightColorTemperature", 8371f, "Color temperature of the light").Value;
            lightOnByDefault = Config.Bind<bool>("General", "lightOnByDefault", true, "Should the light default to being on when player joins?").Value;
            silentLight = Config.Bind<bool>("General", "SilentLight", false, "Should toggling the light be silent?").Value;
            clickVolume = Config.Bind<float>("General", "ClickVolume", 0.5f, "Audio volume of the flashlight toggle click.").Value;

            Patch();

            Logger.LogInfo($"{MyPluginInfo.PLUGIN_GUID} v{MyPluginInfo.PLUGIN_VERSION} has loaded!");
        }

        internal static void Patch()
        {
            Harmony ??= new Harmony(MyPluginInfo.PLUGIN_GUID);

            Logger.LogDebug("Patching...");

            Harmony.PatchAll();

            Logger.LogDebug("Finished patching!");
        }

        internal static void Unpatch()
        {
            Logger.LogDebug("Unpatching...");

            Harmony?.UnpatchSelf();

            Logger.LogDebug("Finished unpatching!");
        }
    }


    public class LightKey : LcInputActions
    {
        [InputAction(KeyboardControl.F, Name = "Toggle Guodalight")]
        public InputAction? LightButton { get; set; }
    }
}
