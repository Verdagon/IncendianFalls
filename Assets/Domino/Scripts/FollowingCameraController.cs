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

    EffectBroadcaster broadcaster;
    Game game;
    Unit followedUnit;
    Location cameraEndLookAtLocation;

    public FollowingCameraController(CameraController cameraController, EffectBroadcaster broadcaster, Game game) {
      this.broadcaster = broadcaster;
      this.cameraController = cameraController;

      if (game.player.Exists()) {
        cameraEndLookAtLocation = game.player.location;
      } else {
        cameraEndLookAtLocation = new Location(0, 0, 0);
      }

      this.game = game;

      this.game.AddObserver(broadcaster, this);

      followedUnit = Unit.Null;

      RefollowPlayer(0);
    }

    public void OnUnitEffect(IUnitEffect effect) { effect.visitIUnitEffect(this); }
    public void visitUnitCreateEffect(UnitCreateEffect effect) { }
    public void visitUnitDeleteEffect(UnitDeleteEffect effect) { }
    public void visitUnitSetHpEffect(UnitSetHpEffect effect) { }
    public void visitUnitSetEvventEffect(UnitSetEvventEffect effect) { }
    public void visitUnitSetMaxHpEffect(UnitSetMaxHpEffect effect) { }
    public void visitUnitSetLifeEndTimeEffect(UnitSetLifeEndTimeEffect effect) { }
    public void visitUnitSetNextActionTimeEffect(UnitSetNextActionTimeEffect effect) { }
    public void visitUnitSetLocationEffect(UnitSetLocationEffect effect) {
      RefollowPlayer(250);
    }

    public void OnGameEffect(IGameEffect effect) { effect.visitIGameEffect(this); }
    public void visitGameCreateEffect(GameCreateEffect effect) { }
    public void visitGameDeleteEffect(GameDeleteEffect effect) { }
    public void visitGameSetActingUnitEffect(GameSetActingUnitEffect effect) { }
    public void visitGameSetPauseBeforeNextUnitEffect(GameSetPauseBeforeNextUnitEffect effect) { }
    public void visitGameSetActionNumEffect(GameSetActionNumEffect effect) { }
    public void visitGameSetEvventEffect(GameSetEvventEffect effect) { }
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
            followedUnit.RemoveObserver(broadcaster, this);
            followedUnit = this.game.player;
            followedUnit.AddObserver(broadcaster, this);
          }
        } else {
          followedUnit.RemoveObserver(broadcaster, this);
        }
      } else {
        if (game.player.Exists()) {
          followedUnit = this.game.player;
          followedUnit.AddObserver(broadcaster, this);
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

      //var currentCameraEndLookAtPosition = cameraController.endLookAtPosition;
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
