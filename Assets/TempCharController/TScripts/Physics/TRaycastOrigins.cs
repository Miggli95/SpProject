using UnityEngine;

 public struct TRaycastOrigins
 {
 public Vector2 TopLeft;

 public Vector2 TopRight;

 public Vector2 BottomLeft;

 public Vector2 BottomRight;

 public TRaycastOrigins Move(Vector2 movement)
 {
 var copy = new TRaycastOrigins();
 copy.TopLeft = TopLeft + movement;
 copy.TopRight = TopRight + movement;
 copy.BottomLeft = BottomLeft + movement;
 copy.BottomRight = BottomRight + movement;
 return copy;
}
}
