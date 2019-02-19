using System;
using System.Collections.Generic;

namespace Atharia.Model {

public interface IIFeatureMutListEffectVisitor {
  void visitIFeatureMutListCreateEffect(IFeatureMutListCreateEffect effect);
  void visitIFeatureMutListDeleteEffect(IFeatureMutListDeleteEffect effect);
  void visitIFeatureMutListAddEffect(IFeatureMutListAddEffect effect);
  void visitIFeatureMutListRemoveEffect(IFeatureMutListRemoveEffect effect);
}
         
}
