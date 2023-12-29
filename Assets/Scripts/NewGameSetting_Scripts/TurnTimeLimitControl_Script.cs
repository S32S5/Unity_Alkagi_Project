/**
 * Controls related to turnTimeLimit
 * 
 * Script Explanation
 * - Check turnTimeLimit is null
 * - Return turnTimeLimit of int type
 * 
 * @version 0.1, First version
 * @author S3
 * @date 2023/12/24
*/

using UnityEngine;
using UnityEngine.UI;

public class TurnTimeLimitControl_Script : MonoBehaviour
{
    private InputField TurnTimeLimit_InputField;

    /*
     * Specifies
     */
    private void Start()
    {
        TurnTimeLimit_InputField = GameObject.Find("NewGame_Canvas").transform.Find("NewGameSetting_Panel").transform.Find("TurnTimeLimit_Panel").transform.Find("TurnTimeLimit_InputField").GetComponent<InputField>();
    }

    /*
     * Check TurnTimeLimit_InputField is null
     * 
     * @return true or false
     */
    public bool CheckTurnTimeLimitNull()
    {
        if (TurnTimeLimit_InputField.text == "")
            return true;
        else
            return false;
    }

    /*
     * Return turnTimeLimit
     * 
     * @return int turnTimeLimit
     */
    public int GetTurnTimeLimit()
    {
        return int.Parse(TurnTimeLimit_InputField.text);
    }
}
