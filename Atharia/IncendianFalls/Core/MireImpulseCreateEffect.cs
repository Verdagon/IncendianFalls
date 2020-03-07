using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct MireImpulseCreateEffect : IMireImpulseEffect {
  public readonly int id;
  public MireImpulseCreateEffect(int id) {
    this.id = id;
  }
  int IMireImpulseEffect.id => id;
  public void visit(IMireImpulseEffectVisitor visitor) {
    visitor.visitMireImpulseCreateEffect(this);
  }
}

}
