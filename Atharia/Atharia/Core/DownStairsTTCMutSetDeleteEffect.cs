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
  public void visitIDownStairsTTCMutSetEffect(IDownStairsTTCMutSetEffectVisitor visitor) {
    visitor.visitDownStairsTTCMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitDownStairsTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
