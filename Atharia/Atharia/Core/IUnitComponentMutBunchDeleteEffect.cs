using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct IUnitComponentMutBunchDeleteEffect : IIUnitComponentMutBunchEffect {
  public readonly int id;
  public IUnitComponentMutBunchDeleteEffect(int id) {
    this.id = id;
  }
  int IIUnitComponentMutBunchEffect.id => id;
  public void visitIIUnitComponentMutBunchEffect(IIUnitComponentMutBunchEffectVisitor visitor) {
    visitor.visitIUnitComponentMutBunchDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitIUnitComponentMutBunchEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
