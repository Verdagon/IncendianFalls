using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ITimeAnchorTTCMutSetEffectVisitor {
  void visitTimeAnchorTTCMutSetCreateEffect(TimeAnchorTTCMutSetCreateEffect effect);
  void visitTimeAnchorTTCMutSetDeleteEffect(TimeAnchorTTCMutSetDeleteEffect effect);
  void visitTimeAnchorTTCMutSetAddEffect(TimeAnchorTTCMutSetAddEffect effect);
  void visitTimeAnchorTTCMutSetRemoveEffect(TimeAnchorTTCMutSetRemoveEffect effect);
}
         
}
