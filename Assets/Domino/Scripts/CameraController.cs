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
    private Vector3 cameraOffsetToLookAt;

    private readonly static float cameraSpeedPerSecond = 8.0f;

    public CameraController(IClock clock, GameObject cameraObject, Vector3 initialLookAtPosition, Vector3 initialCameraOffsetToLookAt) {
      this.clock = clock;
      this.cameraObject = cameraObject;

      cameraEndLookAtPosition = initialLookAtPosition;
      cameraOffsetToLookAt = initialCameraOffsetToLookAt;

      GetOrCreateCameraAnimator().lookAtAnimation = new ConstantVector3Animation(cameraEndLookAtPosition);
      GetOrCreateCameraAnimator().offsetToLookAtAnimation = new ConstantVector3Animation(cameraOffsetToLookAt);
    }

    private CameraAnimator GetOrCreateCameraAnimator() {
      var animator = cameraObject.GetComponent<CameraAnimator>();
      if (animator == null) {
        animator = cameraObject.AddComponent<CameraAnimator>();
        animator.Init(
          clock,
          cameraObject,
          new IdentityVector3Animation(),
          new IdentityVector3Animation());
      }
      Asserts.Assert(animator != null);
      return animator;
    }

    public void StartRotatingCameraTo(Vector3 new_offsetToLookAt, long durationMs) {
      var animator = GetOrCreateCameraAnimator();
      if (durationMs == 0) {
        animator.lookAtAnimation =
          new ConstantVector3Animation(new_offsetToLookAt);
      } else {
        var currentCameraOffsetToLookAt = cameraOffsetToLookAt;
        var offsetToLookAtDifference = new_offsetToLookAt - currentCameraOffsetToLookAt;
        animator.offsetToLookAtAnimation =
            new AddVector3Animation(
                animator.offsetToLookAtAnimation,
                new ClampVector3Animation(
                    clock.GetTimeMs(), clock.GetTimeMs() + durationMs,
                    new AddVector3Animation(
                        new ConstantVector3Animation(offsetToLookAtDifference),
                        new LinearVector3Animation(
                            clock.GetTimeMs(), clock.GetTimeMs() + durationMs, -offsetToLookAtDifference, new Vector3(0, 0, 0)))));
      }
      cameraOffsetToLookAt = new_offsetToLookAt;
    }

    public void StartMovingCameraTo(Vector3 newCameraEndLookAtPosition, long durationMs) {
      var animator = GetOrCreateCameraAnimator();
      if (durationMs == 0) {
        animator.lookAtAnimation =
          new ConstantVector3Animation(newCameraEndLookAtPosition);
      } else {
        var currentCameraEndLookAtPosition = cameraEndLookAtPosition;
        var cameraDifference = newCameraEndLookAtPosition - currentCameraEndLookAtPosition;
        animator.lookAtAnimation =
            new AddVector3Animation(
                animator.lookAtAnimation,
                new ClampVector3Animation(
                    clock.GetTimeMs(), clock.GetTimeMs() + durationMs,
                    new AddVector3Animation(
                        new ConstantVector3Animation(cameraDifference),
                        new LinearVector3Animation(
                            clock.GetTimeMs(), clock.GetTimeMs() + durationMs, -cameraDifference, new Vector3(0, 0, 0)))));
      }
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
