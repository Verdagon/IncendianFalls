using System;
using System.Collections.Generic;

namespace Atharia.Model {

public interface IIItemMutBunchEffectVisitor {
  void visitIItemMutBunchCreateEffect(IItemMutBunchCreateEffect effect);
  void visitIItemMutBunchDeleteEffect(IItemMutBunchDeleteEffect effect);
  void visitIItemMutBunchAddEffect(IItemMutBunchAddEffect effect);
  void visitIItemMutBunchRemoveEffect(IItemMutBunchRemoveEffect effect);
}
         
}
