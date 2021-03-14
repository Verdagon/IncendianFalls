using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ILotusTTCEffect : IEffect {
  int id { get; }
  void visitILotusTTCEffect(ILotusTTCEffectVisitor visitor);
}
       
}
