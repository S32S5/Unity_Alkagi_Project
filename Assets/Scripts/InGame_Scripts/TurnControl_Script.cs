/**
 * Controls turn and time limits.
 * 
 * Script Explanation
 * - Turn - black: false, white: true
 * - Init turn and turnTimeLimit
 * - Set turn and turnEnd
 * - Return turn and turnEnd
 * - Displays current turn and turnTimeLimit
 * 
 * @version 0.1, First version
 * @author S3
 * @date 2023/12/28
*/

using UnityEngine;
using UnityEngine.UI;

public class TurnControl_Script : MonoBehaviour
{
    private bool currentTurn, turnEnd;
    private long turnTimeLimit, currentTimeLimit;
    private float secondTimer;

    private GameObject BlackTurnDisplay_Panel, WhiteTurnDisplay_Panel;
    private Text CurrentTimeLimit_Text;

    private InGame_Script ig_Script;
    private CameraControl_Script cc_Script;

    /*
     * Specifies
     */
    private void Start()
    {
        GameObject InGame_Panel = GameObject.Find("InGame_Canvas").transform.Find("InGame_Panel").gameObject;
        BlackTurnDisplay_Panel = InGame_Panel.transform.Find("BlackTurnDisplay_Panel").gameObject;
        WhiteTurnDisplay_Panel = InGame_Panel.transform.Find("WhiteTurnDisplay_Panel").gameObject;

        CurrentTimeLimit_Text = InGame_Panel.transform.Find("CurrentTimeLimit_Text").GetComponent<Text>();

        ig_Script = GetComponent<InGame_Script>();
        cc_Script = GetComponent<CameraControl_Script>();
    }

    /*
     * Decrease currentTimeLimit and turnEnd operation
     */
    private void Update()
    {
        if (ig_Script.playGame && !turnEnd)
            DecreaseCurrentTimeLimit();
        else if (ig_Script.playGame && turnEnd)
        {
            if (GetComponent<EggControl_Script>().GetAllEggsStop())
            {
                if (currentTurn)
                    GetComponent<CameraControl_Script>().UpdateCameraRotation(360);
                else
                    GetComponent<CameraControl_Script>().UpdateCameraRotation(180);
            }
        }
    }

    /*
     * Init turn
     * 
     * @param bool firstTurn, int turnTimeLimit
     */
    public void InitTurn(bool firstTurn, int turnTimeLimit)
    {
        currentTurn = firstTurn;
        if (currentTurn)
            cc_Script.InitCamera(180);
        else
            cc_Script.InitCamera(0);

        this.turnTimeLimit = turnTimeLimit;
        currentTimeLimit = turnTimeLimit;
        secondTimer = Time.deltaTime;

        DisplaysCurrentTurn();
        DisplaysCurrentTimeLimit();

        turnEnd = false;
    }

    /*
     * Decrease currentTimeLimit by 1
     */
    private void DecreaseCurrentTimeLimit()
    {
        secondTimer += Time.deltaTime;
        if (secondTimer >= 1)
        {
            currentTimeLimit --;
            DisplaysCurrentTimeLimit();

            if (currentTimeLimit <= 0)
                turnEnd = true;

            secondTimer = Time.deltaTime;
        }
    }

    /*
     * Set current turn
     * 
     * @param bool turn
     */
    public void SetTurn(bool turn)
    {
        currentTurn = turn;
        currentTimeLimit = turnTimeLimit;

        DisplaysCurrentTurn();
        DisplaysCurrentTimeLimit();

        turnEnd = false;
    }

    /*
     * Set turnEnd
     * 
     * @param bool onOff
     */
    public void SetTurnEnd(bool onOff)
    {
        turnEnd = onOff;
    }

    /*
     * Return current turn
     * 
     * @return bool
     */
    public bool GetCurrentTurn()
    {
        return currentTurn;
    }

    /*
     * Return turnEnd
     * 
     * @return bool
     */
    public bool GetTurnEnd()
    {
        return turnEnd;
    }

    /*
     * Displays the current turn
     */
    public void DisplaysCurrentTurn()
    {
        if (currentTurn)
        {
            BlackTurnDisplay_Panel.GetComponent<Image>().color = new Color32(255, 255, 255, 100);
            WhiteTurnDisplay_Panel.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
        else
        {
            BlackTurnDisplay_Panel.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            WhiteTurnDisplay_Panel.GetComponent<Image>().color = new Color32(255, 255, 255, 100);
        }
    }

    /*
     * Displays the current time limit
     */
    public void DisplaysCurrentTimeLimit()
    {
        CurrentTimeLimit_Text.text = currentTimeLimit.ToString();
    }
}