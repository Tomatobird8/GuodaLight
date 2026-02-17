using GameNetcodeStuff;
using HarmonyLib;
using UnityEngine;

namespace GuodaLight.Patches
{
    [HarmonyPatch(typeof(PlayerControllerB))]
    public class AddLightObject
    {
        [HarmonyPatch("ConnectClientToPlayerObject")]
        [HarmonyPostfix]
        public static void AddLightObjectPatch(PlayerControllerB __instance)
        {
            GameObject ogLightObj = new GameObject();
            ogLightObj.name = "GuodaLight";
            Light ogLight = ogLightObj.AddComponent<Light>();
            ogLight.enabled = true;
            ogLight.boundingSphereOverride = new Vector4(0f, 0f, 0f, 0f);
            ogLight.bounceIntensity = 0f;
            ogLight.colorTemperature = GuodaLight.lightColorTemperature;
            ogLight.cookieSize = 10f;
            ogLight.cullingMask = -1;
            ogLight.innerSpotAngle = 21.8021f;
            ogLight.intensity = GuodaLight.lightIntensity;
            ogLight.lightShadowCasterMode = LightShadowCasterMode.Everything;
            ogLight.range = GuodaLight.lightRange;
            ogLight.renderingLayerMask = 1;
            ogLight.shadowBias = 0.05f;
            ogLight.shadowNearPlane = 0.2f;
            ogLight.shadowNormalBias = 0.4f;
            ogLight.shadowResolution = UnityEngine.Rendering.LightShadowResolution.FromQualitySettings;
            ogLight.shadows = LightShadows.Hard;
            ogLight.shadowStrength = 1f;
            ogLight.shape = LightShape.Cone;
            ogLight.spotAngle = 73f;
            ogLight.type = LightType.Spot;
            ogLight.useBoundingSphereOverride = false;
            ogLight.useColorTemperature = true;
            ogLight.useShadowMatrixOverride = false;
            ogLight.useViewFrustumForShadowCasterCull = true;
            ogLightObj.transform.SetParent(__instance.gameplayCamera.transform);
            ogLightObj.transform.localScale = new Vector3(0.3627f, 0.4234f, 0.4918f);
            ogLightObj.transform.SetPositionAndRotation(__instance.allHelmetLights[0].transform.position, __instance.allHelmetLights[0].transform.rotation);
            ogLightObj.AddComponent<Utils.FlashKeyPressListener>();
        }
    }
}
