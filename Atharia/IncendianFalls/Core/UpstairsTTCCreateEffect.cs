using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct UpstairsTTCCreateEffect : IUpstairsTTCEffect {
  public readonly int id;
  public UpstairsTTCCreateEffect(int id) {
    this.id = id;
  }
  int IUpstairsTTCEffect.id => id;
  public void visit(IUpstairsTTCEffectVisitor visitor) {
    visitor.visitUpstairsTTCCreateEffect(this);
  }
}

}
