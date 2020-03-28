using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ICommTemplateVisitor {
  void VisitICommTemplate(DramaticCommTemplateAsICommTemplate obj);
  void VisitICommTemplate(NormalCommTemplateAsICommTemplate obj);
  void VisitICommTemplate(DialogueCommTemplateAsICommTemplate obj);
  void VisitICommTemplate(AsideCommTemplateAsICommTemplate obj);
}

}
