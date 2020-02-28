using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IWatEffect {
  int id { get; }
  void visit(IWatEffectVisitor visitor);
}
       
}
