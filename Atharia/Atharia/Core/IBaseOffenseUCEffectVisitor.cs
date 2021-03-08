using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IBaseOffenseUCEffectVisitor {
  void visitBaseOffenseUCCreateEffect(BaseOffenseUCCreateEffect effect);
  void visitBaseOffenseUCDeleteEffect(BaseOffenseUCDeleteEffect effect);
}

}
