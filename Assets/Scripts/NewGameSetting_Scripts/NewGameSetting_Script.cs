/**
 * New Game Setting.
 * 
 * @version 0.0.2
 *  - Code optimization
 * @author S3
 * @date 2024/01/28
*/

using UnityEngine;

public class NewGameSetting_Script : MonoBehaviour
{
    private GameStartControl_Script gsc_Script;
    private QuitGameControl_Script qgc_Script;

    // Specifies
    private void Awake()
    {
        gameObject.AddComponent<InitialEggNumberControl_Script>();
        gameObject.AddComponent<FirstTurnControl_Script>();
        gameObject.AddComponent<TurnTimeLimitControl_Script>();
        gameObject.AddComponent<GameStartControl_Script>();
        gameObject.AddComponent<QuitGameControl_Script>();

        gsc_Script = GetComponent<GameStartControl_Script>();
        qgc_Script = GetComponent<QuitGameControl_Script>();
    }

    // Esc
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gsc_Script.GameSettingError_Panel_EnDis())
                gsc_Script.NewGameSettingErrorMessageCheck();
            else if (qgc_Script.QuitGame_Panel_EnDis())
                qgc_Script.QuitGameCancel();
            else
                qgc_Script.ShowQuitGame_Panel();
        }
    }
}