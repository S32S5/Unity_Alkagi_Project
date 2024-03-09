/**
 * Control player waiting panel
 * 
 * @version 1.0.0
 * - Change class name PlayerWaitingController
 * - Code optimization
 * @author S3
 * @date 2024/03/08
*/

using System.Collections;
using System.Net.Sockets;
using System.Net;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode.Transports.UTP;

public class PlayerWaitingController : MonoBehaviour, Panel
{
    private string ipStr;

    Text statusTxt;
    GameObject cancelOkBtn;
    Text cancelOkBtnTxt;

    HostJoinController hostJoin;
    IsRightPlayerAnswerController isRight;

    GameObject netManager;
    NetworkManager net;

    private void Awake()
    {
        statusTxt = GameObject.Find("PlayerConnectStatusText").GetComponent<Text>();
        cancelOkBtn = GameObject.Find("PlayerWaitingCancelOkButton");
        cancelOkBtnTxt = GameObject.Find("PlayerWaitingCancelOkButtonText").GetComponent<Text>();

        isRight = GameObject.Find("IsRightPlayerAnswerPanel").GetComponent<IsRightPlayerAnswerController>();

        hostJoin = GameObject.Find("HostJoinPanel").GetComponent<HostJoinController>();

        netManager = GameObject.Find("NetworkManager");
        net = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
    }

    private void Update() { EscListener(); }

    public void Init()
    {
        isRight.SetPanel(false);
        CallMyIp();
        InitWaitingObj();
        ConnectNet();
        StartCoroutine(WaitingForJoin());
    }

    public void SetPanel(bool OnOff) 
    { 
        gameObject.SetActive(OnOff);

        if (OnOff)
            Init();
        else
            net.Shutdown();
    }

    public bool GetPanelIsOn() { return gameObject.activeSelf; }

    public void EscListener()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isRight.GetPanelIsOn())
                ShutDownHost();
        }
    }

    // Call my ip
    private void CallMyIp()
    {
        ipStr = "";
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
                ipStr = ip.ToString();
        }
    }

    // Init statusTxt and cancelOkBtn
    private void InitWaitingObj()
    {
        string message = "�뱹 ��븦 ��ٸ��� �ֽ��ϴ�..." + "\n<size=40>�� ������ �ּ�: " + ipStr + "</size>";
        statusTxt.text = message;
        cancelOkBtn.SetActive(true);
        cancelOkBtnTxt.text = "���";
    }

    private void ConnectNet()
    {
        netManager.SetActive(true);
        netManager.GetComponent<UnityTransport>().ConnectionData.ServerListenAddress = ipStr;
        net.StartHost();
    }

    // Procedure of connecting to join
    private IEnumerator WaitingForJoin()
    {
        while (net.IsHost && net.ConnectedClients.Count != 2)
            yield return null;

        if (!net.IsHost)
            yield break;

        NetworkManager.Singleton.CustomMessagingManager.RegisterNamedMessageHandler("joinIp", (senderClientId, reader) =>
        {
            reader.ReadValueSafe(out string ip);
            statusTxt.text = ip + "�뱹 ����� ������ �ּҰ� �½��ϱ�?";
            isRight.SetPanel(true);
        });
        cancelOkBtn.SetActive(false);
    }

    // Shutdown host
    public void ShutDownHost()
    {
        net.Shutdown();
        netManager.SetActive(false);

        gameObject.SetActive(false);
        hostJoin.SetPanel(true);
    }

    // Update status text
    //
    // @param string
    public void UpdateStatusText(string message) { statusTxt.text = message; }

    // Set cancelOkButton on off
    //
    // @param bool, string
    public void SetCancelOkButton(bool OnOff, string text)
    {
        cancelOkBtn.SetActive(OnOff);
        cancelOkBtnTxt.text = text;
    }
}