using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct GrassTTCMutSetRemoveEffect : IGrassTTCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public GrassTTCMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IGrassTTCMutSetEffect.id => id;
  public void visitIGrassTTCMutSetEffect(IGrassTTCMutSetEffectVisitor visitor) {
    visitor.visitGrassTTCMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitGrassTTCMutSetEffect(this);
  }
}

}
