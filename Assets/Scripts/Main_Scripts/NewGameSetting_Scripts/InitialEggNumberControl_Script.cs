/**
 * Manages related to InitialEggNumber
 * 
 * Script Explanation
 * - Updates text of initialEggNumber
 * - Decreases, increases initialEggNumber
 * - Return initialEggNumber
 * 
 * @version 0.0.3
 * - Load initial initialEggNumber from json file
 * @author S3
 * @date 2024/02/07
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
        InitialEggNumberSettedNumber_Text = GameObject.Find("InitialEggSettedNumber_Text").GetComponent<Text>();

        InitialEggNumberDecrease_Button = GameObject.Find("InitialEggNumberDecrease_Button").GetComponent<Button>();
        InitialEggNumberDecrease_Button.onClick.AddListener(() =>
        {
            if (initialEggNumber != 1)
            {
                initialEggNumber--;
                UpdateInitialEggSettedNumber_Text();
            }
        });
        InitialEggNumberIncrease_Button = GameObject.Find("InitialEggNumberIncrease_Button").GetComponent<Button>();
        InitialEggNumberIncrease_Button.onClick.AddListener(() =>
        {
            if (initialEggNumber != 7)
            {
                initialEggNumber++;
                UpdateInitialEggSettedNumber_Text();
            }
        });
    }

    // Specifies when game start
    private void Start()
    {
        initialEggNumber = GetComponent<InitialSettingVariable_Script>().GetInitialEggNumber();
        UpdateInitialEggSettedNumber_Text();
    }

    // Update text of InitialEggNumberSettedNumber_Text
    private void UpdateInitialEggSettedNumber_Text()
    {
        GetComponent<InitialSettingVariable_Script>().SetInitialEggNumber(initialEggNumber);
        InitialEggNumberSettedNumber_Text.text = initialEggNumber.ToString();
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