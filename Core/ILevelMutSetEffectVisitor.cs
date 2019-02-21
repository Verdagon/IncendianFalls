using System;
using System.Collections.Generic;

namespace Atharia.Model {
public interface ILevelMutSetEffectVisitor {
  void visitLevelMutSetCreateEffect(LevelMutSetCreateEffect effect);
  void visitLevelMutSetDeleteEffect(LevelMutSetDeleteEffect effect);
  void visitLevelMutSetAddEffect(LevelMutSetAddEffect effect);
  void visitLevelMutSetRemoveEffect(LevelMutSetRemoveEffect effect);
}
         
}
