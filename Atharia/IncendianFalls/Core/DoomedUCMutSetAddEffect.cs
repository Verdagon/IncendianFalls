using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct DoomedUCMutSetAddEffect : IDoomedUCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public DoomedUCMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IDoomedUCMutSetEffect.id => id;
  public void visit(IDoomedUCMutSetEffectVisitor visitor) {
    visitor.visitDoomedUCMutSetAddEffect(this);
  }
}

}
