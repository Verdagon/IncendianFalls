using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IFlowerTTCEffectVisitor {
  void visitFlowerTTCCreateEffect(FlowerTTCCreateEffect effect);
  void visitFlowerTTCDeleteEffect(FlowerTTCDeleteEffect effect);
}

}
