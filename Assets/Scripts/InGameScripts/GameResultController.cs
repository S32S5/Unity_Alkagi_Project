/**
 * Control game result panel
 * 
 * @version 1.0.0
 * - Change class name GameResultControl_Script to GameResultController
 *  - Code optimization
 * @author S3
 * @date 2024/03/07
*/

using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class GameResultController : MonoBehaviour, Panel
{
    Text blackWinLose, whiteWinLose;
    Text blackScore, whiteScore;

    InGameCanvasController inGame;
    EggController egg;
    EndGameController endGame;

    GameObject netManager;
    NetworkManager net;

    private void Awake()
    {
        blackWinLose = GameObject.Find("BlackWinLoseText").GetComponent<Text>();
        whiteWinLose = GameObject.Find("WhiteWinLoseText").GetComponent<Text>();
        blackScore = GameObject.Find("BlackScoreText").GetComponent<Text>();
        whiteScore = GameObject.Find("WhiteScoreText").GetComponent<Text>();

        inGame = GameObject.Find("InGameCanvas").GetComponent<InGameCanvasController>();
        egg = GameObject.Find("InGamePanel").GetComponent<EggController>();
        endGame = GameObject.Find("EndGamePanel").GetComponent<EndGameController>();

        netManager = GameObject.Find("NetworkManager");
        net = netManager.GetComponent<NetworkManager>();
    }

    private void Update() { EscListener(); }

    public void Init() { }

    public void SetPanel(bool OnOff) 
    { 
        gameObject.SetActive(OnOff);

        if (OnOff)
            ShowGameResult();
    }

    public bool GetPanelIsOn() { return gameObject.activeSelf; }

    public void EscListener()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameObject.activeSelf)
                CheckGameResult();
        }
    }

    // Show game result
    public void ShowGameResult()
    {
        if (egg.GetEggsCount(true) == 0)
        {
            blackWinLose.GetComponent<Text>().text = "½Â";
            blackWinLose.GetComponent<Text>().color = Color.red;

            whiteWinLose.GetComponent<Text>().text = "ÆÐ";
            whiteWinLose.GetComponent<Text>().color = Color.blue;
        }
        else
        {
            blackWinLose.GetComponent<Text>().text = "ÆÐ";
            blackWinLose.GetComponent<Text>().color = Color.blue;

            whiteWinLose.GetComponent<Text>().text = "½Â";
            whiteWinLose.GetComponent<Text>().color = Color.red;
        }

        blackScore.GetComponent<Text>().text = egg.GetEggsCount(false).ToString();
        whiteScore.GetComponent<Text>().text = egg.GetEggsCount(true).ToString();

        if (inGame.GetGameMode() == 2)
            ShutDownNet();
    }

    public void CheckGameResult()
    {
        gameObject.SetActive(false);
        endGame.EndGame();
    }

    private void ShutDownNet()
    {
        net.Shutdown();
        netManager.SetActive(false);
    }
}