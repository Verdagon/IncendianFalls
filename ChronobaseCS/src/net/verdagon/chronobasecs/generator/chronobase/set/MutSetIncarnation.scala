package net.verdagon.chronobasecs.generator.chronobase.set

import net.verdagon.chronobasecs.compiled.{ImmutableS, MutableS, OwnS, SetS}
import net.verdagon.chronobasecs.generator.chronobase.Generator.toCS
import net.verdagon.chronobasecs.generator.chronobase.ChronobaseOptions

object MutSetIncarnation {

  def generateIncarnation(opt: ChronobaseOptions, set: SetS): Map[String, String] = {
    val SetS(setName, MutableS, elementType) = set

    val incarnationName = s"${setName}Incarnation"

    val incarnationDefinition =
      s"""public class ${setName}Incarnation {
         |  public readonly SortedSet<int> elements;
         |
         |  public ${setName}Incarnation(SortedSet<int> elements) {
         |    this.elements = new SortedSet<int>(elements);
         |  }
         |
         |  public ${setName}Incarnation Copy() {
         |    return new ${setName}Incarnation(new SortedSet<int>(elements));
         |  }
         |}
         |""".stripMargin

    Map(incarnationName -> incarnationDefinition)
  }

}
