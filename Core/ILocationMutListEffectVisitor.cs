using System;
using System.Collections.Generic;

namespace Atharia.Model {

public interface ILocationMutListEffectVisitor {
  void visitLocationMutListCreateEffect(LocationMutListCreateEffect effect);
  void visitLocationMutListDeleteEffect(LocationMutListDeleteEffect effect);
  void visitLocationMutListAddEffect(LocationMutListAddEffect effect);
  void visitLocationMutListRemoveEffect(LocationMutListRemoveEffect effect);
}
         
}
