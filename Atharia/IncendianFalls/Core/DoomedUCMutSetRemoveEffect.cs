using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct DoomedUCMutSetRemoveEffect : IDoomedUCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public DoomedUCMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IDoomedUCMutSetEffect.id => id;
  public void visit(IDoomedUCMutSetEffectVisitor visitor) {
    visitor.visitDoomedUCMutSetRemoveEffect(this);
  }
}

}
