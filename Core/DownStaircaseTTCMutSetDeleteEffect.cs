using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct DownStaircaseTTCMutSetDeleteEffect : IDownStaircaseTTCMutSetEffect {
  public readonly int id;
  public DownStaircaseTTCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IDownStaircaseTTCMutSetEffect.id => id;
  public void visit(IDownStaircaseTTCMutSetEffectVisitor visitor) {
    visitor.visitDownStaircaseTTCMutSetDeleteEffect(this);
  }
}

}
