using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public static class ICommTemplateParser {
  public static ICommTemplate Parse(ParseSource source) {
    var nextThingPeek = source.PeekNextWord();
    switch (nextThingPeek) {
      case "DramaticCommTemplate":
        return new DramaticCommTemplateAsICommTemplate(DramaticCommTemplate.Parse(source));
      case "NormalCommTemplate":
        return new NormalCommTemplateAsICommTemplate(NormalCommTemplate.Parse(source));
      case "DialogueCommTemplate":
        return new DialogueCommTemplateAsICommTemplate(DialogueCommTemplate.Parse(source));
      case "AsideCommTemplate":
        return new AsideCommTemplateAsICommTemplate(AsideCommTemplate.Parse(source));
      default:
        throw new Exception("Unexpected: " + nextThingPeek);
    }
  }
}

}
