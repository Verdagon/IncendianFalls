using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct RocksTTCMutSetCreateEffect : IRocksTTCMutSetEffect {
  public readonly int id;
  public RocksTTCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IRocksTTCMutSetEffect.id => id;
  public void visitIRocksTTCMutSetEffect(IRocksTTCMutSetEffectVisitor visitor) {
    visitor.visitRocksTTCMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitRocksTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
