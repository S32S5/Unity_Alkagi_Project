/**
 * Manages related to EndCurrentGame
 * 
 * Script Explanation
 * - End current game
 * - Return EndCurrentGame_Panel is active
 * 
 * @version 0.1, First version
 * @author S3
 * @date 2023/12/28
*/

using UnityEngine;
using UnityEngine.UI;

public class EndCurrentGameControl_Script : MonoBehaviour
{
    private GameObject EndCurrentGame_Panel;
    private Button EndCurrentGameYes_Button, EndCurrentGameNo_Button;

    private InGame_Script ig_Script;

    // Specifies
    private void Awake()
    {
        EndCurrentGame_Panel = GameObject.Find("EndCurrentGame_Panel");
        EndCurrentGameYes_Button = GameObject.Find("EndCurrentGameYes_Button").GetComponent<Button>();
        EndCurrentGameNo_Button = GameObject.Find("EndCurrentGameNo_Button").GetComponent<Button>();
    }

    // Specifies when game start
    private void Start()
    {
        EndCurrentGameYes_Button.GetComponent<Button>().onClick.AddListener(EndCurrentGame);
        EndCurrentGameNo_Button.GetComponent<Button>().onClick.AddListener(EndCurrentGameCancel);

        ig_Script = GetComponent<InGame_Script>();
    }

    // Init
    public void Init()
    {
        EndCurrentGame_Panel.SetActive(false);
    }

    // Show EndCurrentGame_Panel
    public void ShowEndCurrentGame_Panel()
    {
        ig_Script.playGame = false;
        EndCurrentGame_Panel.SetActive(true);
    }

    // End current game
    public void EndCurrentGame()
    {
        GameObject.Find("Main_Director").GetComponent<Main_Director_Script>().NewGameSetting();
    }

    // End current game cancel
    public void EndCurrentGameCancel()
    {
        ig_Script.playGame = true;
        EndCurrentGame_Panel.SetActive(false);
    }

    /*
     * Return EndCurrentGame_Panel is active
     * 
     * @return true or false
     */
    public bool EndCurrentGame_Panel_EnDis()
    {
        return EndCurrentGame_Panel.activeSelf;
    }
}