using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct FallsTTCMutSetAddEffect : IFallsTTCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public FallsTTCMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IFallsTTCMutSetEffect.id => id;
  public void visitIFallsTTCMutSetEffect(IFallsTTCMutSetEffectVisitor visitor) {
    visitor.visitFallsTTCMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitFallsTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
