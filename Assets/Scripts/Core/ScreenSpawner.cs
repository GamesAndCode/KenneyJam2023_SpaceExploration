using UnityEngine;

public class ScreenSpawner
{

    private Vector3 bottomLeft, topLeft, bottomRight, topRight;
    private float width, height;
    private float percentageOutOffScreen = 0.2f;

    public Vector2 GetRandomPositionOutOfScreen(bool fromLeft, bool fromTop, bool fromRight, bool fromBottom)
    {
        CalculateScreenBorders();
        float x = bottomLeft.x + width * Random.Range(0.3f, 0.8f);
        float y = bottomLeft.y + height * Random.Range(0.3f, 0.8f);
        //if all are false, there is a possibiliy that the objects spawns on screen. So we just use fromLeft in that case
        if (fromLeft || (!fromLeft && !fromTop && !fromBottom && !fromRight))
        {
            x = bottomLeft.x - width * percentageOutOffScreen;
        }
        if (fromTop)
        {
            y = topLeft.y + height * percentageOutOffScreen;
        }
        if (fromRight)
        {
            x = bottomRight.x + width * percentageOutOffScreen;
        }
        if (fromBottom)
        {
            y = bottomLeft.y - height * percentageOutOffScreen;
        }
        return new Vector2(x, y);
    }

    private void CalculateScreenBorders()
    {
        // Get the camera used to render the scene
        Camera mainCamera = Camera.main;

        // Get the width and height of the screen (eg. 1920x1080)
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // Get the distance between the camera and the near clipping plane
        float cameraDistance = mainCamera.nearClipPlane;

        // Calculate the coordinates of the screen borders
        bottomLeft = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, cameraDistance));
        topLeft = mainCamera.ScreenToWorldPoint(new Vector3(0, screenHeight, cameraDistance));
        topRight = mainCamera.ScreenToWorldPoint(new Vector3(screenWidth, screenHeight, cameraDistance));
        bottomRight = mainCamera.ScreenToWorldPoint(new Vector3(screenWidth, 0, cameraDistance));

        width = bottomRight.x - bottomLeft.x;
        height = topLeft.y - bottomLeft.y;
    }

    public Vector2 MirrorPointAtMiddleOfScreen(Vector2 startingPoint)
    {
        Vector2 midpoint = new Vector2(bottomLeft.x + width / 2, bottomLeft.y + height / 2);
        Vector2 v = startingPoint - midpoint;
        Vector2 mirroredVector = new Vector2(-v.x, -v.y);
        return midpoint + mirroredVector;
    }
}
