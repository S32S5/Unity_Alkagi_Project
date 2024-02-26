/**
 * Controls turn and time limits.
 * 
 * @version 0.0.4
 * - Code optimization
 * @author S3
 * @date 2024/02/22
*/

using UnityEngine;
using UnityEngine.UI;

public class TurnControl_Script : MonoBehaviour
{
    private bool turn, turnEnd;
    private long turnTimeLimit, currentTimeLimit;
    private float secondTimer;

    private GameObject blackTurnDisplay, whiteTurnDisplay;
    private Text timeLimitText;

    private InGame_Script inGame;

    // Specifies
    private void Awake()
    {
        blackTurnDisplay = GameObject.Find("BlackTurnDisplay_Panel");
        whiteTurnDisplay = GameObject.Find("WhiteTurnDisplay_Panel");

        timeLimitText = GameObject.Find("CurrentTimeLimit_Text").GetComponent<Text>();
    }

    // Specifies when game start
    private void Start()
    {
        inGame = GetComponent<InGame_Script>();
    }

    // Decrease currentTimeLimit and turnEnd operation
    private void Update()
    {
        if (inGame.playGame && !turnEnd)
        {
            secondTimer += Time.deltaTime;
            if (secondTimer >= 1)
            {
                currentTimeLimit--;
                timeLimitText.text = currentTimeLimit.ToString();

                if (currentTimeLimit <= 0)
                    turnEnd = true;

                secondTimer = Time.deltaTime;
            }
        }
        else if (inGame.playGame && turnEnd)
        {
            if (GetComponent<EggControl_Script>().GetAllStop())
            {
                turn = !turn;
                DisplaysCurrentTurn();
                currentTimeLimit = turnTimeLimit;
                secondTimer = Time.deltaTime;
                timeLimitText.text = currentTimeLimit.ToString();

                if(inGame.GetGameMode() == 0)
                    GetComponent<CameraControl_Script>().SetCam(turn);

                turnEnd = false;
            }
        }
    }

    // Init turn
    //
    // @param bool, int
    public void InitTurn(bool turn, int turnTimeLimit)
    {
        this.turn = turn;

        this.turnTimeLimit = turnTimeLimit;
        currentTimeLimit = turnTimeLimit;
        secondTimer = Time.deltaTime;

        DisplaysCurrentTurn();
        timeLimitText.text = currentTimeLimit.ToString();

        turnEnd = false;
    }

    // Return turn
    // 
    // @return bool
    public bool GetTurn() { return turn; }

    // Return turnEnd
    // 
    // @return bool
    public bool GetTurnEnd() { return turnEnd; }

    // Set turnEnd
    // 
    // @param bool
    public void SetTurnEnd(bool onOff) { turnEnd = onOff; }

    // Displays the current turn
    private void DisplaysCurrentTurn()
    {
        if (turn)
        {
            blackTurnDisplay.GetComponent<Image>().color = new Color32(255, 255, 255, 100);
            whiteTurnDisplay.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
        else
        {
            blackTurnDisplay.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            whiteTurnDisplay.GetComponent<Image>().color = new Color32(255, 255, 255, 100);
        }
    }
}