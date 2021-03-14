using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ILeafTTCEffectVisitor {
  void visitLeafTTCCreateEffect(LeafTTCCreateEffect effect);
  void visitLeafTTCDeleteEffect(LeafTTCDeleteEffect effect);
}

}
