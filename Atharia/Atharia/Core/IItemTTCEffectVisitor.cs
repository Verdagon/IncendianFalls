using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IItemTTCEffectVisitor {
  void visitItemTTCCreateEffect(ItemTTCCreateEffect effect);
  void visitItemTTCDeleteEffect(ItemTTCDeleteEffect effect);
}

}
