/**
 * Controls camera
 * 
 * @version 0.4
 * - Code optimization
 * @author S3
 * @date 2024/02/18
*/

using UnityEngine;

public class CameraControl_Script : MonoBehaviour
{
    private Camera cam;

    private InGame_Script inGame;
    private TurnControl_Script turn;

    private Vector3 mouse1, mouse2;
    private RaycastHit2D hit;
    private static float camRotationSpeed = 0.05f;

    // Specifies
    private void Awake()
    {
        cam = GameObject.Find("Main_Camera").GetComponent<Camera>();
    }

    // Specifies when game start
    private void Start()
    {
        inGame = GetComponent<InGame_Script>();
        turn = GetComponent<TurnControl_Script>();
    }

    private void Update()
    {
        if (inGame.playGame && !turn.GetTurnEnd())
        {
            IfMouseButtonDown();
            IfMouseButtonClick();
        }
    }

    // Set mousePositionStart
    private void IfMouseButtonDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouse1 = cam.ScreenToWorldPoint(Input.mousePosition);
            hit = Physics2D.Raycast(mouse1, transform.forward, 10);
            mouse1 = cam.WorldToScreenPoint(mouse1);
        }
    }

    // Rotate camera using mouse
    private void IfMouseButtonClick()
    {
        if (Input.GetMouseButton(0))
        {
            if (!hit)
            {
                mouse2 = Input.mousePosition;

                float z = 0;
                if (mouse1.x > 640 && mouse1.y > 360)
                    z = ((mouse2.x - mouse1.x) + (mouse1.y - mouse2.y)) * camRotationSpeed;
                else if (mouse1.x < 640 && mouse1.y > 360)
                    z = ((mouse2.x - mouse1.x) + (mouse2.y - mouse1.y)) * camRotationSpeed;
                else if (mouse1.x < 640 && mouse1.y < 360)
                    z = ((mouse1.x - mouse2.x) + (mouse2.y - mouse1.y)) * camRotationSpeed;
                else
                    z = ((mouse1.x - mouse2.x) + (mouse1.y - mouse2.y)) * camRotationSpeed;

                cam.transform.Rotate(0, 0, z);
                mouse1 = mouse2;
            }
        }
    }

    // Set camera
    // 
    // @param bool
    public void SetCam(bool turn) { 
        if(!turn)
            cam.transform.rotation = Quaternion.Euler(0, 0, 0);
        else
            cam.transform.rotation = Quaternion.Euler(0, 0, 180);
    }
}
