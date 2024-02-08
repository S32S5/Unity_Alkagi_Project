/**
 * Manages progress of the main
 * 
 * @version 0.0.3, New script
 * @author S3
 * @date 2024/02/06
*/

using UnityEngine;
using UnityEngine.UI;

public class Main_Script : MonoBehaviour
{
    private GameObject Main_Panel;
    private Button PlayOnThisPC_Button, PlayWithAnotherPC_Button;

    private GameObject PlayWithAnotherPc_Panel;

    private Preferences_Script p_Script;
    private NewGameSetting_Script ngs_Script;
    private QuitGameControl_Script qgc_Script;

    // Specifies
    private void Awake()
    {
        p_Script = GameObject.Find("PreferencesBackground_Panel").GetComponent<Preferences_Script>();
        ngs_Script = GameObject.Find("NewGameSetting_Panel").GetComponent<NewGameSetting_Script>();
        qgc_Script = GameObject.Find("QuitGameBackground_Panel").GetComponent<QuitGameControl_Script>();

        Main_Panel = GameObject.Find("Main_Panel");
        PlayWithAnotherPc_Panel = GameObject.Find("PlayWithAnotherPc_Panel");

        PlayOnThisPC_Button = GameObject.Find("PlayOnThisPC_Button").GetComponent<Button>();
        PlayWithAnotherPC_Button = GameObject.Find("PlayWithAnotherPC_Button").GetComponent<Button>();

        PlayWithAnotherPc_Panel.SetActive(false);
    }

    // Specifies when game start
    private void Start()
    {
        PlayOnThisPC_Button.onClick.AddListener(() => {
            Main_Panel.SetActive(false);
            ngs_Script.SetNewGameSetting_Panel(true);
        });
        PlayWithAnotherPC_Button.onClick.AddListener(() =>
        {
            Main_Panel.SetActive(false);
        });
    }

    // Esc
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (p_Script.GetPreferencesBackground_Panel_IsOn())
                p_Script.SetPreferencesBackground_Panel(false);
            else if (ngs_Script.GetNewGameSetting_Panel_IsOn())
            {
                ngs_Script.SetNewGameSetting_Panel(false);
                Main_Panel.SetActive(true);
            }
            else if (qgc_Script.GetQuitGame_Panel_IsOn())
                qgc_Script.SetQuitGame_Panel(false);
            else
                qgc_Script.SetQuitGame_Panel(true);
        }
    }
}