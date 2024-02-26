/**
 * Manages related to InitialEggNumber
 * 
 * @version 0.0.4
 * - Code optimization
 * @author S3
 * @date 2024/02/20
*/

using UnityEngine;
using UnityEngine.UI;

public class InitialEggNumberControl_Script : MonoBehaviour
{
    private Text settedText;

    private int eggNumber;

    // Specifies
    private void Awake()
    {
        settedText = GameObject.Find("InitialEggSettedNumber_Text").GetComponent<Text>();
    }

    // Initialize eggNumber when game start
    private void Start()
    {
        eggNumber = GetComponent<InitialSettingVariable_Script>().GetEggNum();
        UpdateSettedNumberText();
    }

    // Update text of settedNumberText
    private void UpdateSettedNumberText()
    {
        settedText.text = eggNumber.ToString();
        GetComponent<InitialSettingVariable_Script>().SetEggNum(eggNumber);
    }

    // Return eggNumber
    // 
    // @return int
    public int GetEggNumber() { return eggNumber; }

    // DecreaseButton onclick listener
    public void DecreaseButtonOnClick()
    {
        if (eggNumber != 1)
        {
            eggNumber--;
            UpdateSettedNumberText();
        }
    }

    // IncreaseButton onclick listener
    public void IncreaseButtonOnClick()
    {
        if (eggNumber != 7)
        {
            eggNumber++;
            UpdateSettedNumberText();
        }
    }
}