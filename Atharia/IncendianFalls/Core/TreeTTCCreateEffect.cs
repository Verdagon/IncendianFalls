using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct TreeTTCCreateEffect : ITreeTTCEffect {
  public readonly int id;
  public readonly TreeTTCIncarnation incarnation;
  public TreeTTCCreateEffect(int id, TreeTTCIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int ITreeTTCEffect.id => id;
  public void visitITreeTTCEffect(ITreeTTCEffectVisitor visitor) {
    visitor.visitTreeTTCCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitTreeTTCEffect(this);
  }
}

}
