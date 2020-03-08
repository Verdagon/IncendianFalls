using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ISlowRodEffectVisitor {
  void visitSlowRodCreateEffect(SlowRodCreateEffect effect);
  void visitSlowRodDeleteEffect(SlowRodDeleteEffect effect);
}

}
