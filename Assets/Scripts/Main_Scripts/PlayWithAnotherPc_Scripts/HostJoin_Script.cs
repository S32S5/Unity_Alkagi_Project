/**
 * Manages host join
 * 
 * @version 0.0.4, New script
 * @author S3
 * @date 2024/02/23
*/

using UnityEngine;

public class HostJoin_Script : MonoBehaviour
{
    private PlayWithAnotherPc_Script another;
    private Main_Script main;
    private PlayerWaiting_Script player;
    private JoinWaiting_Script join;

    // Specifies
    private void Awake()
    {
        another = GameObject.Find("PlayWithAnotherPc_Panel").GetComponent<PlayWithAnotherPc_Script>();
        main = GameObject.Find("Main_Panel").GetComponent<Main_Script>();
        player = GameObject.Find("PlayerWaiting_Panel").GetComponent<PlayerWaiting_Script>();
        join = GameObject.Find("JoinWaiting_Panel").GetComponent<JoinWaiting_Script>();
    }

    // Esc
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            another.SetPanel(false);
            main.SetPanel();
        }
    }

    // HostJoin_Panel set active
    public void SetPanel() { gameObject.SetActive(true); }

    // Host button onclick listener
    public void HostButtonOnClick()
    {
        gameObject.SetActive(false);
        player.SetPanel(true);
    }

    // Join button onclick listener
    public void JoinButtonOnClick()
    {
        gameObject.SetActive(false);
        join.SetPanel(true);
    }
}