using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct MudTTCCreateEffect : IMudTTCEffect {
  public readonly int id;
  public MudTTCCreateEffect(int id) {
    this.id = id;
  }
  int IMudTTCEffect.id => id;
  public void visit(IMudTTCEffectVisitor visitor) {
    visitor.visitMudTTCCreateEffect(this);
  }
}

}
