/**
 * Manage related to game start
 * 
 * Script Explanation
 * - Game start action
 * 
 * @version 0.0.3
 * - Delete new game setting system
 * - Code optimization
 * @author S3
 * @date 2024/02/08
*/

using UnityEngine;
using UnityEngine.UI;

public class GameStartControl_Script : MonoBehaviour
{
    private Button GameStart_Button;

    // Specifies
    private void Awake()
    {
        GameStart_Button = GameObject.Find("GameStart_Button").GetComponent<Button>();
    }

    // Specifies when game start
    private void Start()
    {
        GameStart_Button.onClick.AddListener(() => {
            GameObject.Find("Scene_Director").GetComponent<Scene_Director_Script>().InGame();
        });
    }
}