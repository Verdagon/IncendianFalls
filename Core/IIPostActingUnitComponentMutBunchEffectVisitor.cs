using System;
using System.Collections.Generic;

namespace Atharia.Model {
public interface IIPostActingUnitComponentMutBunchEffectVisitor {
  void visitIPostActingUnitComponentMutBunchCreateEffect(IPostActingUnitComponentMutBunchCreateEffect effect);
  void visitIPostActingUnitComponentMutBunchDeleteEffect(IPostActingUnitComponentMutBunchDeleteEffect effect);
}

}
