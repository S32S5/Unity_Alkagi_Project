/**
 * Controls related to turnTimeLimit
 * 
 * Script Explanation
 * - Check turnTimeLimit is null
 * - Return turnTimeLimit of int type
 * 
 * @version 0.0.3
 * - Load initial turnTimeLimit from json file
 * - Code optimization
 * @author S3
 * @date 2024/02/08
*/

using UnityEngine;
using UnityEngine.UI;

public class TurnTimeLimitControl_Script : MonoBehaviour
{
    private InputField TurnTimeLimit_InputField;

    // Specifies
    private void Awake()
    {
        TurnTimeLimit_InputField = GameObject.Find("TurnTimeLimit_InputField").GetComponent<InputField>();
        TurnTimeLimit_InputField.onValueChanged.AddListener(delegate {
            if (TurnTimeLimit_InputField.text.Equals("-") || TurnTimeLimit_InputField.text.Equals("0"))
                TurnTimeLimit_InputField.text = ""; 
        });
        TurnTimeLimit_InputField.onEndEdit.AddListener(delegate {
            if (TurnTimeLimit_InputField.text.Equals("") || int.Parse(TurnTimeLimit_InputField.text) < 5)
                TurnTimeLimit_InputField.text = "5";
            GetComponent<InitialSettingVariable_Script>().SetInitialTurnTimeLimit(TurnTimeLimit_InputField.text); 
        });
    }

    // Specifies when game start
    private void Start()
    {
        TurnTimeLimit_InputField.text = GetComponent<InitialSettingVariable_Script>().GetInitialTurnTimeLimit();
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