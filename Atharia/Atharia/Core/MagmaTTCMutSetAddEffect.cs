using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct MagmaTTCMutSetAddEffect : IMagmaTTCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public MagmaTTCMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IMagmaTTCMutSetEffect.id => id;
  public void visitIMagmaTTCMutSetEffect(IMagmaTTCMutSetEffectVisitor visitor) {
    visitor.visitMagmaTTCMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitMagmaTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
