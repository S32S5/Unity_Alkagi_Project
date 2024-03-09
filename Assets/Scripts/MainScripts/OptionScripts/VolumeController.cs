/**
 * Control volume sliders
 * 
 * @version 1.0.0
 * - Change class name VolumeController_Script to VolumeController
 * - Code optimization
 * @author S3
 * @date 2024/03/06
*/

using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour, OptionContent
{
    Slider bgm, se;

    OptionDataController data;
    AudioSource sound;

    private void Awake()
    {
        bgm = GameObject.Find("BgmSlider").GetComponent<Slider>();
        se = GameObject.Find("SeSlider").GetComponent<Slider>();

        data = GameObject.Find("SceneDirector").GetComponent<OptionDataController>();
        sound = GameObject.Find("SceneDirector").GetComponent<AudioSource>();
    }

    // Init when game start
    public void Start() { Init(); }

    public void Init()
    {
        bgm.value = data.GetBgmVolume();
        se.value = data.GetSeVolume();
    }

    public void BgmOnValueChanged()
    {
        sound.volume = bgm.value;
        data.SetBgmVolume(bgm.value);
    }

    public void SeOnValueChanged() { data.SetSeVolume(se.value); }
}