/**
 * Manages play with another pc
 * 
 * @version 0.0.4, New script
 * @author S3
 * @date 2024/02/23
*/

using UnityEngine;

public class PlayWithAnotherPc_Script : MonoBehaviour
{
    // PlayerWithAnotherPc set false when game start
    private void Start() { gameObject.SetActive(false); }

    // Set PlayWithAnotherPc_Panel is active
    //
    // @param bool
    public void SetPanel(bool OnOff) { gameObject.SetActive(OnOff); }
}