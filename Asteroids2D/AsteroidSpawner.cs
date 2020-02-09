
using UnityEngine;

/// <summary>
/// Create a new Asteroid 
/// </summary>
public class AsteroidSpawner : MonoBehaviour
{
    // needed for spawning new asteroids
    // saved for efficiency
    [SerializeField]  
    GameObject[] prefabAsteroids;
    

    // How many different asteroid sprites are 
    private const int DifferentAsteroidsCount = 3;


    // Asteroids going right, left, up and down
    GameObject asteroid_right, asteroid_left, asteroid_up, asteroid_down;
    
    /// <summary>
    /// Use this for initialization
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        
        // Spawn new Asteroid coming into the screen from the 
        // bottom edge of the screen
        asteroid_up = SpawnAsteroid();
        float radius = asteroid_up.gameObject.GetComponent<CircleCollider2D>().radius;
        Vector3 location = new Vector3(0, ScreenUtils.ScreenBottom - radius, 0);
        asteroid_up.GetComponent<Asteroid>().Initialize(Direction.Up, location);
        
        // Spawn new Asteroid coming into the screen from the 
        // top edge of the screen
        asteroid_down = SpawnAsteroid();
        radius = asteroid_down.gameObject.GetComponent<CircleCollider2D>().radius;
        location = new Vector3(0, ScreenUtils.ScreenTop + radius, 0);
        asteroid_down.GetComponent<Asteroid>().Initialize(Direction.Down, location);

        // Spawn new Asteroid coming into the screen from the 
        // right edge of the screen
        asteroid_left = SpawnAsteroid();
        radius = asteroid_left.gameObject.GetComponent<CircleCollider2D>().radius;
        location = new Vector3(ScreenUtils.ScreenRight + radius, 0, 0);
        asteroid_left.GetComponent<Asteroid>().Initialize(Direction.Left, location);

        // Spawn new Asteroid coming into the screen from the 
        // left edge of the screen
        asteroid_right = SpawnAsteroid();
        radius = asteroid_right.gameObject.GetComponent<CircleCollider2D>().radius;
        location = new Vector3(ScreenUtils.ScreenLeft - radius, 0, 0);
        asteroid_right.GetComponent<Asteroid>().Initialize(Direction.Right, location);    
        
    }

    /// <summary>
    /// Spawn a new Asteroid with a random sprite.
    /// </summary>
    private GameObject SpawnAsteroid()
    {     
        // Create a new instance of a random asteroid prefab
        int spriteNumber = Random.Range(0, prefabAsteroids.Length);

        GameObject asteroid;
        
        asteroid = Instantiate(prefabAsteroids[spriteNumber]) as GameObject;
        return asteroid;
    }
}
