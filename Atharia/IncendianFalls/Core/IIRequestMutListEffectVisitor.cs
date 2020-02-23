using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IIRequestMutListEffectVisitor {
  void visitIRequestMutListCreateEffect(IRequestMutListCreateEffect effect);
  void visitIRequestMutListDeleteEffect(IRequestMutListDeleteEffect effect);
  void visitIRequestMutListAddEffect(IRequestMutListAddEffect effect);
  void visitIRequestMutListRemoveEffect(IRequestMutListRemoveEffect effect);
}
         
}
