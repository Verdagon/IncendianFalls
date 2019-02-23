using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct LevelMutSetCreateEffect : ILevelMutSetEffect {
  public readonly int id;
  public readonly LevelMutSetIncarnation incarnation;
  public LevelMutSetCreateEffect(
      int id,
      LevelMutSetIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int ILevelMutSetEffect.id => id;
  public void visit(ILevelMutSetEffectVisitor visitor) {
    visitor.visitLevelMutSetCreateEffect(this);
  }
}

}
