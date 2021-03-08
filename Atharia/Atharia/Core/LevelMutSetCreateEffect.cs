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
  public void visitILevelMutSetEffect(ILevelMutSetEffectVisitor visitor) {
    visitor.visitLevelMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitLevelMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
