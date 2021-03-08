using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IIUnitComponentMutBunchEffectVisitor {
  void visitIUnitComponentMutBunchCreateEffect(IUnitComponentMutBunchCreateEffect effect);
  void visitIUnitComponentMutBunchDeleteEffect(IUnitComponentMutBunchDeleteEffect effect);
}

}
