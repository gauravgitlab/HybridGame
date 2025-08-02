using UnityEngine;

public class SkyboxGradientManager : Singleton<SkyboxGradientManager>
{
    [Header("Skybox Material (with SkyboxGradient shader)")]
    public Material m_skyboxMaterial;

    private void Awake()
    {
        GameplayEvents.LevelReset += GameplayEventsOnLevelReset;
    }

    private void Start()
    {
        if (m_skyboxMaterial == null)
        {
            return;
        }

        RenderSettings.skybox = m_skyboxMaterial;
    }
    
    private void OnDestroy()
    {
        GameplayEvents.LevelReset -= GameplayEventsOnLevelReset;
    }
    
    private Color RandomColor()
    {
        return new Color(Random.Range(0.2f, 1f), Random.Range(0.2f, 1f), Random.Range(0.2f, 1f));
    }

    private void RandomizeSkyboxColors()
    {
        Color topColor = RandomColor();
        Color horizonColor = RandomColor();
        Color bottomColor = RandomColor();
        Color sunColor = RandomColor();

        m_skyboxMaterial.SetColor("_SkyColor1", topColor);
        m_skyboxMaterial.SetColor("_SkyColor2", horizonColor);
        m_skyboxMaterial.SetColor("_SkyColor3", bottomColor);
        m_skyboxMaterial.SetColor("_SunColor", sunColor);
    }
    
    private void GameplayEventsOnLevelReset()
    {
        if (m_skyboxMaterial == null)
        {
            return;
        }

        RandomizeSkyboxColors();
        DynamicGI.UpdateEnvironment();
    }
}