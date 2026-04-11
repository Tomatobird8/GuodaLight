using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.HighDefinition;

namespace GuodaLight.Utils
{
    internal class FlashKeyPressListener : MonoBehaviour
    {
        internal List<AudioClip>? flashlightClips;
        Light? light;
        AudioSource? audio;
        HDAdditionalLightData? lightdata;

        public void Awake()
        {
            if (GuodaLight.InputActionInstance.LightButton == null)
            {
                return;
            }
            GuodaLight.InputActionInstance.LightButton.performed += OnActionPerformed;
            light = GetComponent<Light>();
            if (light != null)
            {
                Texture cookieTexture = StartOfRound.Instance.allPlayerScripts[0].allHelmetLights[0].cookie;
                light.cookie = cookieTexture;
            }
            StartCoroutine(WaitForComponents());
        }

        private IEnumerator WaitForComponents()
        {
            yield return new WaitForEndOfFrame();
            int iteration = 0;
            float interval = 1f;
            float current = 0.2f;
            bool allFound = false;
            while (iteration < 10 && !allFound)
            {
                while (current > 0f)
                {
                    current -= Time.deltaTime;
                    yield return new WaitForEndOfFrame();
                }
                current = interval;
                if (light == null)
                {
                    light = GetComponent<Light>();
                    if (light != null)
                    {
                        GuodaLight.Logger.LogDebug($"Got component {nameof(light)}");
                        Texture cookieTexture = StartOfRound.Instance.allPlayerScripts[0].allHelmetLights[0].cookie;
                        light.cookie = cookieTexture;
                    }
                }
                if (audio == null)
                {
                    audio = GetComponent<AudioSource>();
                    if (audio != null)
                    {
                        GuodaLight.Logger.LogDebug($"Got component {nameof(audio)}");
                    }
                }
                if (lightdata == null)
                {
                    lightdata = GetComponent<HDAdditionalLightData>();
                    if (lightdata != null)
                    {
                        GuodaLight.Logger.LogDebug($"Got component {nameof(lightdata)}");
                        lightdata.fadeDistance = 1000f;
                        lightdata.shadowFadeDistance = 1000f;
                        lightdata.shadowNearPlane = 0.66f;
                    }
                }
                allFound = light != null && audio != null && lightdata != null;
                iteration++;
            }
            yield return new WaitForEndOfFrame();
            GuodaLight.Logger.LogDebug($"All components were found: {allFound}. Iterations: {iteration}");
        }

        public void OnActionPerformed(InputAction.CallbackContext context)
        {
            if (light == null || !Application.isFocused || GameNetworkManager.Instance?.localPlayerController == null || GameNetworkManager.Instance.localPlayerController.isPlayerDead || GameNetworkManager.Instance.localPlayerController.isTypingChat || GameNetworkManager.Instance.localPlayerController.inTerminalMenu || GameNetworkManager.Instance.localPlayerController.quickMenuManager.isMenuOpen)
            {
                return;
            }
            light.enabled = !light.enabled;
            if (audio == null || flashlightClips == null)
            {
                return;
            }
            audio.PlayOneShot(light.enabled ? flashlightClips[0] : flashlightClips[1]);
        }
    }
}
