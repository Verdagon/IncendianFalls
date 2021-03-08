using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class ErrorCommTemplateAsICommTemplate : ICommTemplate {
  public readonly ErrorCommTemplate obj;
  public ErrorCommTemplateAsICommTemplate(ErrorCommTemplate obj) {
    this.obj = obj;
  }
  public string DStr() { return obj.DStr(); }
  public int GetDeterministicHashCode() { return obj.GetDeterministicHashCode(); }
  public override int GetHashCode() { return GetDeterministicHashCode(); }
       
  public void VisitICommTemplate(ICommTemplateVisitor visitor) { visitor.VisitICommTemplate(this); }
}
public static class ErrorCommTemplateAsICommTemplateCaster {
  public static ErrorCommTemplateAsICommTemplate AsICommTemplate(this ErrorCommTemplate obj) {
    return new ErrorCommTemplateAsICommTemplate(obj);
  }
}

}
