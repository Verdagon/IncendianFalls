using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct DoomedUCCreateEffect : IDoomedUCEffect {
  public readonly int id;
  public DoomedUCCreateEffect(int id) {
    this.id = id;
  }
  int IDoomedUCEffect.id => id;
  public void visit(IDoomedUCEffectVisitor visitor) {
    visitor.visitDoomedUCCreateEffect(this);
  }
}

}
