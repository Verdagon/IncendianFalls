using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IBloodTTCEffectVisitor {
  void visitBloodTTCCreateEffect(BloodTTCCreateEffect effect);
  void visitBloodTTCDeleteEffect(BloodTTCDeleteEffect effect);
}

}
