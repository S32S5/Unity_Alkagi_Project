/**
 * Manages join waiting
 * 
 * @version 0.0.4, New script
 * @author S3
 * @date 2024/02/26
*/

using UnityEngine;

public class JoinWaiting_Script : MonoBehaviour
{
    private HostIpInput_Script hostIp;
    private JoinConnectingStatus_Script status;

    // Specifies
    private void Awake()
    {
        hostIp = GameObject.Find("HostIpInput_Panel").GetComponent<HostIpInput_Script>();
        status = GameObject.Find("JoinConnectingStatus_Panel").GetComponent<JoinConnectingStatus_Script>();
    }

    // Panel set active false when game start
    private void Start() { gameObject.SetActive(false); }

    // JoinWaiting_Panel set on off
    //
    // @param bool
    public void SetPanel(bool OnOff) { 
        gameObject.SetActive(OnOff);

        if(OnOff)
        {
            hostIp.SetPanel();
            status.SetPanel(false);
        }
    }
}