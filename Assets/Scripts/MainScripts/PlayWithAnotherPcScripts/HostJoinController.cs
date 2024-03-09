/**
 * Control host join panel
 * 
 * @version 1.0.0
 * - Change class name HostJoin_Script to HostJoinController
 * @author S3
 * @date 2024/03/08
*/

using UnityEngine;

public class HostJoinController : MonoBehaviour, Panel
{
    MainPanelController main;
    PlayWithAnotherPcController anotherPc;
    PlayerWaitingController player;
    JoinWaitingController join;

    private void Awake()
    {
        main = GameObject.Find("MainPanel").GetComponent<MainPanelController>();
        anotherPc = GameObject.Find("PlayWithAnotherPcPanel").GetComponent<PlayWithAnotherPcController>();
        player = GameObject.Find("PlayerWaitingPanel").GetComponent<PlayerWaitingController>();
        join = GameObject.Find("JoinWaitingPanel").GetComponent<JoinWaitingController>();
    }

    private void Update() { EscListener(); }

    public void Init() 
    {
        player.SetPanel(false);
        join.SetPanel(false);
    }

    public void SetPanel(bool OnOff) 
    { 
        gameObject.SetActive(OnOff);

        if (OnOff)
            Init();
    }

    public bool GetPanelIsOn() { return gameObject.activeSelf; }

    public void EscListener()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            anotherPc.SetPanel(false);
            main.SetPanel(true);
        }
    }

    public void HostButtonOnClick()
    {
        gameObject.SetActive(false);
        player.SetPanel(true);
    }

    public void JoinButtonOnClick()
    {
        gameObject.SetActive(false);
        join.SetPanel(true);
    }
}