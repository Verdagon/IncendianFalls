using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct LevelMutSetAddEffect : ILevelMutSetEffect {
  public readonly int id;
  public readonly int element;
  public LevelMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int ILevelMutSetEffect.id => id;
  public void visitILevelMutSetEffect(ILevelMutSetEffectVisitor visitor) {
    visitor.visitLevelMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitLevelMutSetEffect(this);
  }
}

}
