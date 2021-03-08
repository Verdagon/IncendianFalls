using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct MudTTCMutSetRemoveEffect : IMudTTCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public MudTTCMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IMudTTCMutSetEffect.id => id;
  public void visitIMudTTCMutSetEffect(IMudTTCMutSetEffectVisitor visitor) {
    visitor.visitMudTTCMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitMudTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
