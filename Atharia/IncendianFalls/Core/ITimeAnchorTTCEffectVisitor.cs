using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ITimeAnchorTTCEffectVisitor {
  void visitTimeAnchorTTCCreateEffect(TimeAnchorTTCCreateEffect effect);
  void visitTimeAnchorTTCDeleteEffect(TimeAnchorTTCDeleteEffect effect);
}

}
