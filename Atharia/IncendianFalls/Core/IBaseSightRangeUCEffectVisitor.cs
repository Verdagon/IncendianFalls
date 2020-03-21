using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IBaseSightRangeUCEffectVisitor {
  void visitBaseSightRangeUCCreateEffect(BaseSightRangeUCCreateEffect effect);
  void visitBaseSightRangeUCDeleteEffect(BaseSightRangeUCDeleteEffect effect);
}

}
