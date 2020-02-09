using UnityEngine;

/// <summary>
/// An asteroid functionality including
/// screen wrapping and moving in a random direction
/// </summary>
public class Asteroid : MonoBehaviour
{

    // Asteroid Impulse force, maximun and minimum values
    private const float MinImpulse = 1.5f;
    private const float MaxImpulse = 2.2f;

    /// <summary>
    /// Use this for initialization
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
       
    }

    /// <summary>
    /// set the movement direction for the asteroid
    /// </summary>
    /// <param name="moveDirection">In what direction the asteroid should move</param>
    /// <param name="location">Location of the asteroid</param>
    public void Initialize( Direction moveDirection, Vector3 location)
    {
        // set the position of the asteroid
        gameObject.transform.position = location;
        
        // Get random angle based on the "moveDirection" parameter 
        int angle;
        switch (moveDirection)
        {
            case (Direction.Up):
                angle = 75 + Random.Range(0, 30);
                break;
            case (Direction.Down):
                angle = 255 + Random.Range(0, 30);
                break;
            case (Direction.Right):
                angle = 345 + Random.Range(0, 30);
                break;
            default:
                angle = 165 + Random.Range(0, 30);
                break;
        }
        
        // Start moving the asteroid 
        StartMoving(angle * Mathf.Deg2Rad);
    }

    /// <summary>
    /// Apply impulse force to the asteroid
    /// </summary>
    /// <param name="angle">Angle(radians) at which to move the asteroid</param>
    void StartMoving (float angle)
    {
        // Get random velocity direction from the angle
        Vector2 velocityDirection =
            new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

        // Pick a random impulse force and apply it to the asteroid
        float impulseForce = Random.Range(MinImpulse, MaxImpulse);

        // Apply the impulse force to get the asteroid moving in the 
        // random direction
        gameObject.GetComponent<Rigidbody2D>().AddForce(
                                   velocityDirection * impulseForce,
                                   ForceMode2D.Impulse);
    }

    /// <summary>
    /// Called when the asteroid hits another game object
    /// </summary>
    /// <param name="col">further information about the collision</param>    
    void OnCollisionEnter2D(Collision2D col)
    {
        // Destroy the asteroid and the bullet if the 
        // incoming collider is a bullet.
        if (col.collider.tag == "Bullet")
        {
            // Play sound effect when hitting the asteroid
            AudioManager.Play(AudioClipName.AsteroidHit);

            // Destroy incoming bullet
            Destroy(col.collider.gameObject);

            // split larger asteroids
            if (gameObject.transform.localScale.x > 0.6f)
            {
                splitAsteroid();
            }

            // Destroy the asteroid
            Destroy(gameObject);            
        }
    }
    /// <summary>
    /// Split asteroid into two smaller asteroids
    /// with random velocities
    /// </summary>
    void splitAsteroid ()
    {
        // cut the scale of the asteroid in half in both x and y
        Vector3 scale = gameObject.transform.localScale;
        scale.x *= 0.7f;
        scale.y *= 0.7f;
        gameObject.transform.localScale = scale;

        // cut the radius of circle collider in half
        GetComponent<CircleCollider2D>().radius *= 0.7f;

        // Create two new smaller asteroids and start moving them
        // in random directions
        GameObject smallerAsteroid01 = Instantiate(gameObject) as GameObject;
        float angle = Random.Range(0, 2 * Mathf.PI);
        smallerAsteroid01.GetComponent<Asteroid>().StartMoving(angle);
        GameObject smallerAsteroid02 = Instantiate(gameObject) as GameObject;
        angle = Random.Range(0, 2 * Mathf.PI);
        smallerAsteroid02.GetComponent<Asteroid>().StartMoving(angle);
    }
}

