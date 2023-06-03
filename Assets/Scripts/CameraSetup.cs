using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class CameraSetup
{
   /// <summary>
   /// Extension method to adjust the camera so that it is always in the same position, regardless of the shape of the board.
   /// </summary>
   /// <param name="cam">Main Camera</param>
   /// <param name="numberOfRows">Number of rows of the board</param>
   /// <param name="numberOfColumns">Number of columns of the board</param>
   /// <param name="tileOffset">Const value that all tiles positions are multiplied by</param>
   public static void Setup(this Camera cam, int numberOfRows, int numberOfColumns, float tileOffset)
   {
      var evenRowNumberCoefficient = numberOfRows % 2 == 0 ? 0 : 1;
      // Second numberOfRows / 2  moves the camera closer to the bottom of the board
      // EvenRowNumberCoefficient here is so that there is (almost) no visible difference between even and odd row numbers
      int x = numberOfRows / 2 + numberOfRows / 2 + evenRowNumberCoefficient;
      int y = numberOfColumns / 2;

      var camGo = cam.gameObject;
      // y value has been determined empirically, just tried different values and that fits the best for any case tested
      camGo.transform.position = new Vector3(x * tileOffset, (float)(numberOfRows + numberOfColumns + 1) / 2, y * tileOffset);
      
      var camRotation = Quaternion.Euler(60f, -90f, 0f);
      camGo.transform.rotation = camRotation;
   }
}
