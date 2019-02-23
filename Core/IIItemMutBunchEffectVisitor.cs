using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IIItemMutBunchEffectVisitor {
  void visitIItemMutBunchCreateEffect(IItemMutBunchCreateEffect effect);
  void visitIItemMutBunchDeleteEffect(IItemMutBunchDeleteEffect effect);
}

}
