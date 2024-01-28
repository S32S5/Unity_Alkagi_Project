/*
 * Controls egg
 * 
 * Script Explanation
 * - Check egg in pan
 * - Egg collision event
 * - Set egg's color
 * - Set egg's velocity
 * - Return egg's color
 * 
 * @version 0.0.2, Code optimization
 * @author S3
 * @date 2024/01/28
 */

using UnityEngine;

public class Egg_Script : MonoBehaviour
{
    private bool colorBool;

    private Rigidbody2D rb;
    private Vector3 lastVelocity;

    private AudioSource Collision_SE;

    private static float panOut = 4.5f;

    // Specifies
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Collision_SE = GetComponent<AudioSource>();
    }

    private void Update()
    {
        lastVelocity = rb.velocity;
        EggLeavingPan();
    }

    // If egg leaves the pan, delete it
    private void EggLeavingPan()
    {
        float eggX = transform.position.x;
        float eggY = transform.position.y;

        if (eggX >= panOut || eggX <= -panOut || eggY >= panOut || eggY <= -panOut)
            GameObject.Find("InGame_Canvas").GetComponent<EggControl_Script>().DestroyEgg(colorBool, gameObject);
    }

    // If egg collides with another one, reflects and play SE
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (lastVelocity != Vector3.zero && collision.transform.GetComponent<Rigidbody2D>().velocity != Vector2.zero)
        {
            Collision_SE.Play();

            Vector2 reflectVelocity = Vector2.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
            rb.velocity = reflectVelocity;
        }
    }

    /*
     * Set egg's color
     * 
     * @param bool color
     */
    public void SetEggColor(bool color)
    {
        colorBool = color;
    }

    /*
     * Set eggs' velocity
     * 
     * @param speed
     */
    public void SetEggMove(float speed)
    {
        rb.velocity = transform.up * speed;
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
}