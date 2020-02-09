using UnityEngine;

/// <summary>
/// Make a moving game object wrap when it leaves the
/// screen instead of disappearing from the game forever.
/// </summary>
public class ScriptWrapper : MonoBehaviour
{
    // Radius of the collider attached to the game object
    private float colliderRadius;

    /// <summary>
    /// Use this for initialization
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // saved for efficiency, the radius of the 
        // collider attached to the game object
        colliderRadius = gameObject.GetComponent<CircleCollider2D>().radius;

    }

    /// <summary>
    /// Called when the game object renderer is no longer visible by any camera
    /// Make the game object wrap around to the other side of the screen
    /// </summary>
    void OnBecameInvisible()
    {

        // game object current position
        Vector2 position = gameObject.transform.position;

        // When the game object leaves the screen, make the game  
        // object wrap around to the other side of the screen
        if (position.x > ScreenUtils.ScreenRight) 
        { 
            position.x *= -1; 
        } 
        else if (position.x < ScreenUtils.ScreenLeft) 
        { 
            position.x = ScreenUtils.ScreenRight + colliderRadius; 
        } 
        if (position.y > ScreenUtils.ScreenTop) 
        { 
            position.y *= -1; 
        } 
        else if (position.y < ScreenUtils.ScreenBottom) 
        { 
            position.y = ScreenUtils.ScreenTop + colliderRadius; 
        }

        // Set the new game object position
        gameObject.transform.position = position;
    }

}
 