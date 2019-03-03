using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IIUnitEventMutListEffectVisitor {
  void visitIUnitEventMutListCreateEffect(IUnitEventMutListCreateEffect effect);
  void visitIUnitEventMutListDeleteEffect(IUnitEventMutListDeleteEffect effect);
  void visitIUnitEventMutListAddEffect(IUnitEventMutListAddEffect effect);
  void visitIUnitEventMutListRemoveEffect(IUnitEventMutListRemoveEffect effect);
}
         
}
