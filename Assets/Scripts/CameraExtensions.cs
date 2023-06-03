using UnityEngine;

public static class CameraSetup
{
   /// <summary>
   ///     Extension method to adjust the camera so that it is always in the same position, regardless of the shape of the
   ///     board.
   /// </summary>
   /// <param name="cam">Main Camera</param>
   /// <param name="numberOfRows">Number of rows of the board</param>
   /// <param name="numberOfColumns">Number of columns of the board</param>
   /// <param name="tileOffset">Const value that all tiles positions are multiplied by</param>
   public static void Setup(this Camera cam, int numberOfRows, int numberOfColumns, float tileOffset)
    {
        var evenColumnNumberCoefficient = numberOfRows % 2 == 0 ? (-0.5f * tileOffset) : 0;
        var x = numberOfRows;
        // ReSharper disable once PossibleLossOfFraction
        var y = numberOfColumns / 2 + evenColumnNumberCoefficient;

        var camGo = cam.gameObject;
        // y value has been determined empirically, just tried different values and that fits the best for any case tested
        //TODO: verify and optionally come up with new algorithm for camera position
        camGo.transform.position = new Vector3(x * tileOffset, (float)(numberOfRows + numberOfColumns + 1) / 2, y * tileOffset);
        
        //TODO: move rotation values to variables 
        var camRotation = Quaternion.Euler(60f, -90f, 0f);
        camGo.transform.rotation = camRotation;
    }
}