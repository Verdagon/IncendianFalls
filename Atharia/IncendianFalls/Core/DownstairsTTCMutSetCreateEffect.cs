using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct DownstairsTTCMutSetCreateEffect : IDownstairsTTCMutSetEffect {
  public readonly int id;
  public DownstairsTTCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IDownstairsTTCMutSetEffect.id => id;
  public void visit(IDownstairsTTCMutSetEffectVisitor visitor) {
    visitor.visitDownstairsTTCMutSetCreateEffect(this);
  }
}

}
