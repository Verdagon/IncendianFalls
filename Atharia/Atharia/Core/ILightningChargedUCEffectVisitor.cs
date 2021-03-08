using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ILightningChargedUCEffectVisitor {
  void visitLightningChargedUCCreateEffect(LightningChargedUCCreateEffect effect);
  void visitLightningChargedUCDeleteEffect(LightningChargedUCDeleteEffect effect);
}

}
