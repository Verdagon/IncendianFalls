using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class MarkerTTCIncarnation : IMarkerTTCEffectVisitor {
  public readonly string name;
  public MarkerTTCIncarnation(
      string name) {
    this.name = name;
  }
  public MarkerTTCIncarnation Copy() {
    return new MarkerTTCIncarnation(
name    );
  }

  public void visitMarkerTTCCreateEffect(MarkerTTCCreateEffect e) {}
  public void visitMarkerTTCDeleteEffect(MarkerTTCDeleteEffect e) {}

  public void ApplyEffect(IMarkerTTCEffect effect) { effect.visitIMarkerTTCEffect(this); }
}

}
