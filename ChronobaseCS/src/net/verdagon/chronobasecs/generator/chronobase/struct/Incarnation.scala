package net.verdagon.chronobasecs.generator.chronobase.struct

import net.verdagon.chronobasecs.compiled.{MutableS, StructMemberS, StructS, VaryingS}
import net.verdagon.chronobasecs.generator.chronobase.Generator.toCS
import net.verdagon.chronobasecs.generator.chronobase.ChronobaseOptions

object Incarnation {

  def generateIncarnation(opt: ChronobaseOptions, struct: StructS): Map[String, String] = {
    val StructS(structName, _, MutableS, members, _, _, _) = struct
    val incarnationDefinition =
      s"public class ${structName}Incarnation : I${structName}EffectVisitor {\n" +
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
        s"  public ${structName}Incarnation Copy() {\n" +
        s"    return new ${structName}Incarnation(\n" +
        members.map(_.name).mkString(",\n") +
        s"    );\n" +
        s"  }\n" +
      s"""
           |  public void visit${structName}CreateEffect(${structName}CreateEffect e) {}
           |  public void visit${structName}DeleteEffect(${structName}DeleteEffect e) {}
           |""".stripMargin +
        members.map({ case StructMemberS(memberName, variability, memberType) => {
          if (variability == VaryingS) {

            val effectName = s"${structName}Set${memberName.capitalize}Effect"
            s"public void visit${effectName}(${effectName} e) { this.${memberName} = e.newValue; }"
          } else ""
        }}).mkString("\n") +
      s"""
         |  public void ApplyEffect(I${structName}Effect effect) { effect.visitI${structName}Effect(this); }
         |""".stripMargin +
      s"}\n"

    Map(struct.incarnationName -> incarnationDefinition)
  }
}
