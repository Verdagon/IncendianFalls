using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IOnFireUCEffectVisitor {
  void visitOnFireUCCreateEffect(OnFireUCCreateEffect effect);
  void visitOnFireUCDeleteEffect(OnFireUCDeleteEffect effect);
}

}
