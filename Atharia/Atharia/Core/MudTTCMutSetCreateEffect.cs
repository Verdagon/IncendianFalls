using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct MudTTCMutSetCreateEffect : IMudTTCMutSetEffect {
  public readonly int id;
  public MudTTCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IMudTTCMutSetEffect.id => id;
  public void visitIMudTTCMutSetEffect(IMudTTCMutSetEffectVisitor visitor) {
    visitor.visitMudTTCMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitMudTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
