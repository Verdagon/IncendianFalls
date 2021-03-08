using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IMiredUCEffectVisitor {
  void visitMiredUCCreateEffect(MiredUCCreateEffect effect);
  void visitMiredUCDeleteEffect(MiredUCDeleteEffect effect);
}

}
