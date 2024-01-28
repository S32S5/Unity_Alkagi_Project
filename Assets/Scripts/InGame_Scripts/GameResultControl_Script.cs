/**
 * Manages to related game result
 * 
 * Script Explanation
 * - Show game result
 * - Return GetGameResult_Panel is active
 * 
 * @version 0.0.2
 *  - Code optimization
 * @author S3
 * @date 2023/12/8
*/

using UnityEngine;
using UnityEngine.UI;

public class GameResultControl_Script : MonoBehaviour
{
    private GameObject GameResult_Panel;

    private Text BlackWinLose_Text, WhiteWinLose_Text;
    private Text BlackScore_Text, WhiteScore_Text;

    private Button GameResultCheck_Button;

    private EggControl_Script ec_Script;
    private EndCurrentGameControl_Script ecgc_Script;

    // Specifies
    private void Awake()
    {
        GameResult_Panel = GameObject.Find("GameResult_Panel");
        BlackWinLose_Text = GameObject.Find("BlackWinLose_Text").GetComponent<Text>();
        BlackScore_Text = GameObject.Find("BlackScore_Text").GetComponent<Text>();
        WhiteWinLose_Text = GameObject.Find("WhiteWinLose_Text").GetComponent<Text>();
        WhiteScore_Text = GameObject.Find("WhiteScore_Text").GetComponent<Text>();

        GameResultCheck_Button = GameObject.Find("GameResultCheck_Button").GetComponent<Button>();
    }

    // Specifies when game start
    private void Start()
    {
        GameResultCheck_Button.GetComponent<Button>().onClick.AddListener(CheckGameResult);

        ec_Script = GetComponent<EggControl_Script>();
        ecgc_Script = GetComponent<EndCurrentGameControl_Script>();
    }

    // Init
    public void Init()
    {
        GameResult_Panel.SetActive(false);
    }

    // Show game result
    public void ShowGameResult()
    {
        if(ec_Script.GetEggsCount(true) == 0)
        {
            BlackWinLose_Text.GetComponent<Text>().text = "½Â";
            BlackWinLose_Text.GetComponent<Text>().color = Color.red;

            WhiteWinLose_Text.GetComponent<Text>().text = "ÆÐ";
            WhiteWinLose_Text.GetComponent<Text>().color = Color.blue;
        }
        else
        {
            BlackWinLose_Text.GetComponent<Text>().text = "ÆÐ";
            BlackWinLose_Text.GetComponent<Text>().color = Color.blue;

            WhiteWinLose_Text.GetComponent<Text>().text = "½Â";
            WhiteWinLose_Text.GetComponent<Text>().color = Color.red;
        }

        BlackScore_Text.GetComponent<Text>().text = ec_Script.GetEggsCount(false).ToString();
        WhiteScore_Text.GetComponent<Text>().text = ec_Script.GetEggsCount(true).ToString();

        GameResult_Panel.SetActive(true);
    }

    // Check game result
    public void CheckGameResult()
    {
        GameResult_Panel.SetActive(false);
        ecgc_Script.EndCurrentGame();
    }

    /*
     * Get GameResult_Panel activeSelf
     * 
     * @return true or false
     */
    public bool GetGameResult_Panel_EnDis()
    {
        return GameResult_Panel.activeSelf;
    }
}