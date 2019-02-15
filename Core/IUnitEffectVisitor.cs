using System;
using System.Collections.Generic;

namespace Atharia.Model {
public interface IUnitEffectVisitor {
  void visitUnitCreateEffect(UnitCreateEffect effect);
  void visitUnitDeleteEffect(UnitDeleteEffect effect);
  void visitUnitSetAliveEffect(UnitSetAliveEffect effect);
  void visitUnitSetLifeEndTimeEffect(UnitSetLifeEndTimeEffect effect);
  void visitUnitSetLocationEffect(UnitSetLocationEffect effect);
  void visitUnitSetHpEffect(UnitSetHpEffect effect);
  void visitUnitSetMpEffect(UnitSetMpEffect effect);
  void visitUnitSetNextActionTimeEffect(UnitSetNextActionTimeEffect effect);
  void visitUnitSetDirectiveEffect(UnitSetDirectiveEffect effect);
}

}
