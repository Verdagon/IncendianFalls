using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct DoomedUCWeakMutSetCreateEffect : IDoomedUCWeakMutSetEffect {
  public readonly int id;
  public DoomedUCWeakMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IDoomedUCWeakMutSetEffect.id => id;
  public void visitIDoomedUCWeakMutSetEffect(IDoomedUCWeakMutSetEffectVisitor visitor) {
    visitor.visitDoomedUCWeakMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitDoomedUCWeakMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
