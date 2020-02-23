using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct LevelMutSetAddEffect : ILevelMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public LevelMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int ILevelMutSetEffect.id => id;
  public void visit(ILevelMutSetEffectVisitor visitor) {
    visitor.visitLevelMutSetAddEffect(this);
  }
}

}
