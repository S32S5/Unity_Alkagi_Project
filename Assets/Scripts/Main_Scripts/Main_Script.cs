/**
 * Manages progress of the main
 * 
 * @version 0.0.4
 * - Code optimization
 * @author S3
 * @date 2024/02/20
*/

using UnityEngine;

public class Main_Script : MonoBehaviour
{
    private QuitGameControl_Script quit;
    private Preferences_Script preferences;
    private NewGameSetting_Script newGameSetting;
    private PlayWithAnotherPc_Script anotherPc;

    // Specifies
    private void Awake()
    {
        quit = GameObject.Find("QuitGameBackground_Panel").GetComponent<QuitGameControl_Script>();
        preferences = GameObject.Find("PreferencesBackground_Panel").GetComponent<Preferences_Script>();
        newGameSetting = GameObject.Find("NewGameSetting_Panel").GetComponent<NewGameSetting_Script>();
        anotherPc = GameObject.Find("PlayWithAnotherPc_Panel").GetComponent<PlayWithAnotherPc_Script>();
    }

    // Esc
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(!preferences.GetPanelIsOn())
                quit.SetPanel();
        }
    }

    // gameObject set active
    public void SetPanel() { gameObject.SetActive(true); }

    // Preferences button onclick listener
    public void PreferencesButtonOnClick() { preferences.SetPanel(); }

    // ThisPcButton onlick listener
    public void ThisPcButtonOnClick()
    {
        gameObject.SetActive(false);

        newGameSetting.SetGameMode(0);
        newGameSetting.SetPanel(true);
    }

    // AnotherPcButton onlick listener
    public void AnotherPcButtonOnClick()
    {
        gameObject.SetActive(false);

        anotherPc.SetPanel(true);
    }
}