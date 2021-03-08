using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ISimplePresenceTriggerTTCMutSetEffect : IEffect {
  int id { get; }
  void visitISimplePresenceTriggerTTCMutSetEffect(ISimplePresenceTriggerTTCMutSetEffectVisitor visitor);
}

}
