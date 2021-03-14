using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IEvolvifyImpulseEffect : IEffect {
  int id { get; }
  void visitIEvolvifyImpulseEffect(IEvolvifyImpulseEffectVisitor visitor);
}
       
}
