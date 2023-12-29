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

    /*
     * Specifies
     */
    private void Start()
    {
        EndCurrentGame_Panel = GameObject.Find("InGame_Canvas").transform.Find("EndCurrentGame_Panel").gameObject;

        GameObject EndCurrentGameButton_Panel = EndCurrentGame_Panel.transform.Find("EndCurrentGameButton_Panel").gameObject;
        EndCurrentGameYes_Button = EndCurrentGameButton_Panel.transform.Find("EndCurrentGameYes_Button").GetComponent<Button>();
        EndCurrentGameNo_Button = EndCurrentGameButton_Panel.transform.Find("EndCurrentGameNo_Button").GetComponent<Button>();

        EndCurrentGameYes_Button.GetComponent<Button>().onClick.AddListener(EndCurrentGame);
        EndCurrentGameNo_Button.GetComponent<Button>().onClick.AddListener(EndCurrentGameCancel);

        ig_Script = GetComponent<InGame_Script>();
    }

    /*
     * Show EndCurrentGame_Panel
     */
    public void ShowEndCurrentGame_Panel()
    {
        ig_Script.playGame = false;
        EndCurrentGame_Panel.SetActive(true);
    }

    /*
     * End current game
     */
    public void EndCurrentGame()
    {
        ig_Script.SetActiveInGame_Panel(false);
        EndCurrentGame_Panel.SetActive(false);
        GetComponent<Main_Director_Script>().enableNewGameSetting();
    }

    /*
     * End current game cancel
     */
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
