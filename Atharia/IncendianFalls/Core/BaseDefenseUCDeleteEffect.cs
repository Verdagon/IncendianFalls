using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct BaseDefenseUCDeleteEffect : IBaseDefenseUCEffect {
  public readonly int id;
  public BaseDefenseUCDeleteEffect(int id) {
    this.id = id;
  }
  int IBaseDefenseUCEffect.id => id;
  public void visit(IBaseDefenseUCEffectVisitor visitor) {
    visitor.visitBaseDefenseUCDeleteEffect(this);
  }
}

}
