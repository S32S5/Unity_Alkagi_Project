/**
 * Manages to related game result
 * 
 * @version 0.0.4
 *  - Code optimization
 * @author S3
 * @date 2024/02/18
*/

using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class GameResultControl_Script : MonoBehaviour
{
    private GameObject panel;
    private Text blackWinLose, whiteWinLose;
    private Text blackScore, whiteScore;
    private Button checkcButton;

    private InGame_Script inGame;
    private EggControl_Script egg;
    private EndCurrentGameControl_Script endGame;
    private PlayerWaiting_Script player;
    private JoinWaiting_Script join;

    private GameObject netManager;
    private NetworkManager net;

    // Specifies
    private void Awake()
    {
        panel = GameObject.Find("GameResult_Panel");
        blackWinLose = GameObject.Find("BlackWinLose_Text").GetComponent<Text>();
        whiteWinLose = GameObject.Find("WhiteWinLose_Text").GetComponent<Text>();
        blackScore = GameObject.Find("BlackScore_Text").GetComponent<Text>();
        whiteScore = GameObject.Find("WhiteScore_Text").GetComponent<Text>();

        checkcButton = GameObject.Find("GameResultCheck_Button").GetComponent<Button>();

        inGame = GameObject.Find("InGame_Panel").GetComponent<InGame_Script>();
        egg = GameObject.Find("InGame_Panel").GetComponent<EggControl_Script>();
        endGame = GameObject.Find("EndCurrentGame_Panel").GetComponent<EndCurrentGameControl_Script>();
        player = GameObject.Find("PlayerWaiting_Panel").GetComponent<PlayerWaiting_Script>();
        join = GameObject.Find("JoinWaiting_Panel").GetComponent<JoinWaiting_Script>();

        netManager = GameObject.Find("NetworkManager");
        net = netManager.GetComponent<NetworkManager>();
    }

    // Specifies when game start
    private void Start()
    {
        checkcButton.GetComponent<Button>().onClick.AddListener(CheckGameResult);

        panel.SetActive(false);
    }

    // Esc
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (panel.activeSelf)
                CheckGameResult();
        }
    }

    // Show game result
    public void ShowGameResult()
    {
        if(egg.GetEggsCount(true) == 0)
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

        panel.SetActive(true);

        if (inGame.GetGameMode() == 1)
        {
            if (net.IsHost)
                player.SetPanel(false);
            else
                join.SetPanel(false);
            
            net.Shutdown();
            netManager.SetActive(false);
        }
    }

    // Check game result
    public void CheckGameResult()
    {
        panel.SetActive(false);
        endGame.EndGame();
    }

    // Get GameResult_Panel is active
    //
    // @return bool
    public bool GetPanelIsOn() { return panel.activeSelf; }
}