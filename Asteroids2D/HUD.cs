
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The HUD
/// </summary>
public class HUD : MonoBehaviour
{
    [SerializeField]
    Text timerText;

    // Game timer support
    float elapsedSeconds = 0;
    float gameTimer = 0;
    bool gameTimerRunning = true;

    // Start is called before the first frame update
    void Start()
    {
        timerText.text = "0";
    }

    /// <summary>
    /// Shows how long the player has been playing
    /// </summary>
    void Update()
    {
        if (gameTimerRunning)
        {   
            // Update game timer 
            elapsedSeconds += Time.deltaTime;
            if (elapsedSeconds > 1)
            {
                gameTimer += 1;
                timerText.text = gameTimer.ToString();
                elapsedSeconds = 0;
            }

        }       
        
    }

    /// <summary>
    /// Stop the game timer
    /// </summary>
    public void StopGameTimer()
    {
        gameTimerRunning = false;
    }
}
