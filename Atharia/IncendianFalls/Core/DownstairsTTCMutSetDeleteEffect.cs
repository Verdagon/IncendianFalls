using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct DownstairsTTCMutSetDeleteEffect : IDownstairsTTCMutSetEffect {
  public readonly int id;
  public DownstairsTTCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IDownstairsTTCMutSetEffect.id => id;
  public void visit(IDownstairsTTCMutSetEffectVisitor visitor) {
    visitor.visitDownstairsTTCMutSetDeleteEffect(this);
  }
}

}
