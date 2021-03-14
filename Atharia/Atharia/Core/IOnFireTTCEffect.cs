using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IOnFireTTCEffect : IEffect {
  int id { get; }
  void visitIOnFireTTCEffect(IOnFireTTCEffectVisitor visitor);
}
       
}
