using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct LotusTTCMutSetRemoveEffect : ILotusTTCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public LotusTTCMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int ILotusTTCMutSetEffect.id => id;
  public void visitILotusTTCMutSetEffect(ILotusTTCMutSetEffectVisitor visitor) {
    visitor.visitLotusTTCMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitLotusTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
