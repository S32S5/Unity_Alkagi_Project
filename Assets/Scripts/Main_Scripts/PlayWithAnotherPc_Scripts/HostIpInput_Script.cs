/**
 * Manages host ip input
 * 
 * @version 0.0.4, New script
 * @author S3
 * @date 2024/02/24
*/

using System;
using UnityEngine;
using UnityEngine.UI;

public class HostIpInput_Script : MonoBehaviour
{
    private InputField ipInput;
    private GameObject wrongIp;

    private JoinWaiting_Script joinWaiting;
    private HostJoin_Script hostJoin;
    private JoinConnectingStatus_Script status;

    private GameObject netManager;

    // Specifies
    private void Awake()
    {
        ipInput = GameObject.Find("HostIp_InputField").GetComponent<InputField>();
        wrongIp = GameObject.Find("WrongIpForm_Panel");

        hostJoin = GameObject.Find("HostJoin_Panel").GetComponent<HostJoin_Script>();
        joinWaiting = GameObject.Find("JoinWaiting_Panel").GetComponent<JoinWaiting_Script>();
        status = GameObject.Find("JoinConnectingStatus_Panel").GetComponent<JoinConnectingStatus_Script>();
        netManager = GameObject.Find("NetworkManager");
    }

    // WrongIp set active false when game start
    private void Start() { wrongIp.SetActive(false); }

    // Esc
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!wrongIp.activeSelf)
            {
                joinWaiting.SetPanel(false);
                hostJoin.SetPanel();
            }
            if(wrongIp.activeSelf)
                wrongIp.SetActive(false);
        }
    }

    // Set HostIpInput_Panel
    //
    // @param bool OnOff
    public void SetPanel() { gameObject.SetActive(true); }

    // Ok button onclick listener
    public void OkButtonOnClick()
    {
        try
        {
            string[] ipSplit = ipInput.text.Split('.');
            for(int i = 0; i < 4; i++)
            {
                if (int.Parse(ipSplit[i]) > 255)
                    throw new Exception();
            }

            gameObject.SetActive(false);
            netManager.SetActive(true);
            status.SetPanel(true);
            status.StartCoroutine(status.ConnectingToHost(ipInput.text));
        }
        catch
        {
            wrongIp.SetActive(true);
        }
    }

    // Check wrong ip button onclikc listener
    public void CheckWrongIpButtonOnClcik() { wrongIp.SetActive(false); }
}