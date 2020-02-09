
using UnityEngine;

/// <summary>
/// A bullet
/// </summary>
public class Bullet : MonoBehaviour
{
    //how long the bullet will live (seconds)
    private const float BulletLiveDuration = 1.3f;

    // holds the death timer for the bullet
    private Timer deathTimer;

    private const float BulletImpulseForce = 9f;

    // Start is called before the first frame update
    void Start()
    {
        // Create and run timer
        deathTimer = gameObject.AddComponent<Timer>();
        deathTimer.Duration = BulletLiveDuration;
        deathTimer.Run();
    }

    // Update is called once per frame
    void Update()
    {
        // kill the bullet when the death timer is finished.
        if (deathTimer.Finished)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Apply impulse Force to start moving the bullet
    /// </summary>
    /// <param name="velocityDirection">Direction the bullet is moving to</param>
    public void ApplyForce ( Vector2 velocityDirection)
    {
        
        GetComponent<Rigidbody2D>().AddForce(
                                   velocityDirection * BulletImpulseForce,
                                   ForceMode2D.Impulse);
    }
}
