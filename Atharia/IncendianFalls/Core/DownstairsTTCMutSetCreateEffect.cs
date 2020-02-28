using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct DownStairsTTCMutSetCreateEffect : IDownStairsTTCMutSetEffect {
  public readonly int id;
  public DownStairsTTCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IDownStairsTTCMutSetEffect.id => id;
  public void visit(IDownStairsTTCMutSetEffectVisitor visitor) {
    visitor.visitDownStairsTTCMutSetCreateEffect(this);
  }
}

}
