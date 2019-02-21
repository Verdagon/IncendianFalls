using System;
using System.Collections.Generic;

namespace Atharia.Model {
public struct LevelMutSetRemoveEffect : ILevelMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public LevelMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int ILevelMutSetEffect.id => id;
  public void visit(ILevelMutSetEffectVisitor visitor) {
    visitor.visitLevelMutSetRemoveEffect(this);
  }
}

}
