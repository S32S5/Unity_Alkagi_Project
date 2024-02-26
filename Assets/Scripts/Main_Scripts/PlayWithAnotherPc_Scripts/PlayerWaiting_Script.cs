/**
 * Manages player waiting
 * 
 * @version 0.0.4, New script
 * @author S3
 * @date 2024/02/24
*/

using System.Collections;
using System.Net.Sockets;
using System.Net;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWaiting_Script : MonoBehaviour
{
    private Text statusText;
    private GameObject cancelOkButton;
    private Text cancelOkButtonText;

    private HostJoin_Script hostJoin;
    private IsRightPlayerAnswer_Script isRight;

    private GameObject netManager;
    private NetworkManager net;

    // Specifies
    private void Awake()
    {
        statusText = GameObject.Find("PlayerConnectStatus_Text").GetComponent<Text>();
        cancelOkButton = GameObject.Find("PlayerWaitingCancelOk_Button");
        cancelOkButtonText = GameObject.Find("PlayerWaitingCancelOk_Button_Text").GetComponent<Text>();

        hostJoin = GameObject.Find("HostJoin_Panel").GetComponent<HostJoin_Script>();
        isRight = GameObject.Find("IsRightPlayerAnswer_Panel").GetComponent<IsRightPlayerAnswer_Script>();

        netManager = GameObject.Find("NetworkManager");
        net = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
    }
    
    // Panel set active false when game start
    private void Start() { gameObject.SetActive(false); }

    // Esc
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(!isRight.GetPanelIsOn())
                ShutDownHost();
        }
    }

    // Set PlayerWaiting_Panel
    //
    // @param bool
    public void SetPanel(bool OnOff) 
    {
        gameObject.SetActive(OnOff);

        if (OnOff)
        {
            statusText.text = "";
            cancelOkButton.SetActive(true);
            cancelOkButtonText.text = "취소";
            netManager.SetActive(true);
            net.StartHost();
            StartCoroutine(ConnectingToJoin());
        }
    }

    // Update status text
    //
    // @param string
    public void UpdateStatusText(string message) { statusText.text = message; }

    // Set cancelOkButton on off
    //
    // @param bool, string
    public void SetCancelOkButton(bool OnOff, string text)
    {
        cancelOkButton.SetActive(OnOff);
        cancelOkButtonText.text = text;
    }

    // Shutdown host
    public void ShutDownHost()
    {
        net.Shutdown();
        netManager.SetActive(false);
        gameObject.SetActive(false);
        hostJoin.SetPanel();
    }

    // Procedure of connecting to join
    public IEnumerator ConnectingToJoin()
    {
        string message = "";
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
                message = "대국 상대를 기다리고 있습니다..." + "\n<size=40>내 아이피 주소: " + ip.ToString() + "</size>";
        }
        statusText.text = message;

        while (net.IsHost && net.ConnectedClients.Count != 2)
            yield return null;

        if (!net.IsHost)
            yield break;

        cancelOkButton.SetActive(false);
        NetworkManager.Singleton.CustomMessagingManager.RegisterNamedMessageHandler("joinIp", (senderClientId, reader) =>
        {
            reader.ReadValueSafe(out string ip);
            statusText.text = ip + "대국 상대의 아이피 주소가 맞습니까?";
            isRight.SetPanel();
        });
    }
}