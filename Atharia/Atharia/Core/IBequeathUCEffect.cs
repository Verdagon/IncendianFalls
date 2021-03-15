using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IBequeathUCEffect : IEffect {
  int id { get; }
  void visitIBequeathUCEffect(IBequeathUCEffectVisitor visitor);
}
       
}
