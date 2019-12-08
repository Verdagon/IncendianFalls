using System;
using System.Collections.Generic;
using Atharia.Model;
using UnityEngine;
using UnityEngine.UI;
using IncendianFalls;

namespace Domino {
  public class FollowingCameraController :
      IUnitEffectObserver, IUnitEffectVisitor,
      IGameEffectObserver, IGameEffectVisitor {
    GameObject cameraObject;
    Game game;
    Unit followedUnit;

    Location cameraEndLookAtLocation;
    // Where it's supposed to be, after all the animations are done.
    Vector3 cameraEndLookAtPosition;

    private readonly static float cameraSpeedPerSecond = 8.0f;

    public FollowingCameraController(GameObject camera, Game game) {
      throw new Exception("split this into a basic camera and a unit follower");

      this.cameraObject = camera;
      this.game = game;

      this.game.AddObserver(this);

      followedUnit = Unit.Null;
      cameraEndLookAtLocation = game.player.location;
      cameraEndLookAtPosition = game.level.terrain.GetTileCenter(cameraEndLookAtLocation).ToUnity();
      camera.transform.FromMatrix(CalculateCameraMatrix(cameraEndLookAtPosition));

      RefollowPlayer();
    }

    public void OnUnitEffect(IUnitEffect effect) { effect.visit(this); }
    public void visitUnitCreateEffect(UnitCreateEffect effect) { }
    public void visitUnitDeleteEffect(UnitDeleteEffect effect) { }
    public void visitUnitSetHpEffect(UnitSetHpEffect effect) { }
    public void visitUnitSetMpEffect(UnitSetMpEffect effect) { }
    public void visitUnitSetAliveEffect(UnitSetAliveEffect effect) { }
    public void visitUnitSetLifeEndTimeEffect(UnitSetLifeEndTimeEffect effect) { }
    public void visitUnitSetNextActionTimeEffect(UnitSetNextActionTimeEffect effect) { }
    public void visitUnitSetLocationEffect(UnitSetLocationEffect effect) {
      RefollowPlayer();
    }

    public void OnGameEffect(IGameEffect effect) { effect.visit(this); }
    public void visitGameCreateEffect(GameCreateEffect effect) { }
    public void visitGameDeleteEffect(GameDeleteEffect effect) { }
    public void visitGameSetLevelEffect(GameSetLevelEffect effect) { }
    public void visitGameSetTimeEffect(GameSetTimeEffect effect) { }
    public void visitGameSetPlayerEffect(GameSetPlayerEffect effect) {
      RefollowPlayer();
    }

    private void RefollowPlayer() {
      if (followedUnit.Exists()) {
        if (game.player.Exists()) {
          if (followedUnit.NullableIs(game.player)) {
            // Do nothing
          } else {
            // Player changed, follow this one now
            followedUnit.RemoveObserver(this);
            followedUnit = this.game.player;
            followedUnit.AddObserver(this);
          }
        } else {
          followedUnit.RemoveObserver(this);
        }
      } else {
        if (game.player.Exists()) {
          followedUnit = this.game.player;
          followedUnit.AddObserver(this);
        } else {
          // Do nothing, followedUnit and player both dont exist
        }
      }

      if (followedUnit.Exists() &&
          followedUnit.location != cameraEndLookAtLocation) {
        StartMovingCamera();
      }
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


    public void StartMovingCamera() {
      if (!followedUnit.Exists()) {
        Asserts.Assert(false);
      }
      var newCameraEndLookAtLocation = followedUnit.location;
      if (newCameraEndLookAtLocation == cameraEndLookAtLocation) {
        Asserts.Assert(false);
      }

      var currentCameraEndLookAtPosition = cameraEndLookAtPosition;
      //var currentCameraMatrix = CalculateCameraMatrix(currentCameraEndLookAtPosition);

      var newCameraEndLookAtPosition =
          game.level.terrain.GetTileCenter(newCameraEndLookAtLocation).ToUnity();
      //var newCameraMatrix = CalculateCameraMatrix(newCameraEndLookAtPosition);

      StartMovingCameraTo(newCameraEndLookAtPosition, .25f);
    }

    private void StartMovingCameraTo(Vector3 newCameraEndLookAtPosition, float duration) {
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
