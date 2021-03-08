using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IMarkerTTCMutSetEffectVisitor {
  void visitMarkerTTCMutSetCreateEffect(MarkerTTCMutSetCreateEffect effect);
  void visitMarkerTTCMutSetDeleteEffect(MarkerTTCMutSetDeleteEffect effect);
  void visitMarkerTTCMutSetAddEffect(MarkerTTCMutSetAddEffect effect);
  void visitMarkerTTCMutSetRemoveEffect(MarkerTTCMutSetRemoveEffect effect);
}
         
}
