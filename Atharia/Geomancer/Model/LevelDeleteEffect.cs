using System;
using System.Collections;

using System.Collections.Generic;

namespace Geomancer.Model {

public struct LevelDeleteEffect : ILevelEffect {
  public readonly int id;
  public LevelDeleteEffect(int id) {
    this.id = id;
  }
  int ILevelEffect.id => id;
  public void visitILevelEffect(ILevelEffectVisitor visitor) {
    visitor.visitLevelDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitLevelEffect(this);
  }
}

}
