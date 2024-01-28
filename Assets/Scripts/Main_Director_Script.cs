/**
 * Manages progress of the game
 * 
 * @version 0.2
 *  - Delete shortcut key script
 *  - Code optimization
 * @author S3
 * @date 2024/01/28
*/

using UnityEngine;

public class Main_Director_Script : MonoBehaviour
{
    private GameObject NewGame_Canvas, InGame_Canvas;

    // Specifies
    private void Awake()
    {
        NewGame_Canvas = GameObject.Find("NewGame_Canvas");
        InGame_Canvas = GameObject.Find("InGame_Canvas");
    }

    // Start Game
    private void Start()
    {
        NewGameSetting();
    }

    // Close InGame and proceed to NewGameSetting
    public void NewGameSetting()
    {
        InGame_Canvas.SetActive(false);
        NewGame_Canvas.SetActive(true);
    }

    // Close NewGameSetting and proceed to InGame
    public void InGame()
    {
        int initialEggNumber = NewGame_Canvas.GetComponent<InitialEggNumberControl_Script>().GetInitialEggNumber();
        bool firstTurn = NewGame_Canvas.GetComponent<FirstTurnControl_Script>().GetFirstTurn();
        int turnTimeLimit = NewGame_Canvas.GetComponent<TurnTimeLimitControl_Script>().GetTurnTimeLimit();

        NewGame_Canvas.SetActive(false);
        InGame_Canvas.SetActive(true);
        InGame_Canvas.GetComponent<InGame_Script>().Init(initialEggNumber, firstTurn, turnTimeLimit);
    }
}