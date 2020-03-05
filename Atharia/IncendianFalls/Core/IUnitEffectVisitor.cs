using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IUnitEffectVisitor {
  void visitUnitCreateEffect(UnitCreateEffect effect);
  void visitUnitDeleteEffect(UnitDeleteEffect effect);
  void visitUnitSetAliveEffect(UnitSetAliveEffect effect);
  void visitUnitSetLifeEndTimeEffect(UnitSetLifeEndTimeEffect effect);
  void visitUnitSetLocationEffect(UnitSetLocationEffect effect);
  void visitUnitSetNextActionTimeEffect(UnitSetNextActionTimeEffect effect);
  void visitUnitSetHpEffect(UnitSetHpEffect effect);
}

}
