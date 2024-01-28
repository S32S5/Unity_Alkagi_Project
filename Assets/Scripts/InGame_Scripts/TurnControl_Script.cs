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
 * @version 0.0.2, Code optimization
 * @author S3
 * @date 2024/01/28
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

    // Specifies
    private void Awake()
    {
        BlackTurnDisplay_Panel = GameObject.Find("BlackTurnDisplay_Panel");
        WhiteTurnDisplay_Panel = GameObject.Find("WhiteTurnDisplay_Panel");

        CurrentTimeLimit_Text = GameObject.Find("CurrentTimeLimit_Text").GetComponent<Text>();

    }

    // Specifies when game start
    private void Start()
    {
        ig_Script = GetComponent<InGame_Script>();
        cc_Script = GetComponent<CameraControl_Script>();
    }

    // Decrease currentTimeLimit and turnEnd operation
    private void Update()
    {
        if (ig_Script.playGame && !turnEnd)
        {
            void DecreaseCurrentTimeLimit()
            {
                secondTimer += Time.deltaTime;
                if (secondTimer >= 1)
                {
                    currentTimeLimit--;
                    DisplaysCurrentTimeLimit();

                    if (currentTimeLimit <= 0)
                        turnEnd = true;

                    secondTimer = Time.deltaTime;
                }
            }
            DecreaseCurrentTimeLimit();
        }
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

        this.turnTimeLimit = turnTimeLimit;
        currentTimeLimit = turnTimeLimit;
        secondTimer = Time.deltaTime;

        DisplaysCurrentTurn();
        DisplaysCurrentTimeLimit();

        turnEnd = false;
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

    // Displays the current turn
    private void DisplaysCurrentTurn()
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

    // Displays the current time limit
    private void DisplaysCurrentTimeLimit()
    {
        CurrentTimeLimit_Text.text = currentTimeLimit.ToString();
    }
}