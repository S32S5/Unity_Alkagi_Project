/**
 * Manages host ip input
 * 
 * @version 1.0.0
 * - Change class name HostIpInput_Script to HostIpInputController
 * @author S3
 * @date 2024/03/08
*/

using System;
using UnityEngine;
using UnityEngine.UI;

public class HostIpInputController : MonoBehaviour, Panel
{
    InputField input;
    GameObject wrongIp;

    HostJoinController hostJoin;
    JoinConnectingStatusController status;

    private void Awake()
    {
        input = GameObject.Find("HostIpInputField").GetComponent<InputField>();
        wrongIp = GameObject.Find("WrongIpFormPanel");

        hostJoin = GameObject.Find("HostJoinPanel").GetComponent<HostJoinController>();
        status = GameObject.Find("JoinConnectingStatusPanel").GetComponent<JoinConnectingStatusController>();
    }

    private void Update() { EscListener(); }

    public void Init() { wrongIp.SetActive(false); }

    public void SetPanel(bool OnOff) { gameObject.SetActive(OnOff); }
    
    public bool GetPanelIsOn() { return gameObject.activeSelf; }

    public void EscListener()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!wrongIp.activeSelf)
                hostJoin.SetPanel(true);
            else
                wrongIp.SetActive(false);
        }
    }

    public void OkButtonOnClick()
    {
        try
        {
            string[] ipSplit = input.text.Split('.');
            for(int i = 0; i < 4; i++)
            {
                if (int.Parse(ipSplit[i]) > 255)
                    throw new Exception();
            }

            gameObject.SetActive(false);
            status.SetHostIp(input.text);
            status.SetPanel(true);
        }
        catch
        {
            wrongIp.SetActive(true);
        }
    }

    public void CheckWrongIpButtonOnClcik() { wrongIp.SetActive(false); }
}