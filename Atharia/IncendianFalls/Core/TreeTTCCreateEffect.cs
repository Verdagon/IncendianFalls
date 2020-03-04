using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct TreeTTCCreateEffect : ITreeTTCEffect {
  public readonly int id;
  public TreeTTCCreateEffect(int id) {
    this.id = id;
  }
  int ITreeTTCEffect.id => id;
  public void visit(ITreeTTCEffectVisitor visitor) {
    visitor.visitTreeTTCCreateEffect(this);
  }
}

}
