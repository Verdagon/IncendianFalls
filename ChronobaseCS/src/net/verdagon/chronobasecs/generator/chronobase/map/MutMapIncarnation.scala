package net.verdagon.chronobasecs.generator.chronobase.map

import net.verdagon.chronobasecs.compiled._
import net.verdagon.chronobasecs.generator.chronobase.Generator.toCS
import net.verdagon.chronobasecs.generator.chronobase.ChronobaseOptions

object MutMapIncarnation {
  def generateIncarnation(opt: ChronobaseOptions, map: MapS): Map[String, String] = {
    val MapS(mapName, MutableS, keyType, elementType) = map

    val incarnationName = s"${mapName}Incarnation"
    val ieffectName = s"I${mapName}Effect"
    val observerName = s"I${mapName}EffectObserver"
    val visitorName = s"I${mapName}EffectVisitor"
    val createEffectName = s"${mapName}CreateEffect"
    val deleteEffectName = s"${mapName}DeleteEffect"
    val addEffectName = s"${mapName}AddEffect"
    val removeEffectName = s"${mapName}RemoveEffect"

    val keyCSType = toCS(keyType)
    val elementCSType = toCS(elementType)
    val flattenedKeyCSType = toCS(keyType.flatten)

    val incarnationDefinition =
      s"""
         |public class ${mapName}Incarnation {
         |  public readonly SortedDictionary<${flattenedKeyCSType}, int> elements;
         |
         |  public ${mapName}Incarnation(SortedDictionary<${flattenedKeyCSType}, int> elements) {
         |    this.elements = elements;
         |  }
         |
         |  public ${mapName}Incarnation Copy() {
         |    return new ${mapName}Incarnation(new SortedDictionary<${flattenedKeyCSType}, int>(elements));
         |  }
         |}
         """.stripMargin

    Map(incarnationName -> incarnationDefinition)
  }

}
