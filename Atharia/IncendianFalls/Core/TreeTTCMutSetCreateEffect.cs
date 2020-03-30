using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct TreeTTCMutSetCreateEffect : ITreeTTCMutSetEffect {
  public readonly int id;
  public TreeTTCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int ITreeTTCMutSetEffect.id => id;
  public void visitITreeTTCMutSetEffect(ITreeTTCMutSetEffectVisitor visitor) {
    visitor.visitTreeTTCMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitTreeTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
