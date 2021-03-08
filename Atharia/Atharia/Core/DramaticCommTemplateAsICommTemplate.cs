using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class DramaticCommTemplateAsICommTemplate : ICommTemplate {
  public readonly DramaticCommTemplate obj;
  public DramaticCommTemplateAsICommTemplate(DramaticCommTemplate obj) {
    this.obj = obj;
  }
  public string DStr() { return obj.DStr(); }
  public int GetDeterministicHashCode() { return obj.GetDeterministicHashCode(); }
  public override int GetHashCode() { return GetDeterministicHashCode(); }
       
  public void VisitICommTemplate(ICommTemplateVisitor visitor) { visitor.VisitICommTemplate(this); }
}
public static class DramaticCommTemplateAsICommTemplateCaster {
  public static DramaticCommTemplateAsICommTemplate AsICommTemplate(this DramaticCommTemplate obj) {
    return new DramaticCommTemplateAsICommTemplate(obj);
  }
}

}
