using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct LevelMutSetCreateEffect : ILevelMutSetEffect {
  public readonly int id;
  public LevelMutSetCreateEffect(int id) {
    this.id = id;
  }
  int ILevelMutSetEffect.id => id;
  public void visit(ILevelMutSetEffectVisitor visitor) {
    visitor.visitLevelMutSetCreateEffect(this);
  }
}

}
