using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Initializes the game
/// </summary>
public class GameInitializaer : MonoBehaviour
{
    // Awake is called before Start
    void Awake()
    {
        // Initialize screen utils
        ScreenUtils.Initialize();
    }

}
