using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct DirtTTCDeleteEffect : IDirtTTCEffect {
  public readonly int id;
  public DirtTTCDeleteEffect(int id) {
    this.id = id;
  }
  int IDirtTTCEffect.id => id;
  public void visitIDirtTTCEffect(IDirtTTCEffectVisitor visitor) {
    visitor.visitDirtTTCDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitDirtTTCEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
