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
  public void visit(IDirtTTCMutSetEffectVisitor visitor) {
    visitor.visitDirtTTCMutSetCreateEffect(this);
  }
}

}
