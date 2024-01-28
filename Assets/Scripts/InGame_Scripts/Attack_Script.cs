/**
 * Manages related to attack.
 * 
 * @version 0.2, Code optimization
 * @author S3
 * @date 2024/01/28
*/

using UnityEngine;

public class Attack_Script : MonoBehaviour
{
    private GameObject AttackPowerGage;
    private Camera Main_Camera;

    private Vector3 mousePosition;
    private RaycastHit2D hit;

    private float rangeCircleX, rangeCircleY, rangeChecker;
    private float attackPowerGageSize;

    private static float attackPowerGageMin = 20, attackPowerGageMax = 185;
    private static float maxSpeed = 25;

    private InGame_Script ig_Script;
    private TurnControl_Script tc_Script;

    // Specifies
    private void Awake()
    {
        Main_Camera = GameObject.Find("Main_Camera").GetComponent<Camera>();

        AttackPowerGage = GameObject.Find("AttackPowerGage").gameObject;
        AttackPowerGage.SetActive(false);
    }

    // Specifies when game start
    private void Start()
    {
        ig_Script = GetComponent<InGame_Script>();
        tc_Script = GetComponent<TurnControl_Script>();
    }

    private void Update()
    {
        if (ig_Script.playGame && !tc_Script.GetTurnEnd())
        {
            mousePosition = Input.mousePosition;

            IfMouseButtonDown();
            if (hit)
                if(hit.transform.GetComponent<Egg_Script>().GetColor() == tc_Script.GetCurrentTurn())
                {
                    AttackPowerGage.transform.position = Main_Camera.WorldToScreenPoint(hit.transform.position); // Set AttackPowerGage's position
                    IfMouseButtonClick();
                    IfMouseButtonUp();
                }
        }
        else
        {
            hit = new RaycastHit2D();
            AttackPowerGage.SetActive(false);
        }
    }

    // Set mousePoint
    private void IfMouseButtonDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePoint = Main_Camera.ScreenToWorldPoint(mousePosition);
            hit = Physics2D.Raycast(mousePoint, transform.forward, 10);
        }
    }

    // Set AttackPowerGage's active, size and rotation
    private void IfMouseButtonClick()
    {
        if (Input.GetMouseButton(0))
        {
            SetAttackPowerGageActive();
            SetAttackPowerGageSize();
            SetAttackPowerGageRotation();
        }
    }

    // Set attack power gage is active or not
    private void SetAttackPowerGageActive()
    {
        rangeCircleX = mousePosition.x - AttackPowerGage.transform.position.x;
        rangeCircleY = mousePosition.y - AttackPowerGage.transform.position.y;
        rangeChecker = Mathf.Pow(rangeCircleX, 2) + Mathf.Pow(rangeCircleY, 2);

        if (rangeChecker > Mathf.Pow(attackPowerGageMin, 2))
            AttackPowerGage.SetActive(true);
        else
            AttackPowerGage.SetActive(false);
    }

    // Set attack power gage's size
    private void SetAttackPowerGageSize()
    {
        attackPowerGageSize = Mathf.Sqrt(rangeChecker) + 5;
        if (attackPowerGageSize > attackPowerGageMax)
            attackPowerGageSize = attackPowerGageMax;
        AttackPowerGage.GetComponent<RectTransform>().sizeDelta = new Vector2(25, attackPowerGageSize);
    }

    // Set attack power gage's rotation
    private void SetAttackPowerGageRotation()
    {
        float angle = (Mathf.Atan2(rangeCircleX, rangeCircleY) * Mathf.Rad2Deg - 180);
        AttackPowerGage.transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);
        hit.transform.transform.rotation = Quaternion.AngleAxis(angle - Main_Camera.transform.eulerAngles.z, Vector3.back);
    }

    // Shoot egg
    private void IfMouseButtonUp()
    {
        if (Input.GetMouseButtonUp(0))
            if (rangeChecker > Mathf.Pow(attackPowerGageMin, 2))
            {
                float speed = (attackPowerGageSize / attackPowerGageMax) * maxSpeed;
                hit.transform.gameObject.GetComponent<Egg_Script>().SetEggMove(speed);
                hit = new RaycastHit2D();
                AttackPowerGage.SetActive(false);
                tc_Script.SetTurnEnd(true);
            }
    }
}