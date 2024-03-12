/**
 * Control play with ai panel
 * 
 * @version 1.1.0, new class
 * @author S3
 * @date 2024/03/09
*/

using UnityEngine;

public class PlayWithAiController : MonoBehaviour, Panel
{
    MainPanelController main;
    NewGameSettingController newGame;
    InGameCanvasController inGame;

    private void Awake()
    {
        main = GameObject.Find("MainPanel").GetComponent<MainPanelController>();
        newGame = GameObject.Find("NewGameSettingPanel").GetComponent<NewGameSettingController>();
        inGame = GameObject.Find("InGameCanvas").GetComponent <InGameCanvasController>();
    }

    private void Update(){ EscListener(); }

    public void Init() 
    {
        main.SetPanel(false);
        newGame.SetPanel(false);
    }

    public void SetPanel(bool OnOff) 
    { 
        gameObject.SetActive(OnOff); 

        if(OnOff)
            Init();
    }

    public bool GetPanelIsOn() { return gameObject.activeSelf; }

    public void EscListener() 
    {
        if(Input.GetKeyDown(KeyCode.Escape))
            main.SetPanel(true); 
    }

    public void ExpertAiBtnOnClick() { LevelBtnOnClick(3); }

    public void IntermediateAiBtnOnClick() { LevelBtnOnClick(2); }

    public void BeginnerAiBtnOnClick() { LevelBtnOnClick(1); }

    public void NewbieAiBtnOnClick() { LevelBtnOnClick(0); }

    // Level btn's listener method
    //
    // @param int
    private void LevelBtnOnClick(int level)
    {
        SetPanel(false);
        newGame.SetPanel(true);
        inGame.SetAiLevel(level);
    }
}