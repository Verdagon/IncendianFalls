using UnityEngine;

public static class TransformExtensions {
  public static void FromMatrix(this Transform transform, Matrix4x4 matrix) {
    //Debug.Log("scale: " + matrix.ExtractScale() + " rot: " + matrix.ExtractRotation() + " pos: " + matrix.ExtractPosition());
    transform.localScale = matrix.ExtractScale();
    transform.localRotation = matrix.ExtractRotation();
    transform.localPosition = matrix.ExtractPosition();
  }
}
