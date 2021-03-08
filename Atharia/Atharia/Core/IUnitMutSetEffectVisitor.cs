using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IUnitMutSetEffectVisitor {
  void visitUnitMutSetCreateEffect(UnitMutSetCreateEffect effect);
  void visitUnitMutSetDeleteEffect(UnitMutSetDeleteEffect effect);
  void visitUnitMutSetAddEffect(UnitMutSetAddEffect effect);
  void visitUnitMutSetRemoveEffect(UnitMutSetRemoveEffect effect);
}
         
}
