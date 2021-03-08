using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IIItemStrongMutBunchEffectVisitor {
  void visitIItemStrongMutBunchCreateEffect(IItemStrongMutBunchCreateEffect effect);
  void visitIItemStrongMutBunchDeleteEffect(IItemStrongMutBunchDeleteEffect effect);
}

}
