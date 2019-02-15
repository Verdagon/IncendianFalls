using System;
using System.Collections.Generic;

namespace Atharia.Model {

public interface IIDetailMutListEffectVisitor {
  void visitIDetailMutListCreateEffect(IDetailMutListCreateEffect effect);
  void visitIDetailMutListDeleteEffect(IDetailMutListDeleteEffect effect);
  void visitIDetailMutListAddEffect(IDetailMutListAddEffect effect);
  void visitIDetailMutListRemoveEffect(IDetailMutListRemoveEffect effect);
}
         
}
