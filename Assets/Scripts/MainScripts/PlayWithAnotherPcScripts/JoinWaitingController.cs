/**
 * Manages join waiting
 * 
 * @version 1.0.0
 * - Change class name JoinWaiting_Script JoinWaitingController
 * - Code optimization
 * @author S3
 * @date 2024/03/08
*/

using UnityEngine;

public class JoinWaitingController : MonoBehaviour, Panel
{
    HostIpInputController hostIp;
    JoinConnectingStatusController status;

    private void Awake()
    {
        hostIp = GameObject.Find("HostIpInputPanel").GetComponent<HostIpInputController>();
        status = GameObject.Find("JoinConnectingStatusPanel").GetComponent<JoinConnectingStatusController>();
    }

    public void Init() 
    {
        hostIp.SetPanel(true);
        hostIp.GetComponent<HostIpInputController>().Init();
        status.SetPanel(false);
    }

    // JoinWaiting_Panel set on off
    //
    // @param bool
    public void SetPanel(bool OnOff) { 
        gameObject.SetActive(OnOff);

        if (OnOff)
            Init();
    }

    public bool GetPanelIsOn() { return gameObject.activeSelf; }

    public void EscListener() { }
}