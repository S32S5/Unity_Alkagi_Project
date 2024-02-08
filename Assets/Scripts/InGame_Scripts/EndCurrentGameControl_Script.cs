/**
 * Manages related to EndCurrentGame
 * 
 * Script Explanation
 * - End current game
 * - Return EndCurrentGame_Panel is active
 * 
 * @version 0.3
 * - Changed to move to main when the game ends
 * @author S3
 * @date 2023/02/21
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
        GameObject.Find("Scene_Director").GetComponent<Scene_Director_Script>().MainOn();
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
     * @return bool EndCurrentGame_Panel.activeSelf
     */
    public bool EndCurrentGame_Panel_IsOn()
    {
        return EndCurrentGame_Panel.activeSelf;
    }
}