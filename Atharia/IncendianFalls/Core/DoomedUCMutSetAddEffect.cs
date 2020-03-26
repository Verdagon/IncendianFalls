using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct DoomedUCMutSetAddEffect : IDoomedUCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public DoomedUCMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IDoomedUCMutSetEffect.id => id;
  public void visitIDoomedUCMutSetEffect(IDoomedUCMutSetEffectVisitor visitor) {
    visitor.visitDoomedUCMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitDoomedUCMutSetEffect(this);
  }
}

}
