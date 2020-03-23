using System;
using System.Collections.Generic;
using Atharia.Model;
using UnityEngine;
using UnityEngine.UI;
using AthPlayer;

namespace Domino {
  public class FollowingCameraController :
      IUnitEffectObserver, IUnitEffectVisitor,
      IGameEffectObserver, IGameEffectVisitor {
    CameraController cameraController;

    Game game;
    Unit followedUnit;
    Location cameraEndLookAtLocation;

    private readonly static float cameraSpeedPerSecond = 8.0f;

    public FollowingCameraController(CameraController cameraController, Game game) {
      this.cameraController = cameraController;
      cameraEndLookAtLocation = game.player.location;

      this.game = game;

      this.game.AddObserver(this);

      followedUnit = Unit.Null;

      RefollowPlayer(0);
    }

    public void OnUnitEffect(IUnitEffect effect) { effect.visit(this); }
    public void visitUnitCreateEffect(UnitCreateEffect effect) { }
    public void visitUnitDeleteEffect(UnitDeleteEffect effect) { }
    public void visitUnitSetHpEffect(UnitSetHpEffect effect) { }
    public void visitUnitSetAliveEffect(UnitSetAliveEffect effect) { }
    public void visitUnitSetMaxHpEffect(UnitSetMaxHpEffect effect) { }
    public void visitUnitSetLifeEndTimeEffect(UnitSetLifeEndTimeEffect effect) { }
    public void visitUnitSetNextActionTimeEffect(UnitSetNextActionTimeEffect effect) { }
    public void visitUnitSetLocationEffect(UnitSetLocationEffect effect) {
      RefollowPlayer(250);
    }

    public void OnGameEffect(IGameEffect effect) { effect.visit(this); }
    public void visitGameCreateEffect(GameCreateEffect effect) { }
    public void visitGameDeleteEffect(GameDeleteEffect effect) { }
    public void visitGameSetHideInputEffect(GameSetHideInputEffect effect) { }
    public void visitGameSetLevelEffect(GameSetLevelEffect effect) {
      //cameraController.SetCameraAngle(game.level.cameraAngle.ToUnity());
    }
    public void visitGameSetInstructionsEffect(GameSetInstructionsEffect effect) { }
    public void visitGameSetTimeEffect(GameSetTimeEffect effect) { }
    public void visitGameSetPlayerEffect(GameSetPlayerEffect effect) {
      RefollowPlayer(0);
    }

    private void RefollowPlayer(long durationMs) {
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
        StartMovingCamera(durationMs);
      }
    }

    //private Matrix4x4 CalculateCameraMatrix(Vector3 lookAtPosition) {
    //  MatrixBuilder builder = new MatrixBuilder(Matrix4x4.identity);
    //  builder.Rotate(Quaternion.AngleAxis(90 - 26.6f, Vector3.right));
    //  builder.Translate(
    //      new Vector3(lookAtPosition.x, lookAtPosition.y + 16, lookAtPosition.z - 8));
    //  // 26.6f is atan(5/10)
    //  return builder.matrix;
    //}

    public void StartMovingCamera(long durationMs) {
      if (!followedUnit.Exists()) {
        Asserts.Assert(false);
      }
      var newCameraEndLookAtLocation = followedUnit.location;
      if (newCameraEndLookAtLocation == cameraEndLookAtLocation) {
        Asserts.Assert(false);
      }

      var currentCameraEndLookAtPosition = cameraController.endLookAtPosition;
      //var currentCameraMatrix = CalculateCameraMatrix(currentCameraEndLookAtPosition);

      var newCameraEndLookAtPosition =
          game.level.terrain.GetTileCenter(newCameraEndLookAtLocation).ToUnity();
      //var newCameraMatrix = CalculateCameraMatrix(newCameraEndLookAtPosition);

      cameraController.StartMovingCameraTo(newCameraEndLookAtPosition, durationMs);
    }

    public void MoveIn(float deltaTime) {
      cameraController.MoveIn(deltaTime);
    }

    public void MoveOut(float deltaTime) {
      cameraController.MoveOut(deltaTime);
    }

    public void MoveUp(float deltaTime) {
      cameraController.MoveUp(deltaTime);
    }

    public void MoveDown(float deltaTime) {
      cameraController.MoveDown(deltaTime);
    }

    public void MoveLeft(float deltaTime) {
      cameraController.MoveLeft(deltaTime);
    }

    public void MoveRight(float deltaTime) {
      cameraController.MoveRight(deltaTime);
    }

    public void StartMovingCameraTo(Vector3 newCameraEndLookAtPosition, long durationMs) {
      cameraController.StartMovingCameraTo(newCameraEndLookAtPosition, durationMs);
    }
  }
}
