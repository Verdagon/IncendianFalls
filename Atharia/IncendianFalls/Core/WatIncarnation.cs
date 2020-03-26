using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class WatIncarnation : IWatEffectVisitor {
  public readonly int items;
  public readonly int impulses;
  public WatIncarnation(
      int items,
      int impulses) {
    this.items = items;
    this.impulses = impulses;
  }
  public WatIncarnation Copy() {
    return new WatIncarnation(
items,
impulses    );
  }

  public void visitWatCreateEffect(WatCreateEffect e) {}
  public void visitWatDeleteEffect(WatDeleteEffect e) {}


  public void ApplyEffect(IWatEffect effect) { effect.visitIWatEffect(this); }
}

}
