using System;
using System.Collections.Generic;
using Atharia.Model;
using UnityEngine;
using UnityEngine.UI;
using AthPlayer;

namespace Domino {
  public class CameraController {
    private IClock clock;
    private GameObject cameraObject;
    // Where it's supposed to be, after all the animations are done.
    private Vector3 cameraEndLookAtPosition;
    private Vector3 cameraAngle;

    public Vector3 endLookAtPosition {  get { return cameraEndLookAtPosition; } }

    private readonly static float cameraSpeedPerSecond = 8.0f;

    public CameraController(IClock clock, GameObject camera, Vector3 initialLookAtPosition, Vector3 cameraAngle) {
      this.clock = clock;
      this.cameraObject = camera;
      
      cameraEndLookAtPosition = initialLookAtPosition;
      this.cameraAngle = cameraAngle;
      camera.transform.FromMatrix(CalculateCameraMatrix(cameraEndLookAtPosition));
    }

    private Matrix4x4 CalculateCameraMatrix(Vector3 lookAtPosition) {
      MatrixBuilder builder = new MatrixBuilder(Matrix4x4.identity);
      // 26.6f is atan(5/10)
      builder.Rotate(Quaternion.AngleAxis(90 - 26.6f, Vector3.right));


      //float pitch = Vector3.Angle(-cameraAngle, Vector3.forward);
      //float yaw = (float)(Math.Atan2(cameraAngle.x, cameraAngle.z) / Math.PI * 180);
      //builder.Rotate(Quaternion.Euler(pitch, yaw, 0));

      builder.Translate(
    new Vector3(lookAtPosition.x, lookAtPosition.y + 16, lookAtPosition.z - 8));


      //builder.Translate(
      //    new Vector3(
      //      lookAtPosition.x + cameraAngle.x,
      //      lookAtPosition.y + cameraAngle.y,
      //      lookAtPosition.z + cameraAngle.z));
      return builder.matrix;
    }

    private CameraAnimator GetOrCreateCameraAnimator() {
      var animator = cameraObject.GetComponent<CameraAnimator>();
      if (animator == null) {
        animator = cameraObject.AddComponent<CameraAnimator>() as CameraAnimator;
        animator.Init(clock, cameraObject, new ConstantMatrix4x4Animation(cameraObject.transform.localToWorldMatrix));
      }
      Asserts.Assert(animator != null);
      return animator;
    }

    public void StartMovingCameraTo(Vector3 newCameraEndLookAtPosition, long durationMs) {
      var currentCameraEndLookAtPosition = cameraEndLookAtPosition;
      //var currentCameraMatrix = CalculateCameraMatrix(currentCameraEndLookAtPosition);

      //var newCameraMatrix = CalculateCameraMatrix(newCameraEndLookAtPosition);

      var cameraDifference = newCameraEndLookAtPosition - currentCameraEndLookAtPosition;

      var animator = GetOrCreateCameraAnimator();
      animator.cameraAnimation =
          new ComposeMatrix4x4Animation(
              animator.cameraAnimation,
              new ClampMatrix4x4Animation(
                  clock.GetTimeMs(), clock.GetTimeMs() + durationMs,
                  new ComposeMatrix4x4Animation(
                      new ConstantMatrix4x4Animation(Matrix4x4.Translate(cameraDifference)),
                      new LinearMatrix4x4Animation(
                          clock.GetTimeMs(), clock.GetTimeMs() + durationMs, Matrix4x4.Translate(-cameraDifference), Matrix4x4.identity))));

      cameraEndLookAtPosition = newCameraEndLookAtPosition;
    }

    public void MoveIn(float deltaTime) {
      var newEndLookAtPosition =
          cameraEndLookAtPosition +
                cameraObject.transform.forward * deltaTime * cameraSpeedPerSecond;
      StartMovingCameraTo(newEndLookAtPosition, 50);
    }

    public void MoveOut(float deltaTime) {
      var newEndLookAtPosition =
          cameraEndLookAtPosition -
                cameraObject.transform.forward * deltaTime * cameraSpeedPerSecond;
      StartMovingCameraTo(newEndLookAtPosition, 50);
    }

    public void MoveUp(float deltaTime) {
      var newEndLookAtPosition =
          cameraEndLookAtPosition +
                cameraObject.transform.up * deltaTime * cameraSpeedPerSecond;
      StartMovingCameraTo(newEndLookAtPosition, 50);
    }

    public void MoveDown(float deltaTime) {
      var newEndLookAtPosition =
          cameraEndLookAtPosition -
                cameraObject.transform.up * deltaTime * cameraSpeedPerSecond;
      StartMovingCameraTo(newEndLookAtPosition, 50);
    }

    public void MoveLeft(float deltaTime) {
      var newEndLookAtPosition =
          cameraEndLookAtPosition -
              cameraObject.transform.right * deltaTime * cameraSpeedPerSecond;
      StartMovingCameraTo(newEndLookAtPosition, 50);
    }

    public void MoveRight(float deltaTime) {
      var newEndLookAtPosition =
          cameraEndLookAtPosition +
              cameraObject.transform.right * deltaTime * cameraSpeedPerSecond;
      StartMovingCameraTo(newEndLookAtPosition, 50);
    }
  }
}
