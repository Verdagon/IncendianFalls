using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IIGameEventMutListEffectVisitor {
  void visitIGameEventMutListCreateEffect(IGameEventMutListCreateEffect effect);
  void visitIGameEventMutListDeleteEffect(IGameEventMutListDeleteEffect effect);
  void visitIGameEventMutListAddEffect(IGameEventMutListAddEffect effect);
  void visitIGameEventMutListRemoveEffect(IGameEventMutListRemoveEffect effect);
}
         
}
