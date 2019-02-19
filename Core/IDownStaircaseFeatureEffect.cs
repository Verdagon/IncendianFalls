using System;
using System.Collections.Generic;

namespace Atharia.Model {

public interface IDownStaircaseFeatureEffect {
  int id { get; }
  void visit(IDownStaircaseFeatureEffectVisitor visitor);
}
       
}
