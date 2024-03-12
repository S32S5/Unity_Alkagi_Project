/**
 * Control eggNum and buttons
 * 
 * @version 1.0.0
 * - Change class name InitialEggNumber_Scirpt to InitialEggNumberController
 * - Code optimization
 * @author S3
 * @date 2024/03/06
*/

using UnityEngine;
using UnityEngine.UI;

public class InitialEggNumberController : MonoBehaviour
{
    private int eggNum;
    
    Text settedText;

    GameSettingDataController data;

    private void Awake() 
    { 
        settedText = GameObject.Find("InitialEggSettedNumberText").GetComponent<Text>();

        data = GameObject.Find("SceneDirector").GetComponent<GameSettingDataController>();
    }

    // Init when game start
    private void Start() { Init(); }

    private void Init()
    {
        eggNum = data.GetEggNum();
        UpdateSettedNumberText();
    }

    // Update text of settedNumberText
    private void UpdateSettedNumberText()
    {
        settedText.text = eggNum.ToString();
        data.SetEggNum(eggNum);
    }

    public void DecreaseButtonOnClick()
    {
        if (eggNum != 1)
        {
            eggNum--;
            UpdateSettedNumberText();
        }
    }

    public void IncreaseButtonOnClick()
    {
        if (eggNum != 7)
        {
            eggNum++;
            UpdateSettedNumberText();
        }
    }
}