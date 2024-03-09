/**
 * Control play with another pc panel
 * 
 * @version 1.0.0
 * - Change class name PlayWithAnotherPc_Script to PlayWithAnotherPcController
 * @author S3
 * @date 2024/03/08
*/

using UnityEngine;

public class PlayWithAnotherPcController : MonoBehaviour, Panel
{
    HostJoinController hostJoin;
    PlayerWaitingController player;
    JoinWaitingController join;

    private void Awake()
    {
        hostJoin = GameObject.Find("HostJoinPanel").GetComponent<HostJoinController>();
        player = GameObject.Find("PlayerWaitingPanel").GetComponent<PlayerWaitingController>();
        join = GameObject.Find("JoinWaitingPanel").GetComponent<JoinWaitingController>();
    }

    public void Init()
    {
        hostJoin.SetPanel(true);
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

    public void EscListener() { }
}