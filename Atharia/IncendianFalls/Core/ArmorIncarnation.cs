using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class ArmorIncarnation : IArmorEffectVisitor {
  public ArmorIncarnation(
) {
  }
  public ArmorIncarnation Copy() {
    return new ArmorIncarnation(
    );
  }

  public void visitArmorCreateEffect(ArmorCreateEffect e) {}
  public void visitArmorDeleteEffect(ArmorDeleteEffect e) {}

  public void ApplyEffect(IArmorEffect effect) { effect.visitIArmorEffect(this); }
}

}
