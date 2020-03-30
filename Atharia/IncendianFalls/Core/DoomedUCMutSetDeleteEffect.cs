using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct DoomedUCMutSetDeleteEffect : IDoomedUCMutSetEffect {
  public readonly int id;
  public DoomedUCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IDoomedUCMutSetEffect.id => id;
  public void visitIDoomedUCMutSetEffect(IDoomedUCMutSetEffectVisitor visitor) {
    visitor.visitDoomedUCMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitDoomedUCMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
