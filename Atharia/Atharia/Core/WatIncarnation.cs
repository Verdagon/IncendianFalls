using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class WatIncarnation : IWatEffectVisitor {
  public readonly int items;
  public readonly int impulses;
  public readonly int blah;
  public readonly int bloop;
  public WatIncarnation(
      int items,
      int impulses,
      int blah,
      int bloop) {
    this.items = items;
    this.impulses = impulses;
    this.blah = blah;
    this.bloop = bloop;
  }
  public WatIncarnation Copy() {
    return new WatIncarnation(
items,
impulses,
blah,
bloop    );
  }

  public void visitWatCreateEffect(WatCreateEffect e) {}
  public void visitWatDeleteEffect(WatDeleteEffect e) {}




  public void ApplyEffect(IWatEffect effect) { effect.visitIWatEffect(this); }
}

}
