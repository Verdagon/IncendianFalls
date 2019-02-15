using System;
using System.Collections.Generic;

namespace Atharia.Model {

public struct LevelMutBunchCreateEffect : ILevelMutBunchEffect {
  public readonly int id;
  public readonly LevelMutBunchIncarnation incarnation;
  public LevelMutBunchCreateEffect(
      int id,
      LevelMutBunchIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int ILevelMutBunchEffect.id => id;
  public void visit(ILevelMutBunchEffectVisitor visitor) {
    visitor.visitLevelMutBunchCreateEffect(this);
  }
}

}
