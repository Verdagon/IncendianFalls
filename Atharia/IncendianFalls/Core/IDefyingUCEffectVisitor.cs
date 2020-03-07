using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IDefyingUCEffectVisitor {
  void visitDefyingUCCreateEffect(DefyingUCCreateEffect effect);
  void visitDefyingUCDeleteEffect(DefyingUCDeleteEffect effect);
}

}
