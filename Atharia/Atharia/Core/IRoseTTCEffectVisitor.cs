using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IRoseTTCEffectVisitor {
  void visitRoseTTCCreateEffect(RoseTTCCreateEffect effect);
  void visitRoseTTCDeleteEffect(RoseTTCDeleteEffect effect);
}

}
