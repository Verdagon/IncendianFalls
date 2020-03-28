using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class DialogueCommTemplateAsICommTemplate : ICommTemplate {
  public readonly DialogueCommTemplate obj;
  public DialogueCommTemplateAsICommTemplate(DialogueCommTemplate obj) {
    this.obj = obj;
  }
  public string DStr() { return obj.DStr(); }
  public int GetDeterministicHashCode() { return obj.GetDeterministicHashCode(); }
  public override int GetHashCode() { return GetDeterministicHashCode(); }
       
  public void VisitICommTemplate(ICommTemplateVisitor visitor) { visitor.VisitICommTemplate(this); }
}
public static class DialogueCommTemplateAsICommTemplateCaster {
  public static DialogueCommTemplateAsICommTemplate AsICommTemplate(this DialogueCommTemplate obj) {
    return new DialogueCommTemplateAsICommTemplate(obj);
  }
}

}
