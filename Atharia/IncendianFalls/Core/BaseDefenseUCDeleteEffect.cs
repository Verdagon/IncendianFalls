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
  public void visitIBaseDefenseUCEffect(IBaseDefenseUCEffectVisitor visitor) {
    visitor.visitBaseDefenseUCDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitBaseDefenseUCEffect(this);
  }
}

}
