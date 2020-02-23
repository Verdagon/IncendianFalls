package net.verdagon.chronobasecs.generator.chronobase.list

import net.verdagon.chronobasecs.compiled.{ImmutableS, ListS, MutableS, OwnS}
import net.verdagon.chronobasecs.generator.chronobase.Generator.toCS
import net.verdagon.chronobasecs.generator.chronobase.ChronobaseOptions

object MutListIncarnation {

  def generateIncarnation(
                           opt: ChronobaseOptions,
                           list: ListS
  ): Map[String, String] = {
    val ListS(listName, MutableS, elementType) = list

    val incarnationName = s"${listName}Incarnation"
    val ieffectName = s"I${listName}Effect"
    val observerName = s"I${listName}EffectObserver"
    val visitorName = s"I${listName}EffectVisitor"
    val createEffectName = s"${listName}CreateEffect"
    val deleteEffectName = s"${listName}DeleteEffect"
    val addEffectName = s"${listName}AddEffect"
    val removeEffectName = s"${listName}RemoveEffect"

    val flattenedElementCSType = toCS(elementType.flatten)
    val elementCSType = toCS(elementType)

    val incarnationDefinition =
      s"""
         |public class ${listName}Incarnation {
         |  public readonly List<${flattenedElementCSType}> list;
         |
         |  public ${listName}Incarnation(List<${flattenedElementCSType}> list) {
         |    this.list = list;
         |  }
         |}
         """.stripMargin

    Map(incarnationName -> incarnationDefinition)
  }
}
