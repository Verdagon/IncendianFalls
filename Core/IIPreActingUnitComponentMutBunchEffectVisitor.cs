using System;
using System.Collections.Generic;

namespace Atharia.Model {
public interface IIPreActingUnitComponentMutBunchEffectVisitor {
  void visitIPreActingUnitComponentMutBunchCreateEffect(IPreActingUnitComponentMutBunchCreateEffect effect);
  void visitIPreActingUnitComponentMutBunchDeleteEffect(IPreActingUnitComponentMutBunchDeleteEffect effect);
}

}
