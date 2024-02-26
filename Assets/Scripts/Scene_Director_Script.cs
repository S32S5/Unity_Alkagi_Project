/**
 * Manages all aspects of the scene
 * 
 * @version 0.0.4
 *  - Update comment
 *  - Code optimization
 * @author S3
 * @date 2024/02/21
*/

using UnityEngine;

public class Scene_Director_Script : MonoBehaviour
{
    private GameObject MainCanvas, InGameCanvas, InGamePanel, netManager;

    // Specifies
    private void Awake()
    {
        MainCanvas = GameObject.Find("Main_Canvas");
        InGameCanvas = GameObject.Find("InGame_Canvas");
        InGamePanel = GameObject.Find("InGame_Panel");
        netManager = GameObject.Find("NetworkManager");
    }

    // Start Game
    private void Start()
    {
        GetComponent<AudioSource>().volume = GetComponent<PreferencesData_Script>().GetBgmVolume();
        MainOn();
        netManager.SetActive(false);
    }

    // Set active Main_Canvas
    public void MainOn()
    {
        InGameCanvas.SetActive(false);
        MainCanvas.SetActive(true);
    }

    // Close NewGameSetting and proceed to InGame
    public void InGame(int gameMode, int initialEggNumber, bool firstTurn, int turnTimeLimit)
    {
        MainCanvas.SetActive(false);
        InGamePanel.GetComponent<InGame_Script>().Init(gameMode, initialEggNumber, firstTurn, turnTimeLimit);
        InGameCanvas.SetActive(true);
    }
}