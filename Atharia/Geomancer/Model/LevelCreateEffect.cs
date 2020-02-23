using System;
using System.Collections;

using System.Collections.Generic;

namespace Geomancer.Model {

public struct LevelCreateEffect : ILevelEffect {
  public readonly int id;
  public LevelCreateEffect(int id) {
    this.id = id;
  }
  int ILevelEffect.id => id;
  public void visit(ILevelEffectVisitor visitor) {
    visitor.visitLevelCreateEffect(this);
  }
}

}
