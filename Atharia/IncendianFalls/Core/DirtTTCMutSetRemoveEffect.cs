using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct DirtTTCMutSetRemoveEffect : IDirtTTCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public DirtTTCMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IDirtTTCMutSetEffect.id => id;
  public void visit(IDirtTTCMutSetEffectVisitor visitor) {
    visitor.visitDirtTTCMutSetRemoveEffect(this);
  }
}

}
