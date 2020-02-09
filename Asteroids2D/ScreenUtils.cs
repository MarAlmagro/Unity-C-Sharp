using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Static class (does not inherit from MonoBehaviour)
/// Provides screen utilities
/// </summary>
public static class ScreenUtils
{
    #region Fields

    //cached for efficient boundary checking
    private static float screenLeft;
    private static float screenRight;
    private static float screenTop;
    private static float screenBottom;

    #endregion


    #region Properties

    public static float ScreenLeft{
        get { return screenLeft; }
    }

    public static float ScreenRight
    {
        get { return screenRight; }
    }

    public static float ScreenTop
    {
        get { return screenTop; }
    }

    public static float ScreenBottom
    {
        get { return screenBottom; }
    }
    #endregion


    #region Methods
    /// <summary>
    /// Initializes the screen utilities
    /// </summary>
    public static void Initialize()
    {
        // save screen edges in world coordinates
        float screenZ = -Camera.main.transform.position.z;
        Vector3 lowerLeftCornerScreen = new Vector3(0, 0, screenZ);
        Vector3 upperRightCornerScreen = new Vector3(Screen.width, Screen.height, screenZ);

        Vector3 lowerLeftCornerWorld = Camera.main.ScreenToWorldPoint(lowerLeftCornerScreen);
        Vector3 upperRightCornerWorld = Camera.main.ScreenToWorldPoint(upperRightCornerScreen);

        // World coordinates of the four sides of the screen
        screenRight = upperRightCornerWorld.x;
        screenTop = upperRightCornerWorld.y;
        screenBottom = lowerLeftCornerWorld.y;
        screenLeft = lowerLeftCornerWorld.x;
    }

    #endregion

}
