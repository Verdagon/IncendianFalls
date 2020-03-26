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

    val flattenedElementCSType = toCS(elementType.flatten)

    val incarnationDefinition =
      s"""
         |public class ${listName}Incarnation {
         |  public readonly List<${flattenedElementCSType}> list;
         |
         |  public ${listName}Incarnation(List<${flattenedElementCSType}> list) {
         |    this.list = list;
         |  }
         |
         |  public ${listName}Incarnation Copy() {
         |    return new ${listName}Incarnation(new List<${flattenedElementCSType}>(list));
         |  }
         |}
         """.stripMargin

    Map(incarnationName -> incarnationDefinition)
  }
}
