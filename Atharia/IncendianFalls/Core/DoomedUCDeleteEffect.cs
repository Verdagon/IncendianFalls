using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct DoomedUCDeleteEffect : IDoomedUCEffect {
  public readonly int id;
  public DoomedUCDeleteEffect(int id) {
    this.id = id;
  }
  int IDoomedUCEffect.id => id;
  public void visit(IDoomedUCEffectVisitor visitor) {
    visitor.visitDoomedUCDeleteEffect(this);
  }
}

}
