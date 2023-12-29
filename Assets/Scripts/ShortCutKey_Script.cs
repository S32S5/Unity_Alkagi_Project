/**
 * Manages game shortcuts.
 * 
 * @version 0.1, First version
 * @author S3
 * @date 2023/12/28
*/

using UnityEngine;

public class ShortCutKey_Script : MonoBehaviour
{
    private NewGameSetting_Script ngs_Script;
    private InGame_Script ig_Script;

    /*
     * Specifies
     */
    private void Start()
    {
        ngs_Script = gameObject.GetComponent<NewGameSetting_Script>();
        ig_Script = gameObject.GetComponent<InGame_Script>();
    }

    /*
     * If pressed key, operate action
     */
    private void Update()
    {
        // Esc key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // If in NewGameSetting_Script
            if(ngs_Script.enabled)
            {
                GameStartControl_Script gsc_Script = GetComponent<GameStartControl_Script>();
                QuitGameControl_Script qgc_Script = GetComponent<QuitGameControl_Script>();

                if (gsc_Script.GameSettingError_Panel_EnDis())
                    gsc_Script.NewGameSettingErrorMessageCheck();
                else if (qgc_Script.QuitGame_Panel_EnDis())
                    qgc_Script.QuitGameCancel();
                else
                    qgc_Script.ShowQuitGame_Panel();
            }
            // If in InGame_Script
            else
            {
                if (ig_Script.ecgc_Script.EndCurrentGame_Panel_EnDis())
                    ig_Script.ecgc_Script.EndCurrentGameCancel();
                else if(ig_Script.grc_Script.GetGameResult_Panel_EnDis())
                    ig_Script.grc_Script.CheckGameResult();
                else
                    ig_Script.ecgc_Script.ShowEndCurrentGame_Panel();
            }
        }
    }
}