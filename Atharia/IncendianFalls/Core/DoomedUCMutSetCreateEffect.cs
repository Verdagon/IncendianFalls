using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct DoomedUCMutSetCreateEffect : IDoomedUCMutSetEffect {
  public readonly int id;
  public DoomedUCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IDoomedUCMutSetEffect.id => id;
  public void visitIDoomedUCMutSetEffect(IDoomedUCMutSetEffectVisitor visitor) {
    visitor.visitDoomedUCMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitDoomedUCMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
