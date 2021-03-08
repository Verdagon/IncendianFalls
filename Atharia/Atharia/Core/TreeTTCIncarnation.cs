using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class TreeTTCIncarnation : ITreeTTCEffectVisitor {
  public TreeTTCIncarnation(
) {
  }
  public TreeTTCIncarnation Copy() {
    return new TreeTTCIncarnation(
    );
  }

  public void visitTreeTTCCreateEffect(TreeTTCCreateEffect e) {}
  public void visitTreeTTCDeleteEffect(TreeTTCDeleteEffect e) {}

  public void ApplyEffect(ITreeTTCEffect effect) { effect.visitITreeTTCEffect(this); }
}

}
