/**
 * Control inGame panel and variables
 * 
 * @version 1.0.0, new class
 * @author S3
 * @date 2024/03/09
*/

using Unity.Netcode;
using UnityEngine;

public class InGameCanvasController : MonoBehaviour, Canvas
{
    private int gameMode;
    private bool playGame;

    NewGameSettingController newGame;

    EndGameController end;
    PauseGameController pause;
    DisconnectedController disconnected;
    GameResultController result;

    InitialSettingDataController data;
    EggController egg;
    TurnController turn;
    CameraController cam;

    NetworkManager net;

    private void Awake()
    {
        newGame = GameObject.Find("NewGameSettingPanel").GetComponent<NewGameSettingController>();

        end = GameObject.Find("EndGamePanel").GetComponent<EndGameController>();
        pause = GameObject.Find("PauseGamePanel").GetComponent<PauseGameController>();
        disconnected = GameObject.Find("DisconnectedPanel").GetComponent<DisconnectedController>();
        result = GameObject.Find("GameResultPanel").GetComponent<GameResultController>();

        data = GameObject.Find("NewGameSettingPanel").GetComponent<InitialSettingDataController>();
        egg = GetComponent<EggController>();
        turn = GetComponent<TurnController>();
        cam = GetComponent<CameraController>();

        net = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
    }

    private void Update()
    {
        if(end.GetPanelIsOn() || pause.GetPanelIsOn() || disconnected.GetPanelIsOn() || result.GetPanelIsOn())
            playGame = false;
        else
            playGame = true;
    }

    public void Init()
    {
        end.SetPanel(false);
        pause.SetPanel(false);
        disconnected.SetPanel(false);
        result.SetPanel(false);

        SetGame();
    }

    public void SetCanvas(bool OnOff) 
    {
        gameObject.SetActive(OnOff);

        if (OnOff)
            Init();
    }

    // Set new game
    private void SetGame()
    {
        egg.Init(data.GetEggNum());
        turn.Init(data.GetFirstTurn(), int.Parse(data.GetTimeLimit()));

        if (gameMode == 1)
            cam.SetCam(data.GetFirstTurn());
        else if (gameMode == 2)
        {
            if (net.IsHost)
                cam.SetCam(false);
            else
                cam.SetCam(true);
        }

        playGame = true;
    }

    // Set gameMode
    //
    // @param int
    public void SetGameMode(int gameMode) { this.gameMode = gameMode; }

    // Return gameMode
    //
    // @return int
    public int GetGameMode() { return gameMode; }

    // Set playGame
    //
    // @param bool
    public void SetPlayGame(bool OnOff) { playGame = OnOff; }

    // Return playGame
    //
    // @return bool
    public bool GetPlayGame() { return playGame; }
}