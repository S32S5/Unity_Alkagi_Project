/**
 * Controls related to turnTimeLimit
 * 
 * @version 0.0.4
 * - Code optimization
 * @author S3
 * @date 2024/02/20
*/

using UnityEngine;
using UnityEngine.UI;

public class TurnTimeLimitControl_Script : MonoBehaviour
{
    private InputField timeLimitInput;

    private InitialSettingVariable_Script initSet;

    // Specifies
    private void Awake()
    {
        timeLimitInput = GameObject.Find("TurnTimeLimit_InputField").GetComponent<InputField>();

        initSet = GetComponent<InitialSettingVariable_Script>();
    }

    // Initialize timeLimitInput text
    private void Start() { timeLimitInput.text = initSet.GetTimeLimit(); }

    // Return turnTimeLimit
    // 
    // @return int
    public int GetTurnTimeLimit() { return int.Parse(timeLimitInput.text); }

    // TimeLimit changed listener
    public void TimeLimitChanged()
    {
        if (timeLimitInput.text.Equals("-") || timeLimitInput.text.Equals("0"))
            timeLimitInput.text = "";
    }

    // TimeLimit end edit listener
    public void TimeLimitEndEdit()
    {
        if (timeLimitInput.text.Equals("") || int.Parse(timeLimitInput.text) < 5)
            timeLimitInput.text = "5";
        initSet.SetTimeLimit(timeLimitInput.text);
    }
}