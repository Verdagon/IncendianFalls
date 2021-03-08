using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IBaseDefenseUCEffectVisitor {
  void visitBaseDefenseUCCreateEffect(BaseDefenseUCCreateEffect effect);
  void visitBaseDefenseUCDeleteEffect(BaseDefenseUCDeleteEffect effect);
}

}
