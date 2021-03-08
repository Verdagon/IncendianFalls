using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class AsideCommTemplateAsICommTemplate : ICommTemplate {
  public readonly AsideCommTemplate obj;
  public AsideCommTemplateAsICommTemplate(AsideCommTemplate obj) {
    this.obj = obj;
  }
  public string DStr() { return obj.DStr(); }
  public int GetDeterministicHashCode() { return obj.GetDeterministicHashCode(); }
  public override int GetHashCode() { return GetDeterministicHashCode(); }
       
  public void VisitICommTemplate(ICommTemplateVisitor visitor) { visitor.VisitICommTemplate(this); }
}
public static class AsideCommTemplateAsICommTemplateCaster {
  public static AsideCommTemplateAsICommTemplate AsICommTemplate(this AsideCommTemplate obj) {
    return new AsideCommTemplateAsICommTemplate(obj);
  }
}

}
