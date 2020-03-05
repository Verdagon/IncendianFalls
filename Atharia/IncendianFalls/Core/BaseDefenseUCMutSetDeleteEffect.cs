using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct BaseDefenseUCMutSetDeleteEffect : IBaseDefenseUCMutSetEffect {
  public readonly int id;
  public BaseDefenseUCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IBaseDefenseUCMutSetEffect.id => id;
  public void visit(IBaseDefenseUCMutSetEffectVisitor visitor) {
    visitor.visitBaseDefenseUCMutSetDeleteEffect(this);
  }
}

}
