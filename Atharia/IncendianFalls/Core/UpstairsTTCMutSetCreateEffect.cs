using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct UpstairsTTCMutSetCreateEffect : IUpstairsTTCMutSetEffect {
  public readonly int id;
  public UpstairsTTCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IUpstairsTTCMutSetEffect.id => id;
  public void visit(IUpstairsTTCMutSetEffectVisitor visitor) {
    visitor.visitUpstairsTTCMutSetCreateEffect(this);
  }
}

}
