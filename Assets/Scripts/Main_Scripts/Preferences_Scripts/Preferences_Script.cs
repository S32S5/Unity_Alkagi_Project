/**
 * Manages related to preferences
 * 
 * @version 0.0.4
 * - Code optimization
 * - Delete close panel button
 * @author S3
 * @date 2024/02/23
*/

using UnityEngine;

public class Preferences_Script : MonoBehaviour
{
    // GameObjet set active false when game start
    private void Start() { gameObject.SetActive(false); }

    // Esc
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
        }
    }

    // Return PreferencesBackground_Panel is active
    //
    // @return bool
    public bool GetPanelIsOn() { return gameObject.activeSelf; }

    // gameObject set active
    public void SetPanel() { gameObject.SetActive(true); }

    // Preferences button onclick listener
    public void PreferencesButtonOnClick() { gameObject.SetActive(true); }
}