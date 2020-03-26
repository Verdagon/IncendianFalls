using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IWatEffect : IEffect {
  int id { get; }
  void visitIWatEffect(IWatEffectVisitor visitor);
}
       
}
