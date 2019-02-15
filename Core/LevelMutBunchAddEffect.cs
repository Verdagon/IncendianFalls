using System;
using System.Collections.Generic;

namespace Atharia.Model {

public struct LevelMutBunchAddEffect : ILevelMutBunchEffect {
  public readonly int id;
  public readonly int elementId;
  public LevelMutBunchAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int ILevelMutBunchEffect.id => id;
  public void visit(ILevelMutBunchEffectVisitor visitor) {
    visitor.visitLevelMutBunchAddEffect(this);
  }
}

}
