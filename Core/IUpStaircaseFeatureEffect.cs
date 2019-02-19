using System;
using System.Collections.Generic;

namespace Atharia.Model {

public interface IUpStaircaseFeatureEffect {
  int id { get; }
  void visit(IUpStaircaseFeatureEffectVisitor visitor);
}
       
}
