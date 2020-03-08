using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IIImpulseStrongMutBunchEffectVisitor {
  void visitIImpulseStrongMutBunchCreateEffect(IImpulseStrongMutBunchCreateEffect effect);
  void visitIImpulseStrongMutBunchDeleteEffect(IImpulseStrongMutBunchDeleteEffect effect);
}

}
