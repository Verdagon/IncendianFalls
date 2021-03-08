using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ICommTemplate {
  string DStr();
  int GetDeterministicHashCode();
  void VisitICommTemplate(ICommTemplateVisitor visitor);
}

}
