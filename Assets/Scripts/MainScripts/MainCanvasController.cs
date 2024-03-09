/**
 * Control main canvas
 * 
 * @version 1.0.0, new script
 * @author S3
 * @date 2024/03/08
*/

using UnityEngine;

public class MainCanvasController : MonoBehaviour, Canvas
{
    ExitController exit;
    OptionController option;
    NewGameSettingController newGame;
    PlayWithAnotherPcController anotherPc;

    private void Awake()
    {
        exit = GameObject.Find("ExitBackgroundPanel").GetComponent<ExitController>();
        option = GameObject.Find("OptionBackgroundPanel").GetComponent<OptionController>();
        newGame = GameObject.Find("NewGameSettingPanel").GetComponent<NewGameSettingController>();
        anotherPc = GameObject.Find("PlayWithAnotherPcPanel").GetComponent<PlayWithAnotherPcController>();
    }

    public void Init()
    {
        exit.SetPanel(false);
        option.SetPanel(false);
        newGame.SetPanel(false);
        anotherPc.SetPanel(false);
    }

    public void SetCanvas(bool OnOff) { gameObject.SetActive(OnOff); }
}