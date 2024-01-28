/**
 * Manages related to InitialEggNumber
 * 
 * Script Explanation
 * - Updates text of initialEggNumber
 * - Decreases, increases initialEggNumber
 * - Return initialEggNumber
 * 
 * @version 0.0.2, Code optimization
 * @author S3
 * @date 2024/01/28
*/

using UnityEngine;
using UnityEngine.UI;

public class InitialEggNumberControl_Script : MonoBehaviour
{
    private int initialEggNumber;

    private Text InitialEggNumberSettedNumber_Text;
    private Button InitialEggNumberDecrease_Button, InitialEggNumberIncrease_Button;

    // Specifies
    private void Awake()
    {
        GameObject InitialEggNumberSetting_Panel = GameObject.Find("NewGame_Canvas").transform.Find("NewGameSetting_Panel").transform.Find("InitialEggNumber_Panel").transform.Find("InitialEggNumberSetting_Panel").gameObject;
        InitialEggNumberSettedNumber_Text = InitialEggNumberSetting_Panel.transform.Find("InitialEggSettedNumber_Text").GetComponent<Text>();
        InitialEggNumberDecrease_Button = InitialEggNumberSetting_Panel.transform.Find("InitialEggNumberDecrease_Button").GetComponent<Button>();
        InitialEggNumberIncrease_Button = InitialEggNumberSetting_Panel.transform.Find("InitialEggNumberIncrease_Button").GetComponent<Button>();

        InitialEggNumberDecrease_Button.onClick.AddListener(DecreasesInitialEggNumber);
        InitialEggNumberIncrease_Button.onClick.AddListener(IncreasesInitialEggNumber);
    }

    // Specifies when game start
    private void Start()
    {
        initialEggNumber = 4;
        UpdateInitialEggSettedNumber_Text();
    }

    // Update text of InitialEggNumberSettedNumber_Text
    private void UpdateInitialEggSettedNumber_Text()
    {
        InitialEggNumberSettedNumber_Text.text = initialEggNumber.ToString();
    }

    // Decreases initialEggNumber by 1
    public void DecreasesInitialEggNumber()
    {
        if (initialEggNumber != 1)
        {
            initialEggNumber--;
            UpdateInitialEggSettedNumber_Text();
        }
    }

    // Increases initialEggNumber by 1
    public void IncreasesInitialEggNumber()
    {
        if (initialEggNumber != 7)
        {
            initialEggNumber++;
            UpdateInitialEggSettedNumber_Text();
        }
    }

    /*
     * Get initialEggNumber
     * 
     * @return initialEggNumber
     */
    public int GetInitialEggNumber()
    {
        return initialEggNumber;
    }
}