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
  public void visit(IDoomedUCMutSetEffectVisitor visitor) {
    visitor.visitDoomedUCMutSetDeleteEffect(this);
  }
}

}
