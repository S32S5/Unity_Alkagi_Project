/**
 * New Game Setting.
 * 
 * @version 0.0.3
 *  - Delete QuitGameControl_Script
 *  - Add Esc
 *  - Code optimization
 * @author S3
 * @date 2024/02/07
*/

using UnityEngine;

public class NewGameSetting_Script : MonoBehaviour
{
    private Scene_Director_Script sd_Script;

    // Specifies
    private void Awake()
    {
        gameObject.AddComponent<InitialSettingVariable_Script>();
        gameObject.AddComponent<InitialEggNumberControl_Script>();
        gameObject.AddComponent<FirstTurnControl_Script>();
        gameObject.AddComponent<TurnTimeLimitControl_Script>();
        gameObject.AddComponent<GameStartControl_Script>();

        sd_Script = GameObject.Find("Scene_Director").GetComponent<Scene_Director_Script>();
    }

    // Specifies when game start
    private void Start()
    {
        gameObject.SetActive(false);
    }

    /*
     * Get new game setting panel is on
     * 
     * @return bool new game setting panel is active
     */
    public bool GetNewGameSetting_Panel_IsOn()
    {
        return gameObject.activeSelf;
    }

    /*
     * Set new game setting panel
     * 
     * @param bool on off
     */
    public void SetNewGameSetting_Panel(bool OnOff)
    {
        gameObject.SetActive(OnOff);
    }
}