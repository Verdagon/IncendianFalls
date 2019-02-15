using System;
using System.Collections.Generic;

namespace Atharia.Model {

public interface ILevelMutBunchEffectVisitor {
  void visitLevelMutBunchCreateEffect(LevelMutBunchCreateEffect effect);
  void visitLevelMutBunchDeleteEffect(LevelMutBunchDeleteEffect effect);
  void visitLevelMutBunchAddEffect(LevelMutBunchAddEffect effect);
  void visitLevelMutBunchRemoveEffect(LevelMutBunchRemoveEffect effect);
}
         
}
