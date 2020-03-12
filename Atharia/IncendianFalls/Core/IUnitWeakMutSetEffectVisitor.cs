using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IUnitWeakMutSetEffectVisitor {
  void visitUnitWeakMutSetCreateEffect(UnitWeakMutSetCreateEffect effect);
  void visitUnitWeakMutSetDeleteEffect(UnitWeakMutSetDeleteEffect effect);
  void visitUnitWeakMutSetAddEffect(UnitWeakMutSetAddEffect effect);
  void visitUnitWeakMutSetRemoveEffect(UnitWeakMutSetRemoveEffect effect);
}
         
}
