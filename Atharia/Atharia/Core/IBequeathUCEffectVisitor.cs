using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IBequeathUCEffectVisitor {
  void visitBequeathUCCreateEffect(BequeathUCCreateEffect effect);
  void visitBequeathUCDeleteEffect(BequeathUCDeleteEffect effect);
}

}
