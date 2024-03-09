/**
 * Controls the input of turnTimeLimit
 * 
 * @version 1.0.0
 * - Change class name TurnTimeLimitControl_Script to TurnTimeLimitController
 * - Code optimization
 * @author S3
 * @date 2024/03/08
*/

using UnityEngine;
using UnityEngine.UI;

public class TurnTimeLimitControl_Script : MonoBehaviour
{
    InputField input;

    InitialSettingDataController data;

    private void Awake()
    {
        input = GameObject.Find("TurnTimeLimitInputField").GetComponent<InputField>();

        data = GameObject.Find("NewGameSettingPanel").GetComponent<InitialSettingDataController>();
    }

    // Init when game start
    private void Start() { Init(); }

    private void Init() { input.text = data.GetTimeLimit(); }

    // Prevents '-' or '0' from entering input
    public void TimeLimitOnValueChanged()
    {
        if (input.text.Equals("-") || input.text.Equals("0"))
            input.text = "";
    }

    // If a number less than 5 is entered in input, 5 is entered
    public void TimeLimitEndEdit()
    {
        if (input.text.Equals("") || int.Parse(input.text) < 5)
            input.text = "5";
        data.SetTimeLimit(input.text);
    }
}