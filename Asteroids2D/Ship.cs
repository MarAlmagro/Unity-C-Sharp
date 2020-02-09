using UnityEngine;

/// <summary>
/// Implement a space ship functionality :
/// thrusting, rotating, and screen wrapping.
/// </summary>
public class Ship : MonoBehaviour
{
    [SerializeField]
    GameObject prefabBullet;

    // Game Timer support
    [SerializeField]
    GameObject HUD;

    // Rigidbody component attached to the ship
    Rigidbody2D shipRigidBody;

    // Movement support
    const float ThrustForce = 5f;

    // Rotating support
    const float RotateDegreesPerSecond = 300f;

    // Direction the ship is moving to
    Vector2 thrustDirection = new Vector2(1, 0);

    

    /// <summary>
    /// Use this for initialization
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // Retrieve (for efficiency) the Rigidbody 2D attached to the Ship
        shipRigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // Save the Rotate axis value
        float rotationInput = Input.GetAxis("Rotate");

        // Rotate the ship using the left arrow key to rotate
        // counter-clockwise and the right arrow key to rotate 
        // clockwise
        if ( rotationInput != 0 )
        { 
            // Calculate rotation amount 
            float rotationAmount = RotateDegreesPerSecond * Time.deltaTime;
            if (rotationInput < 0)
            {
                rotationAmount *= -1;
            }

            // Apply rotation
            transform.Rotate(Vector3.forward, rotationAmount);

            //  *** Change the thrust direction after rotating the ship ***  //

            // Get the current rotation degrees around the z axis 
            // and convert this angle to radians
            float zAngle = (gameObject.transform.eulerAngles.z * Mathf.Deg2Rad);
            
            // Calculate the new values of the x and y components of the 
            // "thrustDirection" vector
            thrustDirection = new Vector2(Mathf.Cos(zAngle), Mathf.Sin(zAngle));
        }

        // When the left control key is pressed => Make the ship shoot bullets
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            // Create a new instance of a bullet prefab with the ship's transform
            GameObject bullet = Instantiate(prefabBullet) as GameObject;
            bullet.transform.position = transform.position;

            // Move the bullet in the direction the ship is facing
            bullet.GetComponent<Bullet>().ApplyForce(thrustDirection);

            // Play sound effect when shooting the bullet
            AudioManager.Play(AudioClipName.PlayerShot);
        }
    }

    /// <summary>
    /// Called once on each update of the physics system.
    //  Put anything that does need to be updated quickly in there.
    /// To control the Ship through the physics system using Rigidbody and 
    /// to have a more responsive game
    /// </summary>
    void FixedUpdate()
    {
        // Apply thrust using the space bar to move the ship
        // The thrust is applied in the direction the ship is facing
        if (Input.GetAxis("Thrust") != 0)
        {
            shipRigidBody.AddForce( thrustDirection * ThrustForce,
                                    ForceMode2D.Force);            
        }
    }

    /// <summary>
    /// Called when the ship hits an asteroid
    /// </summary>
    /// <param name="col">further information about the collision</param>    
    void OnCollisionEnter2D(Collision2D col)
    {
        // Destroy the ship if the incoming collider
        // is an asteroid and stop game timer
        if (col.collider.tag == "Asteroid")
        {
            // Play sound effect when ship is destroyed
            AudioManager.Play(AudioClipName.PlayerDeath);

            HUD.GetComponent<HUD>().StopGameTimer();

            Destroy(gameObject);

            // Destroy incoming asteroid
            //Destroy(col.collider.gameObject);
        }
    }

}

