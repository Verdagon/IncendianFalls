using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct LotusTTCMutSetCreateEffect : ILotusTTCMutSetEffect {
  public readonly int id;
  public LotusTTCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int ILotusTTCMutSetEffect.id => id;
  public void visitILotusTTCMutSetEffect(ILotusTTCMutSetEffectVisitor visitor) {
    visitor.visitLotusTTCMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitLotusTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
