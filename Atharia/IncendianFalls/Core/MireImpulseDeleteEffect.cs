using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct MireImpulseDeleteEffect : IMireImpulseEffect {
  public readonly int id;
  public MireImpulseDeleteEffect(int id) {
    this.id = id;
  }
  int IMireImpulseEffect.id => id;
  public void visit(IMireImpulseEffectVisitor visitor) {
    visitor.visitMireImpulseDeleteEffect(this);
  }
}

}
