using System;
using System.Collections.Generic;

namespace Atharia.Model {

public interface IUnitMutBunchEffectVisitor {
  void visitUnitMutBunchCreateEffect(UnitMutBunchCreateEffect effect);
  void visitUnitMutBunchDeleteEffect(UnitMutBunchDeleteEffect effect);
  void visitUnitMutBunchAddEffect(UnitMutBunchAddEffect effect);
  void visitUnitMutBunchRemoveEffect(UnitMutBunchRemoveEffect effect);
}
         
}
