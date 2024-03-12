/**
 * Control power gage
 * 
 * @version 1.1.0
 * - Blocks white hits when in ai mode
 * @author S3
 * @date 2024/03/09
*/

using Unity.Netcode;
using UnityEngine;

public class AttackController : NetworkBehaviour
{
    GameObject powerGage;
    Camera cam;

    private Vector3 mousePosition;
    private RaycastHit2D hit;

    private float rangeCircleX, rangeCircleY, rangeChecker;
    private float powerGageSize;

    private static float powerGageMin = 20, powerGageMax = 185;

    InGameCanvasController inGame;
    EggController egg;
    TurnController turn;

    NetworkManager manager;

    private void Awake()
    {
        powerGage = GameObject.Find("AttackPowerGage");
        cam = GameObject.Find("MainCamera").GetComponent<Camera>();

        inGame = GameObject.Find("InGameCanvas").GetComponent<InGameCanvasController>();
        egg = GetComponent<EggController>();
        turn = GetComponent<TurnController>();

        manager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
    }

    private void Start() { powerGage.SetActive(false); }

    // If you click and drag egg, setting power gage
    private void Update() { SetPowerGage(); }

    private void SetPowerGage()
    {
        if (inGame.GetPlayGame() && !turn.GetTurnEnd())
        {
            mousePosition = Input.mousePosition;

            MouseButtonDown();
            if (hit)
            {
                if (hit.transform.GetComponent<Egg>().GetColor() == turn.GetTurn())
                {
                    if (inGame.GetGameMode() == 0)
                        if (hit.transform.GetComponent<Egg>().GetColor() == true)
                            return;
                    if (inGame.GetGameMode() == 2)
                        if (manager.IsHost && turn.GetTurn() == true || !manager.IsHost && turn.GetTurn() == false)
                            return;

                    MouseButtonClick();
                    IfMouseButtonUp();
                }
            }
        }
        else
        {
            hit = new RaycastHit2D();
            powerGage.SetActive(false);
        }
    }

    // Set mousePoint
    private void MouseButtonDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePoint = cam.ScreenToWorldPoint(mousePosition);
            hit = Physics2D.Raycast(mousePoint, transform.forward, 10);
            if (hit && hit.transform.GetComponent<Egg>().GetColor() == turn.GetTurn())
                powerGage.transform.position = cam.WorldToScreenPoint(hit.transform.position);
        }
    }

    // Set attack power gage's rotation, size and active
    private void MouseButtonClick()
    {
        if (Input.GetMouseButton(0))
        {
            SetGageRotation();
            SetGageSize();
            SetGageActive();
        }
    }

    // Set attack power gage's rotation
    private void SetGageRotation()
    {
        float angle = (Mathf.Atan2(rangeCircleX, rangeCircleY) * Mathf.Rad2Deg - 180);
        powerGage.transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);
        hit.transform.transform.rotation = Quaternion.AngleAxis(angle - cam.transform.eulerAngles.z, Vector3.back);
    }

    // Set attack power gage's size
    private void SetGageSize()
    {
        rangeCircleX = mousePosition.x - powerGage.transform.position.x;
        rangeCircleY = mousePosition.y - powerGage.transform.position.y;
        rangeChecker = Mathf.Pow(rangeCircleX, 2) + Mathf.Pow(rangeCircleY, 2);

        powerGageSize = Mathf.Sqrt(rangeChecker) + 5;
        if (powerGageSize > powerGageMax)
            powerGageSize = powerGageMax;
        powerGage.GetComponent<RectTransform>().sizeDelta = new Vector2(25, powerGageSize);
    }

    // Set attack power gage is active or not
    private void SetGageActive()
    {
        if (rangeChecker > Mathf.Pow(powerGageMin, 2))
            powerGage.SetActive(true);
        else
            powerGage.SetActive(false);
    }

    // Shoot egg
    private void IfMouseButtonUp()
    {
        if (Input.GetMouseButtonUp(0))
            if (rangeChecker > Mathf.Pow(powerGageMin, 2))
            {
                powerGage.SetActive(false);
                float powerGagePercentage = powerGageSize / powerGageMax;
                hit.transform.gameObject.GetComponent<Egg>().SetEggVelocity(powerGagePercentage);

                if (inGame.GetGameMode() == 2)
                {
                    string velStr = "";
                    int attackIndex;
                    string velx, vely;

                    void sendVel(int color)
                    {
                        attackIndex = egg.eggs[color].IndexOf(hit.transform.gameObject);
                        velx = egg.eggs[color][attackIndex].GetComponent<Rigidbody2D>().velocity.x.ToString();
                        vely = egg.eggs[color][attackIndex].GetComponent<Rigidbody2D>().velocity.y.ToString();
                        velStr += attackIndex + " " + velx + " " + vely;

                        using FastBufferWriter writer = new FastBufferWriter(256, Unity.Collections.Allocator.Temp);
                        writer.WriteValueSafe(velStr);
                        if(color == 0)
                            NetworkManager.Singleton.CustomMessagingManager.SendNamedMessage("eggVel", manager.ConnectedClientsIds[1], writer, NetworkDelivery.Reliable);
                        else
                            NetworkManager.Singleton.CustomMessagingManager.SendNamedMessage("eggVel", OwnerClientId, writer, NetworkDelivery.Reliable);
                    }
                    if (!turn.GetTurn())
                        sendVel(0);
                    else
                        sendVel(1);
                }

                hit = new RaycastHit2D();
                turn.SetTurnEnd(true);
            }
    }
}