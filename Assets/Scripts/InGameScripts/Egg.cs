/*
 * Control eggs status
 *
 * @version 1.1.0
 * - Add savedRb
 * @author S3
 * @date 2024/03/11
 */

using System.Collections;
using UnityEngine;

public class Egg : MonoBehaviour
{
    private bool colorBool;

    private Rigidbody2D rb;
    private Vector2 lastVelocity;
    private Vector2 savedRb;
    private AudioSource Collision_SE;

    private static float maxPower;
    private static float lickAngle = 115, panOut = 4.5f;

    InGameCanvasController inGame;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Collision_SE = GetComponent<AudioSource>();

        EggPhysicsController physics = GameObject.Find("InGamePanel").GetComponent<EggPhysicsController>();
        maxPower = physics.GetMaxPower();
        rb.mass = physics.GetMass();
        rb.drag = physics.GetLinearDrag();

        inGame = GameObject.Find("InGameCanvas").GetComponent<InGameCanvasController>();
    }

    private void Update()
    {
        if(!inGame.GetPlayGame()) 
        { 
            if(rb.velocity != Vector2.zero)
            {
                savedRb = rb.velocity;
                rb.velocity = Vector2.zero;
            }
        }
        else
        {
            if (rb.velocity == Vector2.zero)
            {
                rb.velocity = savedRb;
                savedRb = Vector2.zero;
            }

            float eggX = transform.position.x;
            float eggY = transform.position.y;

            if (eggX >= panOut || eggX <= -panOut || eggY >= panOut || eggY <= -panOut)
                GameObject.Find("InGamePanel").GetComponent<EggController>().DestroyEgg(colorBool, gameObject);

            lastVelocity = rb.velocity;
        }
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
    }

    public Vector2 GetSavedRb() { return savedRb; }
}