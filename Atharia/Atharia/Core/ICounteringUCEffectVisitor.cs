using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ICounteringUCEffectVisitor {
  void visitCounteringUCCreateEffect(CounteringUCCreateEffect effect);
  void visitCounteringUCDeleteEffect(CounteringUCDeleteEffect effect);
}

}
