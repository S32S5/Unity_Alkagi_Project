/*
 * Control eggs status
 *
 * @version 1.0.0
 * - Change class name Egg_Script to Egg
 * - Code optimization
 * @author S3
 * @date 2024/03/07
 */

using System.Collections;
using UnityEngine;

public class Egg : MonoBehaviour
{
    private bool colorBool;

    private Rigidbody2D rb;
    private Vector2 lastVelocity;
    private AudioSource Collision_SE;

    private static float maxPower;
    private static float lickAngle = 115, panOut = 4.5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Collision_SE = GetComponent<AudioSource>();

        EggPhysicsController ep_Script = GameObject.Find("InGameCanvas").GetComponent<EggPhysicsController>();
        maxPower = ep_Script.GetMaxPower();
        rb.mass = ep_Script.GetMass();
        rb.drag = ep_Script.GetLinearDrag();
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

    // Action while the egg is moving
    public IEnumerator EggIsMoving()
    {
        while (rb.velocity != Vector2.zero)
        {
            lastVelocity = rb.velocity;

            float eggX = transform.position.x;
            float eggY = transform.position.y;

            if (eggX >= panOut || eggX <= -panOut || eggY >= panOut || eggY <= -panOut)
                GameObject.Find("InGameCanvas").GetComponent<EggController>().DestroyEgg(colorBool, gameObject);

            yield return null;
        }
        lastVelocity = Vector2.zero;
    }

    // Set egg's color
    // 
    // @param bool
    public void SetEgg(bool color)
    {
        colorBool = color;
        GetComponent<AudioSource>().volume = GameObject.Find("SceneDirector").GetComponent<OptionDataController>().GetSeVolume();
    }

    // Return egg's color
    // 
    // @return bool
    public bool GetColor() { return colorBool; }

    // Set eggs' velocity
    // 
    // @param float
    public void SetEggVelocity(float powerGagePercentage)
    {
        rb.velocity = transform.up * maxPower * powerGagePercentage;

        StartCoroutine(EggIsMoving());
    }
}