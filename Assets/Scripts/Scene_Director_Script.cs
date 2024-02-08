/**
 * Manages progress of the scene
 * 
 * @version 0.0.3
 *  - Code optimization
 *  - Delete new game canvas
 * @author S3
 * @date 2024/02/07
*/

using UnityEngine;

public class Scene_Director_Script : MonoBehaviour
{
    private GameObject Main_Canvas, InGame_Canvas;
    private GameObject NewGameSetting_Panel;

    // Specifies
    private void Awake()
    {
        Main_Canvas = GameObject.Find("Main_Canvas");
        InGame_Canvas = GameObject.Find("InGame_Canvas");

        NewGameSetting_Panel = GameObject.Find("NewGameSetting_Panel");
    }

    // Start Game
    private void Start()
    {
        GetComponent<AudioSource>().volume = GetComponent<PreferencesData_Script>().GetBgmVolume();
        MainOn();
    }

    // Set active Main_Canvas
    public void MainOn()
    {
        InGame_Canvas.SetActive(false);
        Main_Canvas.SetActive(true);
    }

    // Close NewGameSetting and proceed to InGame
    public void InGame()
    {
        int initialEggNumber = NewGameSetting_Panel.GetComponent<InitialEggNumberControl_Script>().GetInitialEggNumber();
        bool firstTurn = NewGameSetting_Panel.GetComponent<FirstTurnControl_Script>().GetFirstTurn();
        int turnTimeLimit = NewGameSetting_Panel.GetComponent<TurnTimeLimitControl_Script>().GetTurnTimeLimit();

        Main_Canvas.SetActive(false);
        InGame_Canvas.GetComponent<InGame_Script>().Init(initialEggNumber, firstTurn, turnTimeLimit);
        InGame_Canvas.SetActive(true);
    }
}