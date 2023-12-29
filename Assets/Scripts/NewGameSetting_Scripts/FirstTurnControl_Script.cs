/**
 * Manages first turn
 * If firstTurn is false, black, if true, white
 * 
 * Script Explanation
 * - Updates firstTurn checkbox
 * - FirstTurn checkbox behavior
 * - Return firstTurn
 * 
 * @version 0.1, First version
 * @author S3
 * @date 2023/12/27
*/

using UnityEngine;
using UnityEngine.UI;

public class FirstTurnControl_Script : MonoBehaviour
{
    private bool firstTurn;

    private GameObject BlackFirstTurn_Button, WhiteFirstTurn_Button;
    private Sprite UICheckBoxOn_Button, UICheckBoxOff_Button;

    /*
     * Specifies
     */
    private void Start()
    {
        GameObject BlackWhiteFirstTurn_Panel = GameObject.Find("NewGame_Canvas").transform.Find("NewGameSetting_Panel").transform.Find("FirstTurn_Panel").transform.Find("BlackWhiteFirstTurn_Panel").gameObject;
        GameObject BlackFirstTurn_Panel = BlackWhiteFirstTurn_Panel.transform.Find("BlackFirstTurn_Panel").gameObject;
        GameObject WhiteFirstTurn_Panel = BlackWhiteFirstTurn_Panel.transform.Find("WhiteFirstTurn_Panel").gameObject;
        BlackFirstTurn_Button = BlackFirstTurn_Panel.transform.Find("BlackFirstTurn_Button").gameObject;
        WhiteFirstTurn_Button = WhiteFirstTurn_Panel.transform.Find("WhiteFirstTurn_Button").gameObject;

        BlackFirstTurn_Button.GetComponent<Button>().onClick.AddListener(BlackFirstTurn_Button_Pressed);
        WhiteFirstTurn_Button.GetComponent<Button>().onClick.AddListener(WhiteFirstTurn_Button_Pressed);

        UICheckBoxOn_Button = Resources.Load<Sprite>("Images/UI/UICheckBoxOn_Button");
        UICheckBoxOff_Button = Resources.Load<Sprite>("Images/UI/UICheckBoxOff_Button");

        UpdateFirstTurn_Check();
    }

    /*
     * Updates the CheckBox to current firstTurn
     */
    public void UpdateFirstTurn_Check()
    {
        if (firstTurn)
        {
            BlackFirstTurn_Button.GetComponent<Image>().sprite = UICheckBoxOff_Button;
            WhiteFirstTurn_Button.GetComponent<Image>().sprite = UICheckBoxOn_Button;
        }
        else
        {
            BlackFirstTurn_Button.GetComponent<Image>().sprite = UICheckBoxOn_Button;
            WhiteFirstTurn_Button.GetComponent<Image>().sprite = UICheckBoxOff_Button;
        }
    }

    /*
     * Sets firstTurn to false
     */
    public void BlackFirstTurn_Button_Pressed()
    {
        firstTurn = false;
        UpdateFirstTurn_Check();
    }

    /*
     * Sets firstTurn to true
     */
    public void WhiteFirstTurn_Button_Pressed()
    {
        firstTurn = true;
        UpdateFirstTurn_Check();
    }

    /*
     * Return firstTurn
     */
    public bool GetFirstTurn()
    {
        return firstTurn;
    }
}