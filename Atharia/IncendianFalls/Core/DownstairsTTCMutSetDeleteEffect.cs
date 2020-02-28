using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct DownStairsTTCMutSetDeleteEffect : IDownStairsTTCMutSetEffect {
  public readonly int id;
  public DownStairsTTCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IDownStairsTTCMutSetEffect.id => id;
  public void visit(IDownStairsTTCMutSetEffectVisitor visitor) {
    visitor.visitDownStairsTTCMutSetDeleteEffect(this);
  }
}

}
