package net.verdagon.chronobasecs.generator.chronobase.struct

import net.verdagon.chronobasecs.compiled.{MutableS, StructMemberS, StructS}
import net.verdagon.chronobasecs.generator.chronobase.Generator.toCS
import net.verdagon.chronobasecs.generator.chronobase.ChronobaseOptions

object Incarnation {

  def generateIncarnation(opt: ChronobaseOptions, struct: StructS): Map[String, String] = {
    val StructS(structName, _, MutableS, members, _, _, _) = struct
    val incarnationDefinition =
      s"public class ${structName}Incarnation {\n" +
      members.map({ case StructMemberS(memberName, variability, memberType) =>
        s"  public ${toCS(variability)} ${toCS(memberType.flatten)} ${memberName};\n"
      }).mkString("") +
      s"  public ${structName}Incarnation(\n" +
      members.map({ case StructMemberS(memberName, _, memberType) =>
        s"      ${toCS(memberType.flatten)} ${memberName}"
      }).mkString(",\n") +
      s") {\n" +
      members.map({ case StructMemberS(memberName, _, _) =>
        s"    this.${memberName} = ${memberName};\n"
      }).mkString("") +
      s"  }\n" +
      s"}\n"

    Map(struct.incarnationName -> incarnationDefinition)
  }

}
