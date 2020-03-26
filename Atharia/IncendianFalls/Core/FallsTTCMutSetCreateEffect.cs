using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct FallsTTCMutSetCreateEffect : IFallsTTCMutSetEffect {
  public readonly int id;
  public FallsTTCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IFallsTTCMutSetEffect.id => id;
  public void visitIFallsTTCMutSetEffect(IFallsTTCMutSetEffectVisitor visitor) {
    visitor.visitFallsTTCMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitFallsTTCMutSetEffect(this);
  }
}

}
