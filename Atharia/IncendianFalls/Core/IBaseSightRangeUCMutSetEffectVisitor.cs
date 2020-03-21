using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IBaseSightRangeUCMutSetEffectVisitor {
  void visitBaseSightRangeUCMutSetCreateEffect(BaseSightRangeUCMutSetCreateEffect effect);
  void visitBaseSightRangeUCMutSetDeleteEffect(BaseSightRangeUCMutSetDeleteEffect effect);
  void visitBaseSightRangeUCMutSetAddEffect(BaseSightRangeUCMutSetAddEffect effect);
  void visitBaseSightRangeUCMutSetRemoveEffect(BaseSightRangeUCMutSetRemoveEffect effect);
}
         
}
