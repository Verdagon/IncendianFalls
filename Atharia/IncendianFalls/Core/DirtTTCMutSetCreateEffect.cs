using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct DirtTTCMutSetCreateEffect : IDirtTTCMutSetEffect {
  public readonly int id;
  public DirtTTCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IDirtTTCMutSetEffect.id => id;
  public void visitIDirtTTCMutSetEffect(IDirtTTCMutSetEffectVisitor visitor) {
    visitor.visitDirtTTCMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitDirtTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
