/**
 * Control exit panel's listeners
 * 
 * @version 1.0.0
 * - Change class name QuitGameControl_Script to ExitController
 *  - Code optimization
 * @author S3
 * @date 2024/03/08
*/

using UnityEngine;

public class ExitController : MonoBehaviour, Panel
{
    private void Update() { EscListener(); }

    public void Init() { }

    public void SetPanel(bool OnOff) { gameObject.SetActive(OnOff); }

    public bool GetPanelIsOn() { return gameObject.activeSelf; }

    public void EscListener()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            if (gameObject.activeSelf)
                NoButtonOnClick();
    }

    // Yes button onclick listener
    public void YesButtonOnClick() { Application.Quit(); }

    // No button onclick listener
    public void NoButtonOnClick() { gameObject.SetActive(false); }
}