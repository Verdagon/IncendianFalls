using System;
using System.Collections;

using System.Collections.Generic;

namespace Geomancer.Model {

public interface IStrMutListEffectVisitor {
  void visitStrMutListCreateEffect(StrMutListCreateEffect effect);
  void visitStrMutListDeleteEffect(StrMutListDeleteEffect effect);
  void visitStrMutListAddEffect(StrMutListAddEffect effect);
  void visitStrMutListRemoveEffect(StrMutListRemoveEffect effect);
}
         
}
