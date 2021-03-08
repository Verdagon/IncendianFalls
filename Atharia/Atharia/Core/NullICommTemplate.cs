using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class NullICommTemplate : ICommTemplate {
  public static NullICommTemplate Null = new NullICommTemplate();
  public string DStr() { return "null"; }
  public int GetDeterministicHashCode() { return 0; }
  public void VisitICommTemplate(ICommTemplateVisitor visitor) { throw new Exception("Called method on a null!"); }
}

}
