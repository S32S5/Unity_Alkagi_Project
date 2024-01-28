/**
 * Controls related to turnTimeLimit
 * 
 * Script Explanation
 * - Check turnTimeLimit is null
 * - Return turnTimeLimit of int type
 * 
 * @version 0.2, Code optimization
 * @author S3
 * @date 2023/01/28
*/

using UnityEngine;
using UnityEngine.UI;

public class TurnTimeLimitControl_Script : MonoBehaviour
{
    private InputField TurnTimeLimit_InputField;

    // Specifies
    private void Awake()
    {
        TurnTimeLimit_InputField = GameObject.Find("TurnTimeLimit_Panel").transform.Find("TurnTimeLimit_InputField").GetComponent<InputField>();
        TurnTimeLimit_InputField.onValueChanged.AddListener(delegate { LimitTurnTimeLimit_InputField(); });
    }

    // Limit TurnTimeLimit_InputField is "-", "0"
    private void LimitTurnTimeLimit_InputField()
    {
        if (TurnTimeLimit_InputField.text.Equals("-") || TurnTimeLimit_InputField.text.Equals("0"))
            TurnTimeLimit_InputField.text = "";
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