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
            GameObject ogLightObj = new()
            {
                name = "GuodaLight"
            };
            Light ogLight = ogLightObj.AddComponent<Light>();
            ogLight.enabled = GuodaLight.lightOnByDefault;
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
            Utils.FlashKeyPressListener flashKeyPressListener = ogLightObj.AddComponent<Utils.FlashKeyPressListener>();
            if (GuodaLight.silentLight)
            {
                return;
            }
            AudioSource audioSource = ogLightObj.AddComponent<AudioSource>();
            audioSource.dopplerLevel = 0.3f;
            audioSource.maxDistance = 18f;
            audioSource.minDistance = 4f;
            audioSource.playOnAwake = false;
            audioSource.loop = false;
            audioSource.rolloffMode = AudioRolloffMode.Linear;
            audioSource.volume = GuodaLight.clickVolume;
            audioSource.priority = 128;
            audioSource.spread = 71f;
            audioSource.spatialBlend = 1f;
            audioSource.outputAudioMixerGroup = SoundManager.Instance.ambienceAudio.outputAudioMixerGroup;
            foreach (Item i in HUDManager.Instance.terminalScript.buyableItemsList)
            {
                if (i.name == "ProFlashlight")
                {
                    flashKeyPressListener.flashlightClips = [.. i.spawnPrefab.GetComponent<FlashlightItem>().flashlightClips];
                    break;
                }
            }
        }
    }
}
