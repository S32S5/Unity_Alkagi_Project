/**
 * Manages progress of the game
 * 
 * @version 0.1, First version
 * @author S3
 * @date 2023/12/22
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Director_Script : MonoBehaviour
{
    // Scripts
    private NewGameSetting_Script ngss;
    private InGame_Script igs;

    /*
     * Specifies
     */
    private void Awake()
    {
        gameObject.AddComponent<ShortCutKey_Script>();

        ngss = gameObject.GetComponent<NewGameSetting_Script>();
        igs = gameObject.GetComponent<InGame_Script>();
    }

    /*
     * Enable NewGameSetting and disable InGame when game start
     */
    private void Start()
    {
        enableNewGameSetting();
    }


    /*
     * Close InGame and proceed to NewGameSetting
     */
    public void enableNewGameSetting()
    {
        igs.enabled = false;
        ngss.enabled = true;
    }


    /*
     * Close NewGameSetting and proceed to InGame
     */
    public void enableInGame()
    {
        ngss.enabled = false;
        igs.enabled = true;
    }
}