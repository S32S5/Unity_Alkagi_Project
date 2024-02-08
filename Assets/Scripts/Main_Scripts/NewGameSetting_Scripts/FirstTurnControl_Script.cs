/**
 * Manages first turn
 * If firstTurn is false, black, if true, white
 * 
 * Script Explanation
 * - Updates firstTurn checkbox
 * - FirstTurn checkbox behavior
 * - Return firstTurn
 * 
 * @version 0.0.3
 * - Load initial firstTurn from json file
 * - Code opitimization
 * @author S3
 * @date 2024/02/07
*/

using UnityEngine;
using UnityEngine.UI;

public class FirstTurnControl_Script : MonoBehaviour
{
    private Toggle BlackFirstTurn_Toggle, WhiteFirstTurn_Toggle;

    // Specifies
    private void Awake()
    {
        BlackFirstTurn_Toggle = GameObject.Find("BlackFirstTurn_Toggle").GetComponent<Toggle>();
        BlackFirstTurn_Toggle.onValueChanged.AddListener(delegate {
            GetComponent<InitialSettingVariable_Script>().SetInitialFirstTurn(false);
        });
        WhiteFirstTurn_Toggle = GameObject.Find("WhiteFirstTurn_Toggle").GetComponent<Toggle>();
        WhiteFirstTurn_Toggle.onValueChanged.AddListener(delegate {
            GetComponent<InitialSettingVariable_Script>().SetInitialFirstTurn(true);
        });

        if (GetComponent<InitialSettingVariable_Script>().GetInitialFirstTurn())
            WhiteFirstTurn_Toggle.isOn = true;
        else
            BlackFirstTurn_Toggle.isOn = true;
    }

    /*
     * Get first turn
     * 
     * @return bool first turn
     */
    public bool GetFirstTurn()
    {
        if (BlackFirstTurn_Toggle.isOn)
            return false;
        else
            return true;
    }
}