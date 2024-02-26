/**
 * Manages related to quit game
 * 
 * @version 0.0.4
 *  - Code optimization
 * @author S3
 * @date 2024/02/20
*/

using UnityEngine;

public class QuitGameControl_Script : MonoBehaviour
{
    private GameObject panel;

    // Specifies
    private void Awake()
    {
        panel = GameObject.Find("QuitGameBackground_Panel");
    }

    // Panel set false when game start
    private void Start() { panel.SetActive(false); }

    // Esc
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (panel.activeSelf)
                panel.SetActive(false);
        }
    }

    // Return QuitGame_Panel is active
    //
    // @return bool
    public bool GetPanelIsOn() { return panel.activeSelf; }

    // Set QuitGame_Panel
    //
    // @param bool
    public void SetPanel() { panel.SetActive(true); }

    // Yes button onclick listener
    public void YesButtonOnClick() { Application.Quit(); }

    // No button onclick listener
    public void NoButtonOnClick() { panel.SetActive(false); }
}