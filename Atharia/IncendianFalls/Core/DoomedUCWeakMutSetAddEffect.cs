using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct DoomedUCWeakMutSetAddEffect : IDoomedUCWeakMutSetEffect {
  public readonly int id;
  public readonly int element;
  public DoomedUCWeakMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IDoomedUCWeakMutSetEffect.id => id;
  public void visitIDoomedUCWeakMutSetEffect(IDoomedUCWeakMutSetEffectVisitor visitor) {
    visitor.visitDoomedUCWeakMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitDoomedUCWeakMutSetEffect(this);
  }
}

}
