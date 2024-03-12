/**
 * Control inGame panel and variables
 * 
 * @version 1.1.0
 * - Add aiLevel
 * - Move SetGame to panel
 * - Code optimization
 * @author S3
 * @date 2024/03/10
*/

using UnityEngine;

public class InGameCanvasController : MonoBehaviour, Canvas
{
    private int gameMode;
    private int aiLevel;
    private bool playGame;

    EndGameController end;
    PauseGameController pause;
    DisconnectedController disconnected;
    GameResultController result;
    InGamePanelController panel;

    private void Awake()
    {
        end = GameObject.Find("EndGamePanel").GetComponent<EndGameController>();
        pause = GameObject.Find("PauseGamePanel").GetComponent<PauseGameController>();
        disconnected = GameObject.Find("DisconnectedPanel").GetComponent<DisconnectedController>();
        result = GameObject.Find("GameResultPanel").GetComponent<GameResultController>();
        panel = GameObject.Find("InGamePanel").GetComponent<InGamePanelController>();
    }

    private void Update() { SetPlayGame(); }

    public void Init()
    {
        end.SetPanel(false);
        pause.SetPanel(false);
        disconnected.SetPanel(false);
        result.SetPanel(false);
        panel.SetPanel(true);
    }

    public void SetCanvas(bool OnOff) 
    {
        gameObject.SetActive(OnOff);

        if (OnOff)
            Init();
    }

    // Play game set false if end or pause or disconnected or result, else set true 
    public void SetPlayGame() 
    {
        if (end.GetPanelIsOn() || pause.GetPanelIsOn() || disconnected.GetPanelIsOn() || result.GetPanelIsOn())
            playGame = false;
        else
            playGame = true;
    }

    // Return playGame
    //
    // @return bool
    public bool GetPlayGame() { return playGame; }

    // Set gameMode
    //
    // @param int
    public void SetGameMode(int gameMode) { this.gameMode = gameMode; }

    // Return gameMode
    //
    // @return int
    public int GetGameMode() { return gameMode; }

    // Set aiLevel
    //
    // @param int
    public void SetAiLevel(int aiLevel) { this.aiLevel = aiLevel; }

    // Return aiLevel
    //
    // @return int
    public int GetAiLevel() { return aiLevel; }
}