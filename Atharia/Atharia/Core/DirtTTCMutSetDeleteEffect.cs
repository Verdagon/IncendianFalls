using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct DirtTTCMutSetDeleteEffect : IDirtTTCMutSetEffect {
  public readonly int id;
  public DirtTTCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IDirtTTCMutSetEffect.id => id;
  public void visitIDirtTTCMutSetEffect(IDirtTTCMutSetEffectVisitor visitor) {
    visitor.visitDirtTTCMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitDirtTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
