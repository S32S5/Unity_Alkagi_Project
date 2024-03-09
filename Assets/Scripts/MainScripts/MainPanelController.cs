/**
 * Control main panel's listeners
 * 
 * @version 1.0.0
 * - Change class name Main_Script to MainPanelController
 * - Code optimization
 * @author S3
 * @date 2024/03/08
*/

using UnityEngine;

public class MainPanelController : MonoBehaviour, Panel
{
    ExitController exit;
    OptionController option;
    NewGameSettingController newGame;
    PlayWithAnotherPcController anotherPc;

    InitialSettingDataController data;

    private void Awake()
    {
        exit = GameObject.Find("ExitBackgroundPanel").GetComponent<ExitController>();
        option = GameObject.Find("OptionBackgroundPanel").GetComponent<OptionController>();
        newGame = GameObject.Find("NewGameSettingPanel").GetComponent<NewGameSettingController>();
        anotherPc = GameObject.Find("PlayWithAnotherPcPanel").GetComponent<PlayWithAnotherPcController>();

        data = GameObject.Find("NewGameSettingPanel").GetComponent<InitialSettingDataController>();
    }

    private void Update() { EscListener(); }

    public void Init() { }

    public void SetPanel (bool OnOff) { gameObject.SetActive(OnOff); }

    public bool GetPanelIsOn() { return gameObject.activeSelf; }

    public void EscListener()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            if (!option.GetPanelIsOn())
                exit.SetPanel(true);
    }

    public void OptionButtonOnClick() { option.SetPanel(true); }

    public void ThisPcButtonOnClick()
    {
        gameObject.SetActive(false);
        data.SetGameMode(0);
        newGame.SetPanel(true);
    }

    public void AnotherPcButtonOnClick()
    {
        gameObject.SetActive(false);
        data.SetGameMode(1);
        anotherPc.SetPanel(true);
    }
}