/**
 * Controls camera
 * 
 * Script Explanation
 * - Rotate camera using mouse
 * - Init camera
 * - Update camera rotation
 * 
 * @version 0.1, First version
 * @author S3
 * @date 2023/12/28
*/

using UnityEngine;

public class CameraControl_Script : MonoBehaviour
{
    private Camera Main_Camera;

    private InGame_Script ig_Script;
    private TurnControl_Script tc_Script;

    private Vector3 mousePosition1, mousePosition2;
    private RaycastHit2D hit;
    private static float cameraRotationSpeed = 0.05f;

    /*
     * Specifies
     */
    private void Start()
    {
        Main_Camera = GameObject.Find("Main_Camera").GetComponent<Camera>(); ;

        ig_Script = GetComponent<InGame_Script>();
        tc_Script = GetComponent<TurnControl_Script>();
    }

    private void Update()
    {
        if (ig_Script.playGame && !tc_Script.GetTurnEnd())
        {
            IfMouseButtonDown();
            IfMouseButtonClick();
        }
    }

    /*
     * Set mousePositionStart
     */
    private void IfMouseButtonDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePosition1 = Main_Camera.ScreenToWorldPoint(Input.mousePosition);
            hit = Physics2D.Raycast(mousePosition1, transform.forward, 10);
            mousePosition1 = Main_Camera.WorldToScreenPoint(mousePosition1);
        }
    }

    /*
     * Rotate camera using mouse
     */
    private void IfMouseButtonClick()
    {
        if (Input.GetMouseButton(0))
        {
            if (!hit)
            {
                mousePosition2 = Input.mousePosition;

                float z = 0;
                if (mousePosition1.x > 640 && mousePosition1.y > 360)
                    z = ((mousePosition2.x - mousePosition1.x) + (mousePosition1.y - mousePosition2.y)) * cameraRotationSpeed;
                else if (mousePosition1.x < 640 && mousePosition1.y > 360)
                    z = ((mousePosition2.x - mousePosition1.x) + (mousePosition2.y - mousePosition1.y)) * cameraRotationSpeed;
                else if (mousePosition1.x < 640 && mousePosition1.y < 360)
                    z = ((mousePosition1.x - mousePosition2.x) + (mousePosition2.y - mousePosition1.y)) * cameraRotationSpeed;
                else
                    z = ((mousePosition1.x - mousePosition2.x) + (mousePosition1.y - mousePosition2.y)) * cameraRotationSpeed;

                Main_Camera.transform.Rotate(0, 0, z);
                mousePosition1 = mousePosition2;
            }
        }
    }

    /*
     * Init camera
     * 
     * @param int z
     */
    public void InitCamera(int z)
    {
        Main_Camera.transform.rotation = Quaternion.Euler(0, 0, z);
    }

    /*
     * Rotate camera to targetAngle
     * 
     * @param float 180 or 360
     */
    public void UpdateCameraRotation(float targetAngle)
    {
        if (targetAngle == 180)
        {
            float plusMinus = Mathf.Sign(180 - Main_Camera.transform.eulerAngles.z);
            Main_Camera.transform.Rotate(0, 0, plusMinus * 35 * Time.deltaTime);

            if (plusMinus > 0)
            {
                if(Main_Camera.transform.eulerAngles.z >= targetAngle)
                {
                    Main_Camera.transform.rotation = Quaternion.Euler(0, 0, 180);
                    tc_Script.SetTurn(true);
                }
            }
            else
            {
                if (Main_Camera.transform.eulerAngles.z <= targetAngle)
                {
                    Main_Camera.transform.rotation = Quaternion.Euler(0, 0, 180);
                    tc_Script.SetTurn(true);
                }
            }
        }
        else
        {
            float plusMinus = Mathf.Sign(Main_Camera.transform.eulerAngles.z - 180);
            Main_Camera.transform.Rotate(0, 0, plusMinus * 35 * Time.deltaTime);

            if (plusMinus > 0)
            {
                if (Main_Camera.transform.eulerAngles.z < 180 && Main_Camera.transform.eulerAngles.z >= 0)
                {
                    Main_Camera.transform.rotation = Quaternion.Euler(0, 0, 0);
                    tc_Script.SetTurn(false);
                }
            }
            else
            {
                if (Main_Camera.transform.eulerAngles.z > 180)
                {
                    Main_Camera.transform.rotation = Quaternion.Euler(0, 0, 0);
                    tc_Script.SetTurn(false);
                }
            }
        }
    }
}
