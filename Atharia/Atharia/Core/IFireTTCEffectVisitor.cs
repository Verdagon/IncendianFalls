using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IFireTTCEffectVisitor {
  void visitFireTTCCreateEffect(FireTTCCreateEffect effect);
  void visitFireTTCDeleteEffect(FireTTCDeleteEffect effect);
}

}
