using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IArmorEffectVisitor {
  void visitArmorCreateEffect(ArmorCreateEffect effect);
  void visitArmorDeleteEffect(ArmorDeleteEffect effect);
}

}
