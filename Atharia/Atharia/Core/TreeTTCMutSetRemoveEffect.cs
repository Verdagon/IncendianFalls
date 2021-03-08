using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct TreeTTCMutSetRemoveEffect : ITreeTTCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public TreeTTCMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int ITreeTTCMutSetEffect.id => id;
  public void visitITreeTTCMutSetEffect(ITreeTTCMutSetEffectVisitor visitor) {
    visitor.visitTreeTTCMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitTreeTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
