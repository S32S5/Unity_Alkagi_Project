/**
 * Control first turn toggles
 * If firstTurn is false, black, if true, white
 * 
 * @version 1.0.0
 * - Change class name FirstTurnControl_Script to FirstTurnController
 * - Code opitimization
 * @author S3
 * @date 2024/03/08
*/

using UnityEngine;
using UnityEngine.UI;

public class FirstTurnControl_Script : MonoBehaviour
{
    ToggleGroup group;
    Toggle black, white;

    GameSettingDataController data;

    private void Awake()
    {
        group = GameObject.Find("BlackWhiteFirstTurnPanel").GetComponent<ToggleGroup>();
        black = GameObject.Find("BlackFirstTurnToggle").GetComponent<Toggle>();
        white = GameObject.Find("WhiteFirstTurnToggle").GetComponent<Toggle>();

        data = GameObject.Find("SceneDirector").GetComponent<GameSettingDataController>();
    }

    // Init when game start
    private void Start() { Init(); }

    private void Init()
    {
        if (!data.GetFirstTurn())
            black.isOn = true;
        else
            white.isOn = true;

        group.allowSwitchOff = false;
    }

    // BlackToggle onclick listener
    public void BlackToggleChanged() { data.SetFirstTurn(false); }

    // WhiteToggle onclick listener
    public void WhiteToggleChanged() { data.SetFirstTurn(true); }
}