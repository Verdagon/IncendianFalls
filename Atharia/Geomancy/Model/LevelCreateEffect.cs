using System;
using System.Collections;

using System.Collections.Generic;

namespace Geomancer.Model {

public struct LevelCreateEffect : ILevelEffect {
  public readonly int id;
  public readonly LevelIncarnation incarnation;
  public LevelCreateEffect(int id, LevelIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int ILevelEffect.id => id;
  public void visitILevelEffect(ILevelEffectVisitor visitor) {
    visitor.visitLevelCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitLevelEffect(this);
  }
}

}
