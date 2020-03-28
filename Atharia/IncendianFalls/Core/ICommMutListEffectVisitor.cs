using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ICommMutListEffectVisitor {
  void visitCommMutListCreateEffect(CommMutListCreateEffect effect);
  void visitCommMutListDeleteEffect(CommMutListDeleteEffect effect);
  void visitCommMutListAddEffect(CommMutListAddEffect effect);
  void visitCommMutListRemoveEffect(CommMutListRemoveEffect effect);
}
         
}
