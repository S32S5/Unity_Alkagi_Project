/**
 * Manages first turn
 * If firstTurn is false, black, if true, white
 * 
 * @version 0.0.4
 * - Code opitimization
 * @author S3
 * @date 2024/02/20
*/

using UnityEngine;
using UnityEngine.UI;

public class FirstTurnControl_Script : MonoBehaviour
{
    private ToggleGroup group;
    private Toggle BlackToggle, WhiteToggle;

    private InitialSettingVariable_Script initSet;

    // Specifies
    private void Awake()
    {
        group = GameObject.Find("BlackWhiteFirstTurn_Panel").GetComponent<ToggleGroup>();
        BlackToggle = GameObject.Find("BlackFirstTurn_Toggle").GetComponent<Toggle>();
        WhiteToggle = GameObject.Find("WhiteFirstTurn_Toggle").GetComponent<Toggle>();

        initSet = GetComponent<InitialSettingVariable_Script>();
    }

    // Initialize first turn
    private void Start()
    {
        if (!initSet.GetFirstTurn())
            BlackToggle.isOn = true;
        else
            WhiteToggle.isOn = true;

        group.allowSwitchOff = false;
    }

    // Get first turn
    // 
    // @return bool
    public bool GetFirstTurn()
    {
        if (BlackToggle.isOn)
            return false;
        else
            return true;
    }

    // BlackToggle onclick listener
    public void BlackToggleChanged() { initSet.SetFirstTurn(false); }

    // WhiteToggle onclick listener
    public void WhiteToggleChanged() { initSet.SetFirstTurn(true); }
}