using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct DoomedUCWeakMutSetRemoveEffect : IDoomedUCWeakMutSetEffect {
  public readonly int id;
  public readonly int element;
  public DoomedUCWeakMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IDoomedUCWeakMutSetEffect.id => id;
  public void visitIDoomedUCWeakMutSetEffect(IDoomedUCWeakMutSetEffectVisitor visitor) {
    visitor.visitDoomedUCWeakMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitDoomedUCWeakMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
