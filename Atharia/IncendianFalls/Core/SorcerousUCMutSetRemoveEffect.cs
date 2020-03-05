using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct SorcerousUCMutSetRemoveEffect : ISorcerousUCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public SorcerousUCMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int ISorcerousUCMutSetEffect.id => id;
  public void visit(ISorcerousUCMutSetEffectVisitor visitor) {
    visitor.visitSorcerousUCMutSetRemoveEffect(this);
  }
}

}
