using System;
using System.Collections.Generic;
using Atharia.Model;
using UnityEngine;
using UnityEngine.UI;
using IncendianFalls;

namespace Domino {
  public class CameraController {
    private GameObject cameraObject;
    // Where it's supposed to be, after all the animations are done.
    private Vector3 cameraEndLookAtPosition;

    public Vector3 endLookAtPosition {  get { return cameraEndLookAtPosition; } }

    private readonly static float cameraSpeedPerSecond = 8.0f;

    public CameraController(GameObject camera, Vector3 initialLookAtPosition) {
      this.cameraObject = camera;
      
      cameraEndLookAtPosition = initialLookAtPosition;
      camera.transform.FromMatrix(CalculateCameraMatrix(cameraEndLookAtPosition));
    }

    private Matrix4x4 CalculateCameraMatrix(Vector3 lookAtPosition) {
      MatrixBuilder builder = new MatrixBuilder(Matrix4x4.identity);
      builder.Rotate(Quaternion.AngleAxis(90 - 26.6f, Vector3.right));
      builder.Translate(
          new Vector3(lookAtPosition.x, lookAtPosition.y + 16, lookAtPosition.z - 8));
      // 26.6f is atan(5/10)
      return builder.matrix;
    }

    private CameraAnimator GetOrCreateCameraAnimator() {
      var animator = cameraObject.GetComponent<CameraAnimator>();
      if (animator == null) {
        animator = cameraObject.AddComponent<CameraAnimator>() as CameraAnimator;
        animator.Init(cameraObject, new ConstantMatrix4x4Animation(cameraObject.transform.localToWorldMatrix));
      }
      return animator;
    }

    public void StartMovingCameraTo(Vector3 newCameraEndLookAtPosition, float duration) {
      var currentCameraEndLookAtPosition = cameraEndLookAtPosition;
      //var currentCameraMatrix = CalculateCameraMatrix(currentCameraEndLookAtPosition);

      //var newCameraMatrix = CalculateCameraMatrix(newCameraEndLookAtPosition);

      var cameraDifference = newCameraEndLookAtPosition - currentCameraEndLookAtPosition;

      var animator = GetOrCreateCameraAnimator();
      animator.cameraAnimation =
          new ComposeMatrix4x4Animation(
              animator.cameraAnimation,
              new ClampMatrix4x4Animation(
                  Time.time, Time.time + duration,
                  new ComposeMatrix4x4Animation(
                      new ConstantMatrix4x4Animation(Matrix4x4.Translate(cameraDifference)),
                      new LinearMatrix4x4Animation(
                          Time.time, Time.time + duration, Matrix4x4.Translate(-cameraDifference), Matrix4x4.identity))));

      cameraEndLookAtPosition = newCameraEndLookAtPosition;
    }

    public void MoveIn(float deltaTime) {
      var newEndLookAtPosition =
          cameraEndLookAtPosition +
                cameraObject.transform.forward * deltaTime * cameraSpeedPerSecond;
      StartMovingCameraTo(newEndLookAtPosition, .05f);
    }

    public void MoveOut(float deltaTime) {
      var newEndLookAtPosition =
          cameraEndLookAtPosition -
                cameraObject.transform.forward * deltaTime * cameraSpeedPerSecond;
      StartMovingCameraTo(newEndLookAtPosition, .05f);
    }

    public void MoveUp(float deltaTime) {
      var newEndLookAtPosition =
          cameraEndLookAtPosition +
                cameraObject.transform.up * deltaTime * cameraSpeedPerSecond;
      StartMovingCameraTo(newEndLookAtPosition, .05f);
    }

    public void MoveDown(float deltaTime) {
      var newEndLookAtPosition =
          cameraEndLookAtPosition -
                cameraObject.transform.up * deltaTime * cameraSpeedPerSecond;
      StartMovingCameraTo(newEndLookAtPosition, .05f);
    }

    public void MoveLeft(float deltaTime) {
      var newEndLookAtPosition =
          cameraEndLookAtPosition -
              cameraObject.transform.right * deltaTime * cameraSpeedPerSecond;
      StartMovingCameraTo(newEndLookAtPosition, .05f);
    }

    public void MoveRight(float deltaTime) {
      var newEndLookAtPosition =
          cameraEndLookAtPosition +
              cameraObject.transform.right * deltaTime * cameraSpeedPerSecond;
      StartMovingCameraTo(newEndLookAtPosition, .05f);
    }
  }
}
