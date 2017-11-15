using System;
using UnityEngine;

 public class TRaySpacing
 {
 public int HorizontalRayCount { get; private set; }

 public int VerticalRayCount { get; private set; }

 public float HorizontalRaySpacing { get; private set; }

 public float VerticalRaySpacing { get; private set; }

 public TRaySpacing(Bounds bounds, float maxDistanceBetweenRays)
 {
    var boundsWidth = bounds.size.x;
    var boundsHeight = bounds.size.y;

     if (maxDistanceBetweenRays<TMathHelper.FloatEpsilon)
    {
         var message = string.Format("The max distance between the rays cannot be smaller than {0}.", TMathHelper.FloatEpsilon);
         throw new ArgumentOutOfRangeException("maxDistanceBetweenRays", maxDistanceBetweenRays, message);
    }

    HorizontalRayCount = Mathf.RoundToInt(boundsHeight / maxDistanceBetweenRays + 1.5f);
    VerticalRayCount = Mathf.RoundToInt(boundsWidth / maxDistanceBetweenRays + 1.5f);
    HorizontalRaySpacing = boundsHeight / (HorizontalRayCount - 1);
    VerticalRaySpacing = boundsWidth / (VerticalRayCount - 1); ;
 }
 }