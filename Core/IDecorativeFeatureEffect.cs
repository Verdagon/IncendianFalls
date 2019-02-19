using System;
using System.Collections.Generic;

namespace Atharia.Model {

public interface IDecorativeFeatureEffect {
  int id { get; }
  void visit(IDecorativeFeatureEffectVisitor visitor);
}
       
}
