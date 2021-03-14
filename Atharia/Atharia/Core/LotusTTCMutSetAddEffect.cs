using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct LotusTTCMutSetAddEffect : ILotusTTCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public LotusTTCMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int ILotusTTCMutSetEffect.id => id;
  public void visitILotusTTCMutSetEffect(ILotusTTCMutSetEffectVisitor visitor) {
    visitor.visitLotusTTCMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitLotusTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
