using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class SettingsPanel : MonoBehaviour
{
    public RenderPipelineAsset[] qualityLevels;
    public TMP_Dropdown dropDown;


    // Start is called before the first frame update
    void Start()
    {
        dropDown.value = QualitySettings.GetQualityLevel();
    }

    public void ChangeLevel(int value)
    {
        QualitySettings.SetQualityLevel(value);
        QualitySettings.renderPipeline = qualityLevels[value];
    }
}
