using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct MudTTCMutSetRemoveEffect : IMudTTCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public MudTTCMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IMudTTCMutSetEffect.id => id;
  public void visit(IMudTTCMutSetEffectVisitor visitor) {
    visitor.visitMudTTCMutSetRemoveEffect(this);
  }
}

}
