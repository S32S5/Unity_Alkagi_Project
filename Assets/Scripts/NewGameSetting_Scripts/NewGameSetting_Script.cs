/**
 * New Game Setting.
 * 
 * Script Explanation
 * - Add related to NewGameSetting class
 * - Init NewGameSetting
 * - Set enable or disable NewGameSetting_Panel
 * 
 * @version 0.1, First version
 * @author S3
 * @date 2023/12/24
*/

using UnityEngine;

public class NewGameSetting_Script : MonoBehaviour
{
    private GameObject NewGameSetting_Panel;

    /*
     * Specifies
     */
    private void Awake()
    {
        GameObject NewGame_Canvas = GameObject.Find("NewGame_Canvas");
        NewGameSetting_Panel = NewGame_Canvas.transform.Find("NewGameSetting_Panel").gameObject;

        gameObject.AddComponent<InitialEggNumberControl_Script>();
        gameObject.AddComponent<FirstTurnControl_Script>();
        gameObject.AddComponent<TurnTimeLimitControl_Script>();
        gameObject.AddComponent<GameStartControl_Script>();
        gameObject.AddComponent<QuitGameControl_Script>();
    }

    /*
     * Init when OnEnable
     */
    private void OnEnable()
    {
        NewGameSetting_Panel.SetActive(true);
    }

    /*
     * Enable or disable NewGameSetting_Panel
     * 
     * @param true or false
     */
    public void SetNewGameSetting_Panel(bool onOff)
    {
        NewGameSetting_Panel.SetActive(onOff);
    }
}