using UnityEngine;

public class SceneLightingSettings : MonoBehaviour
{
    [Header("Ambient Lighting Setup")]
    [SerializeField] private UnityEngine.Rendering.AmbientMode ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
    [SerializeField] private Color ambientColor = new Color(0.65f, 0.65f, 0.65f, 1f);
    [SerializeField] private Material sceneSkybox;

    private void Awake()
    {
        ApplyLightingSettings();
    }

    private void ApplyLightingSettings()
    {
        RenderSettings.ambientMode = ambientMode;
        RenderSettings.ambientLight = ambientColor;

        if (sceneSkybox != null)
        {
            RenderSettings.skybox = sceneSkybox;
        }

        DynamicGI.UpdateEnvironment();

        Debug.Log("<color=cyan>Scene Lighting Initialized Successfully for: </color>" + gameObject.scene.name, gameObject);
    }
}