using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class InstructionsCommTemplateAsICommTemplate : ICommTemplate {
  public readonly InstructionsCommTemplate obj;
  public InstructionsCommTemplateAsICommTemplate(InstructionsCommTemplate obj) {
    this.obj = obj;
  }
  public string DStr() { return obj.DStr(); }
  public int GetDeterministicHashCode() { return obj.GetDeterministicHashCode(); }
  public override int GetHashCode() { return GetDeterministicHashCode(); }
       
  public void VisitICommTemplate(ICommTemplateVisitor visitor) { visitor.VisitICommTemplate(this); }
}
public static class InstructionsCommTemplateAsICommTemplateCaster {
  public static InstructionsCommTemplateAsICommTemplate AsICommTemplate(this InstructionsCommTemplate obj) {
    return new InstructionsCommTemplateAsICommTemplate(obj);
  }
}

}
