using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct IPreActingUCWeakMutBunchDeleteEffect : IIPreActingUCWeakMutBunchEffect {
  public readonly int id;
  public IPreActingUCWeakMutBunchDeleteEffect(int id) {
    this.id = id;
  }
  int IIPreActingUCWeakMutBunchEffect.id => id;
  public void visitIIPreActingUCWeakMutBunchEffect(IIPreActingUCWeakMutBunchEffectVisitor visitor) {
    visitor.visitIPreActingUCWeakMutBunchDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitIPreActingUCWeakMutBunchEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
