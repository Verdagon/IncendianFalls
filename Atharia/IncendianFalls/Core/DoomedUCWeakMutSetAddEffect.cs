using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct DoomedUCWeakMutSetAddEffect : IDoomedUCWeakMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public DoomedUCWeakMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IDoomedUCWeakMutSetEffect.id => id;
  public void visit(IDoomedUCWeakMutSetEffectVisitor visitor) {
    visitor.visitDoomedUCWeakMutSetAddEffect(this);
  }
}

}
