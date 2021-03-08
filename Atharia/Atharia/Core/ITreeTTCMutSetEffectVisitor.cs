using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ITreeTTCMutSetEffectVisitor {
  void visitTreeTTCMutSetCreateEffect(TreeTTCMutSetCreateEffect effect);
  void visitTreeTTCMutSetDeleteEffect(TreeTTCMutSetDeleteEffect effect);
  void visitTreeTTCMutSetAddEffect(TreeTTCMutSetAddEffect effect);
  void visitTreeTTCMutSetRemoveEffect(TreeTTCMutSetRemoveEffect effect);
}
         
}
