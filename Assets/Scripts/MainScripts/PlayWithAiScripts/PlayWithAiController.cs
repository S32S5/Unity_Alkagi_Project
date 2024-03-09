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

    private void Awake()
    {
        main = GameObject.Find("MainPanel").GetComponent<MainPanelController>();
        newGame = GameObject.Find("NewGameSettingPanel").GetComponent<NewGameSettingController>();
    }

    private void Update(){ EscListener(); }

    public void Init() { }

    public void SetPanel(bool OnOff) { gameObject.SetActive(OnOff); }

    public bool GetPanelIsOn() { return gameObject.activeSelf; }

    public void EscListener() { main.SetPanel(true); }

    public void ExpertAiBtnOnClick()
    {
        SetPanel(false);
        newGame.SetPanel(true);
    }

    public void IntermediateAiBtnOnClick()
    {
        SetPanel(false);
        newGame.SetPanel(true);
    }

    public void BeginnerAiBtnOnClick()
    {
        SetPanel(false);
        newGame.SetPanel(true);
    }

    public void NewbieAiBtnOnClick()
    {
        SetPanel(false);
        newGame.SetPanel(true);
    }
}