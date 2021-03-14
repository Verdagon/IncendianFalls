using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IFlowerTTCMutSetEffectVisitor {
  void visitFlowerTTCMutSetCreateEffect(FlowerTTCMutSetCreateEffect effect);
  void visitFlowerTTCMutSetDeleteEffect(FlowerTTCMutSetDeleteEffect effect);
  void visitFlowerTTCMutSetAddEffect(FlowerTTCMutSetAddEffect effect);
  void visitFlowerTTCMutSetRemoveEffect(FlowerTTCMutSetRemoveEffect effect);
}
         
}
