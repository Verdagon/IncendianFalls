using System;
using System.Collections.Generic;

namespace Atharia.Model {

public struct LevelMutBunchRemoveEffect : ILevelMutBunchEffect {
  public readonly int id;
  public readonly int elementId;
  public LevelMutBunchRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int ILevelMutBunchEffect.id => id;
  public void visit(ILevelMutBunchEffectVisitor visitor) {
    visitor.visitLevelMutBunchRemoveEffect(this);
  }
}

}
