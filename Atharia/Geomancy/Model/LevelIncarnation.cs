using System;
using System.Collections;

using System.Collections.Generic;

namespace Geomancer.Model {
public class LevelIncarnation : ILevelEffectVisitor {
  public readonly int terrain;
  public LevelIncarnation(
      int terrain) {
    this.terrain = terrain;
  }
  public LevelIncarnation Copy() {
    return new LevelIncarnation(
terrain    );
  }

  public void visitLevelCreateEffect(LevelCreateEffect e) {}
  public void visitLevelDeleteEffect(LevelDeleteEffect e) {}

  public void ApplyEffect(ILevelEffect effect) { effect.visitILevelEffect(this); }
}

}
