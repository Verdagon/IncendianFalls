using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct DirtTTCCreateEffect : IDirtTTCEffect {
  public readonly int id;
  public DirtTTCCreateEffect(int id) {
    this.id = id;
  }
  int IDirtTTCEffect.id => id;
  public void visit(IDirtTTCEffectVisitor visitor) {
    visitor.visitDirtTTCCreateEffect(this);
  }
}

}
