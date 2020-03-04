using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct MudTTCDeleteEffect : IMudTTCEffect {
  public readonly int id;
  public MudTTCDeleteEffect(int id) {
    this.id = id;
  }
  int IMudTTCEffect.id => id;
  public void visit(IMudTTCEffectVisitor visitor) {
    visitor.visitMudTTCDeleteEffect(this);
  }
}

}