using System;
using System.Collections;

using System.Collections.Generic;

namespace Geomancer.Model {

public struct RandDeleteEffect : IRandEffect {
  public readonly int id;
  public RandDeleteEffect(int id) {
    this.id = id;
  }
  int IRandEffect.id => id;
  public void visitIRandEffect(IRandEffectVisitor visitor) {
    visitor.visitRandDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitRandEffect(this);
  }
}

}
