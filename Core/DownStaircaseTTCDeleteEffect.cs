using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct DownStaircaseTTCDeleteEffect : IDownStaircaseTTCEffect {
  public readonly int id;
  public DownStaircaseTTCDeleteEffect(int id) {
    this.id = id;
  }
  int IDownStaircaseTTCEffect.id => id;
  public void visit(IDownStaircaseTTCEffectVisitor visitor) {
    visitor.visitDownStaircaseTTCDeleteEffect(this);
  }
}

}
