/**
 * Init game scene, set main and inGame
 * 
 * @version 1.0.0
 *  - Change class name Scene_Director_Script to SceneDirector
 *  - Code optimization
 * @author S3
 * @date 2024/03/08
*/

using UnityEngine;

public class SceneDirector : MonoBehaviour
{
    MainCanvasController main;
    InGameCanvasController inGame;

    GameObject netManager;

    private void Awake()
    {
        main = GameObject.Find("MainCanvas").GetComponent<MainCanvasController>();
        inGame = GameObject.Find("InGameCanvas").GetComponent<InGameCanvasController>();

        netManager = GameObject.Find("NetworkManager");
    }

    // Init when game start
    private void Start() { Init(); }

    private void Init()
    {
        GetComponent<AudioSource>().volume = GetComponent<OptionDataController>().GetBgmVolume();
        main.Init();
        inGame.SetCanvas(false);

        netManager.SetActive(false);
    }

    // Set active MainCanvas
    public void Main()
    {
        inGame.SetCanvas(false);
        main.SetCanvas(true);
    }

    // Close NewGameSetting and proceed to InGame
    public void InGame()
    {
        main.SetCanvas(false);
        inGame.SetCanvas(true);
    }
}