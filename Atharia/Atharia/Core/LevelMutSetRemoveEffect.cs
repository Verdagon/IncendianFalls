using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct LevelMutSetRemoveEffect : ILevelMutSetEffect {
  public readonly int id;
  public readonly int element;
  public LevelMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int ILevelMutSetEffect.id => id;
  public void visitILevelMutSetEffect(ILevelMutSetEffectVisitor visitor) {
    visitor.visitLevelMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitLevelMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
