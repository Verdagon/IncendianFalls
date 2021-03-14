using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct LotusTTCCreateEffect : ILotusTTCEffect {
  public readonly int id;
  public readonly LotusTTCIncarnation incarnation;
  public LotusTTCCreateEffect(int id, LotusTTCIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int ILotusTTCEffect.id => id;
  public void visitILotusTTCEffect(ILotusTTCEffectVisitor visitor) {
    visitor.visitLotusTTCCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitLotusTTCEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
