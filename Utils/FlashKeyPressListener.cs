using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.SceneManagement;

namespace GuodaLight.Utils
{
    internal class FlashKeyPressListener : MonoBehaviour
    {
        Light? light;
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

            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            lightdata = GetComponent<HDAdditionalLightData>();
            if (lightdata != null)
            {
                lightdata.fadeDistance = 1000f;
                lightdata.shadowFadeDistance = 1000f;
                lightdata.shadowNearPlane = 0.66f;
                SceneManager.sceneLoaded -= OnSceneLoaded;
            }
        }

        public void OnActionPerformed(InputAction.CallbackContext context)
        {
            if (light == null)
            {
                return;
            }
            light.enabled = !light.enabled;
        }
    }
}
