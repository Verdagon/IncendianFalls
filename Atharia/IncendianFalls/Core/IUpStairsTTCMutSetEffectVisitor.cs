using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IUpStairsTTCMutSetEffectVisitor {
  void visitUpStairsTTCMutSetCreateEffect(UpStairsTTCMutSetCreateEffect effect);
  void visitUpStairsTTCMutSetDeleteEffect(UpStairsTTCMutSetDeleteEffect effect);
  void visitUpStairsTTCMutSetAddEffect(UpStairsTTCMutSetAddEffect effect);
  void visitUpStairsTTCMutSetRemoveEffect(UpStairsTTCMutSetRemoveEffect effect);
}
         
}
