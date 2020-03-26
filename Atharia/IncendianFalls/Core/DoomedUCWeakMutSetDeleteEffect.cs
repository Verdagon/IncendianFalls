using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct DoomedUCWeakMutSetDeleteEffect : IDoomedUCWeakMutSetEffect {
  public readonly int id;
  public DoomedUCWeakMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IDoomedUCWeakMutSetEffect.id => id;
  public void visitIDoomedUCWeakMutSetEffect(IDoomedUCWeakMutSetEffectVisitor visitor) {
    visitor.visitDoomedUCWeakMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitDoomedUCWeakMutSetEffect(this);
  }
}

}
