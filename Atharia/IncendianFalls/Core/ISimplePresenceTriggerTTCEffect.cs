using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ISimplePresenceTriggerTTCEffect {
  int id { get; }
  void visit(ISimplePresenceTriggerTTCEffectVisitor visitor);
}
       
}
