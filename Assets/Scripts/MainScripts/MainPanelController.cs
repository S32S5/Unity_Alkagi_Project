/**
 * Control main panel's listeners
 * 
 * @version 1.1.0
 * - Add PlayWithAiButtonOnClick method
 * - Add Init method
 * @author S3
 * @date 2024/03/09
*/

using UnityEngine;

public class MainPanelController : MonoBehaviour, Panel
{
    ExitController exit;
    OptionController option;
    NewGameSettingController newGame;
    PlayWithAiController ai;
    PlayWithAnotherPcController anotherPc;

    InGameCanvasController inGame;

    private void Awake()
    {
        exit = GameObject.Find("ExitBackgroundPanel").GetComponent<ExitController>();
        option = GameObject.Find("OptionBackgroundPanel").GetComponent<OptionController>();
        newGame = GameObject.Find("NewGameSettingPanel").GetComponent<NewGameSettingController>();
        ai = GameObject.Find("PlayWithAiPanel").GetComponent<PlayWithAiController>();
        anotherPc = GameObject.Find("PlayWithAnotherPcPanel").GetComponent<PlayWithAnotherPcController>();

        inGame = GameObject.Find("InGameCanvas").GetComponent<InGameCanvasController>();
    }

    private void Update() { EscListener(); }

    public void Init() 
    {
        exit.SetPanel(false);
        option.SetPanel(false);
        newGame.SetPanel(false);
        ai.SetPanel(false);
        anotherPc.SetPanel(false);
    }

    public void SetPanel (bool OnOff) 
    { 
        gameObject.SetActive(OnOff);

        if (OnOff)
            Init();
    }

    public bool GetPanelIsOn() { return gameObject.activeSelf; }

    public void EscListener()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            if (!option.GetPanelIsOn())
                exit.SetPanel(true);
    }

    public void OptionButtonOnClick() { option.SetPanel(true); }

    public void PlayWithAiButtonOnClick()
    {
        gameObject.SetActive(false);
        inGame.SetGameMode(0);
        ai.SetPanel(true);
    }

    public void ThisPcButtonOnClick()
    {
        gameObject.SetActive(false);
        inGame.SetGameMode(1);
        newGame.SetPanel(true);
    }

    public void AnotherPcButtonOnClick()
    {
        gameObject.SetActive(false);
        inGame.SetGameMode(2);
        anotherPc.SetPanel(true);
    }
}