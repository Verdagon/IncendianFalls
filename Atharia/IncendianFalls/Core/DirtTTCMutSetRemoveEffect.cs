using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct DirtTTCMutSetRemoveEffect : IDirtTTCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public DirtTTCMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IDirtTTCMutSetEffect.id => id;
  public void visitIDirtTTCMutSetEffect(IDirtTTCMutSetEffectVisitor visitor) {
    visitor.visitDirtTTCMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitDirtTTCMutSetEffect(this);
  }
}

}
