/**
 * Manages related to preferences
 * 
 * @version 1.0.0
 * - Change class name Preferences_Script to OptionController
 * @author S3
 * @date 2024/03/08
*/

using UnityEngine;

public class OptionController : MonoBehaviour, Panel
{
    private void Update() { EscListener(); }

    public void Init() { }

    public void SetPanel(bool OnOff){ gameObject.SetActive(OnOff); }

    public bool GetPanelIsOn() { return gameObject.activeSelf; }

    public void EscListener()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            gameObject.SetActive(false);
    }
}

public interface OptionContent
{
    void Start();
    void Init();
}