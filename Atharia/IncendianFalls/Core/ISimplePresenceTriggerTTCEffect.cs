using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ISimplePresenceTriggerTTCEffect : IEffect {
  int id { get; }
  void visitISimplePresenceTriggerTTCEffect(ISimplePresenceTriggerTTCEffectVisitor visitor);
}
       
}
