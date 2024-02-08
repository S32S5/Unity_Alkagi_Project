/**
 * Manage related to volume
 * 
 * @version 0.0.3
 * - New script
 * @author S3
 * @date 2024/02/08
*/

using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    private Slider Bgm_Slider, Se_Slider;

    // Specifies
    private void Awake()
    {
        Bgm_Slider = GameObject.Find("Bgm_Slider").GetComponent<Slider>();
        Se_Slider = GameObject.Find("Se_Slider").GetComponent<Slider>();
    }

    // Specifies when game start
    private void Start()
    {
        Bgm_Slider.value = GameObject.Find("Scene_Director").GetComponent<PreferencesData_Script>().GetBgmVolume();
        Se_Slider.value = GameObject.Find("Scene_Director").GetComponent<PreferencesData_Script>().GetSeVolume();

        Bgm_Slider.onValueChanged.AddListener(delegate {
            GameObject.Find("Scene_Director").GetComponent<AudioSource>().volume = Bgm_Slider.value;
            GameObject.Find("Scene_Director").GetComponent<PreferencesData_Script>().SetBgmVolume(Bgm_Slider.value);
        });
        Se_Slider.onValueChanged.AddListener(delegate {
            GameObject.Find("Scene_Director").GetComponent<PreferencesData_Script>().SetSeVolume(Se_Slider.value);
        });
    }
}