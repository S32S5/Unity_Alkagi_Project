/**
 * Control turn and timeLimit
 * 
 * @version 1.0.0
 * - Change class name TurnControl_Script to TurnController
 * - Code optimization
 * @author S3
 * @date 2024/03/08
*/

using UnityEngine;
using UnityEngine.UI;

public class TurnController : MonoBehaviour
{
    InGameCanvasController inGame;

    private bool turn, turnEnd;
    private long turnTimeLimit, currentTimeLimit;
    private float secondTimer;

    GameObject blackTurn, whiteTurn;
    Text timeLimitTxt;

    GameObject powerGage;

    private void Awake()
    {
        inGame = GameObject.Find("InGameCanvas").GetComponent<InGameCanvasController>();

        blackTurn = GameObject.Find("BlackTurnDisplayPanel");
        whiteTurn = GameObject.Find("WhiteTurnDisplayPanel");
        timeLimitTxt = GameObject.Find("CurrentTimeLimitText").GetComponent<Text>();

        powerGage = GameObject.Find("AttackPowerGage");
    }

    private void Update()
    {
        CalculateSec();
        TurnEndOperation();
    }

    // Init
    //
    // @param bool, int
    public void Init(bool turn, int turnTimeLimit)
    {
        this.turn = turn;
        this.turnTimeLimit = turnTimeLimit;
        currentTimeLimit = turnTimeLimit;
        secondTimer = Time.deltaTime;

        DisplaysCurrentTurn();
        timeLimitTxt.text = currentTimeLimit.ToString();

        turnEnd = false;
    }

    private void CalculateSec()
    {
        if (inGame.GetPlayGame() && !turnEnd)
        {
            secondTimer += Time.deltaTime;
            if (secondTimer >= 1)
            {
                currentTimeLimit--;
                timeLimitTxt.text = currentTimeLimit.ToString();

                if (currentTimeLimit <= 0)
                    turnEnd = true;

                secondTimer = Time.deltaTime;
            }
        }
    }

    private void TurnEndOperation()
    {
        if (inGame.GetPlayGame() && turnEnd)
        {
            if (GetComponent<EggController>().GetAllStop())
            {
                powerGage.SetActive(false);

                turn = !turn;
                currentTimeLimit = turnTimeLimit;
                secondTimer = Time.deltaTime;

                DisplaysCurrentTurn();
                timeLimitTxt.text = currentTimeLimit.ToString();

                if (inGame.GetGameMode() == 1)
                    GetComponent<CameraController>().SetCam(turn);

                turnEnd = false;
            }
        }
    }

    // Return turn
    // 
    // @return bool
    public bool GetTurn() { return turn; }

    // Set turnEnd
    // 
    // @param bool
    public void SetTurnEnd(bool onOff) { turnEnd = onOff; }

    // Return turnEnd
    // 
    // @return bool
    public bool GetTurnEnd() { return turnEnd; }

    private void DisplaysCurrentTurn()
    {
        if (turn)
        {
            blackTurn.GetComponent<Image>().color = new Color32(255, 255, 255, 100);
            whiteTurn.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
        else
        {
            blackTurn.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            whiteTurn.GetComponent<Image>().color = new Color32(255, 255, 255, 100);
        }
    }
}