using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IMarkerTTCEffectVisitor {
  void visitMarkerTTCCreateEffect(MarkerTTCCreateEffect effect);
  void visitMarkerTTCDeleteEffect(MarkerTTCDeleteEffect effect);
}

}
