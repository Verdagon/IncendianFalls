using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class NormalCommTemplateAsICommTemplate : ICommTemplate {
  public readonly NormalCommTemplate obj;
  public NormalCommTemplateAsICommTemplate(NormalCommTemplate obj) {
    this.obj = obj;
  }
  public string DStr() { return obj.DStr(); }
  public int GetDeterministicHashCode() { return obj.GetDeterministicHashCode(); }
  public override int GetHashCode() { return GetDeterministicHashCode(); }
       
  public void VisitICommTemplate(ICommTemplateVisitor visitor) { visitor.VisitICommTemplate(this); }
}
public static class NormalCommTemplateAsICommTemplateCaster {
  public static NormalCommTemplateAsICommTemplate AsICommTemplate(this NormalCommTemplate obj) {
    return new NormalCommTemplateAsICommTemplate(obj);
  }
}

}
