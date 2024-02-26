/*
 * Controls egg
 * 
 * Script Explanation
 * - Check egg is moving
 * - Egg collision event
 * - Set egg's color
 * - Return egg's color
 * - Set egg's velocity
 * 
 * @version 0.0.3
 * - Update physics movement
 * - Code optimization
 * @author S3
 * @date 2024/02/08
 */

using System.Collections;
using UnityEngine;

public class Egg_Script : MonoBehaviour
{
    private bool colorBool;

    private Rigidbody2D rb;
    private Vector2 lastVelocity;
    private AudioSource Collision_SE;

    private static float maxPower;
    private static float lickAngle = 115, panOut = 4.5f;

    // Specifies
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Collision_SE = GetComponent<AudioSource>();
    }

    // Specifies when game start
    private void Start()
    {
        EggPhysics_Script ep_Script = GameObject.Find("InGame_Panel").GetComponent<EggPhysics_Script>();
        maxPower = ep_Script.GetMaxPower();
        rb.mass = ep_Script.GetMass();
        rb.drag = ep_Script.GetLinearDrag();
    }

    // Action while the egg is moving
    public IEnumerator EggIsMoving()
    {
        while (rb.velocity != Vector2.zero)
        {
            lastVelocity = rb.velocity;

            float eggX = transform.position.x;
            float eggY = transform.position.y;

            if (eggX >= panOut || eggX <= -panOut || eggY >= panOut || eggY <= -panOut)
                GameObject.Find("InGame_Panel").GetComponent<EggControl_Script>().DestroyEgg(colorBool, gameObject);

            yield return null;
        }
        lastVelocity = Vector2.zero;
    }

    // If egg collides with another one, reflects and play SE
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collision_SE.Play();

        if (lastVelocity != Vector2.zero)
        {
            float collisionAngle = Vector2.Angle(rb.velocity.normalized, collision.contacts[0].normal);
            if (collisionAngle > lickAngle)
            {
                Vector2 reflectVelocity = Vector2.Reflect(rb.velocity.normalized, collision.contacts[0].normal);
                rb.velocity = reflectVelocity;
            }
        }
        else
            StartCoroutine(EggIsMoving());
    }

    /*
     * Set egg's color
     * 
     * @param bool color
     */
    public void SetEgg(bool color)
    {
        colorBool = color;
        GetComponent<AudioSource>().volume = GameObject.Find("Scene_Director").GetComponent<PreferencesData_Script>().GetSeVolume();
    }

    /*
     * Return egg's color
     * 
     * @return bool
     */
    public bool GetColor()
    {
        return colorBool;
    }

    /*
     * Set eggs' velocity
     * 
     * @param float powerGage
     */
    public void SetEggVelocity(float powerGagePercentage)
    {
        rb.velocity = transform.up * maxPower * powerGagePercentage;

        StartCoroutine(EggIsMoving());
    }
}