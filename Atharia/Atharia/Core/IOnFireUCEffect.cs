using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IOnFireUCEffect : IEffect {
  int id { get; }
  void visitIOnFireUCEffect(IOnFireUCEffectVisitor visitor);
}
       
}
