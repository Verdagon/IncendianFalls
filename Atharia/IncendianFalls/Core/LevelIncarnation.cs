using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class LevelIncarnation : ILevelEffectVisitor {
  public readonly Vec3 cameraAngle;
  public readonly int terrain;
  public readonly int units;
  public  int controller;
  public  int time;
  public LevelIncarnation(
      Vec3 cameraAngle,
      int terrain,
      int units,
      int controller,
      int time) {
    this.cameraAngle = cameraAngle;
    this.terrain = terrain;
    this.units = units;
    this.controller = controller;
    this.time = time;
  }
  public LevelIncarnation Copy() {
    return new LevelIncarnation(
cameraAngle,
terrain,
units,
controller,
time    );
  }

  public void visitLevelCreateEffect(LevelCreateEffect e) {}
  public void visitLevelDeleteEffect(LevelDeleteEffect e) {}



public void visitLevelSetControllerEffect(LevelSetControllerEffect e) { this.controller = e.newValue; }
public void visitLevelSetTimeEffect(LevelSetTimeEffect e) { this.time = e.newValue; }
  public void ApplyEffect(ILevelEffect effect) { effect.visitILevelEffect(this); }
}

}
