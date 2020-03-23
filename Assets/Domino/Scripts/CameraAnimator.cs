using System;
using System.Collections.Generic;
using Atharia.Model;
using UnityEngine;
using UnityEngine.UI;
using AthPlayer;

namespace Domino {
  public class CameraAnimator : MonoBehaviour {
    bool initialized = false;

    private IClock clock;
    private GameObject cameraObject;

    public void Init(IClock clock, GameObject cameraObject, IVector3Animation initialLookAtAnimation, IVector3Animation initialOffsetFromLookAtAnimation) {
      this.clock = clock;
      this.cameraObject = cameraObject;
      this.lookAtAnimation = initialLookAtAnimation;
      this.offsetToLookAtAnimation = initialOffsetFromLookAtAnimation;
      initialized = true;
    }

    public IVector3Animation lookAtAnimation = new IdentityVector3Animation();
    public IVector3Animation offsetToLookAtAnimation = new IdentityVector3Animation();

    void Start() {
      Asserts.Assert(cameraObject != null);
      Asserts.Assert(initialized);
    }

    float CalculatePitchDegreesDownwardFromForward(Vector3 offsetToLookAt) {
      float distanceToLookAt = (float)Math.Sqrt(offsetToLookAt.x * offsetToLookAt.x + offsetToLookAt.y * offsetToLookAt.y + offsetToLookAt.z * offsetToLookAt.z);
      float heightAboveLookAt = -offsetToLookAt.y;

      float opposite = heightAboveLookAt;
      float hypotenuse = distanceToLookAt;
      float oppositeOverHypotenuse = opposite / hypotenuse;
      float radiansAngleFromForward = (float)Math.Asin(oppositeOverHypotenuse); ;
      float degreesAngleFromForward = radiansAngleFromForward * Mathf.Rad2Deg;
      return degreesAngleFromForward;
    }

    float CalculateYawDegreesRightFromForward(Vector3 offsetToLookAt) {
      // Only considering z and x here, because we're doing a flat spin of the camera.
      // Atan2 (adj to deg) will give us 0 for (1, 0), 45 for (1, 1), 90 for (0, 1), 180 for (-1, 0).
      // The camera's default forward is looking up the z axis, (0, 0, 1).
      // If we have a offsetToLookAt (0, 0, 1), then this function should return 0.
      // If we have a offsetToLookAt (1, 0, 1), then this function should return 45.
      // If we have a offsetToLookAt (1, 0, 0), then this function should return 90.
      // If we have a offsetToLookAt (1, 0, -1), then this function should return 135.
      // If we have a offsetToLookAt (0, 0, -1), then this function should return 180.
      // If we have a offsetToLookAt (-1, 0, -1), then this function should return 225.
      // If we have a offsetToLookAt (-1, 0, 0), then this function should return 270.

      float f = Mathf.Atan2(offsetToLookAt.x, offsetToLookAt.z) * Mathf.Rad2Deg;

      return f;
    }

    void Update() {
      lookAtAnimation = lookAtAnimation.Simplify(clock.GetTimeMs());
      offsetToLookAtAnimation = offsetToLookAtAnimation.Simplify(clock.GetTimeMs());

      Vector3 lookAt = lookAtAnimation.Get(clock.GetTimeMs());
      Vector3 offsetToLookAt = offsetToLookAtAnimation.Get(clock.GetTimeMs());
      cameraObject.transform.localPosition = lookAt - offsetToLookAt;

      cameraObject.transform.localRotation =
        Quaternion.Euler(
          CalculatePitchDegreesDownwardFromForward(offsetToLookAt),
          CalculateYawDegreesRightFromForward(offsetToLookAt),
          0);

      bool lookAtStable = lookAtAnimation is ConstantMatrix4x4Animation || lookAtAnimation is IdentityMatrix4x4Animation;
      bool offsetFromLookAtStable = offsetToLookAtAnimation is ConstantMatrix4x4Animation || offsetToLookAtAnimation is IdentityMatrix4x4Animation;
      if (lookAtStable && offsetFromLookAtStable) {
        Destroy(this);
      }
    }
  }
}
