/**
 * Manages to related game result
 * 
 * Script Explanation
 * - Show game result
 * - Return GetGameResult_Panel is active
 * 
 * @version 0.1, First version
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

    /*
     * Specifies
     */
    private void Start()
    {
        GameResult_Panel = GameObject.Find("InGame_Canvas").transform.Find("GameResult_Panel").gameObject;

        GameObject BlackResult_Panel = GameResult_Panel.transform.Find("BlackResult_Panel").gameObject;
        BlackWinLose_Text = BlackResult_Panel.transform.Find("BlackWinLose_Text").GetComponent<Text>();
        BlackScore_Text = BlackResult_Panel.transform.Find("BlackScore_Text").GetComponent<Text>();

        GameObject WhiteResult_Panel = GameResult_Panel.transform.Find("WhiteResult_Panel").gameObject;
        WhiteWinLose_Text = WhiteResult_Panel.transform.Find("WhiteWinLose_Text").GetComponent<Text>();
        WhiteScore_Text = WhiteResult_Panel.transform.Find("WhiteScore_Text").GetComponent<Text>();

        GameResultCheck_Button = GameResult_Panel.transform.Find("GameResultCheck_Button").GetComponent<Button>();
        GameResultCheck_Button.GetComponent<Button>().onClick.AddListener(CheckGameResult);

        ec_Script = GetComponent<EggControl_Script>();
        ecgc_Script = GetComponent<EndCurrentGameControl_Script>();
    }

    /*
     * Show game result
     */
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

    /*
     * Check game result
     */
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