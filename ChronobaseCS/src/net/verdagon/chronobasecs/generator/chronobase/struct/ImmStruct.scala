package net.verdagon.chronobasecs.generator

import net.verdagon.chronobasecs.compiled._
import net.verdagon.chronobasecs.generator.chronobase.Generator.toCS
import net.verdagon.chronobasecs.generator.chronobase.ChronobaseOptions

object ImmStruct {

  def generateValue(opt: ChronobaseOptions, struct: StructS): Map[String, String] = {
    val StructS(structName, _, ImmutableS, members, _, _, _) = struct

    val structCode =
      s"public class ${structName} : IComparable<${structName}> {\n" +
      "  public static readonly string NAME = \"" + structName + "\";" +
      s"""
         |  public class EqualityComparer : IEqualityComparer<${structName}> {
         |    public bool Equals(${structName} a, ${structName} b) {
         |      return a.Equals(b);
         |    }
         |    public int GetHashCode(${structName} a) {
         |      return a.GetDeterministicHashCode();
         |    }
         |  }
         |  public class Comparer : IComparer<${structName}> {
         |    public int Compare(${structName} a, ${structName} b) {
         |      return a.CompareTo(b);
         |    }
         |  }
         |  private readonly int hashCode;
       """.stripMargin +
      members.map({ case StructMemberS(memberName, variability, memberType) =>
        s"  public readonly ${toCS(memberType)} ${memberName};\n"
      }).mkString("") +
      s"  public ${structName}(\n" +
      members.map({ case StructMemberS(memberName, variability, memberType) =>
        s"      ${toCS(memberType)} ${memberName}"
      }).mkString(",\n") +
      s") {\n" +
      members.map({ case StructMemberS(memberName, variability, memberType) =>
        s"    this.${memberName} = ${memberName};\n"
      }).mkString("") +
      s"    int hash = 0;\n" +
        members.map({ case StructMemberS(memberName, variability, memberType) =>
          s"    hash = hash * 37 + ${memberName}.GetDeterministicHashCode();\n"
        }).mkString("") +
      s"    this.hashCode = hash;\n" +
      s"""
         |  }
         |  public static bool operator==(${structName} a, ${structName} b) {
         |    if (object.ReferenceEquals(a, null))
         |      return object.ReferenceEquals(b, null);
         |    return a.Equals(b);
         |  }
         |  public static bool operator!=(${structName} a, ${structName} b) {
         |    if (object.ReferenceEquals(a, null))
         |      return !object.ReferenceEquals(b, null);
         |    return !a.Equals(b);
         |  }
         |  public override bool Equals(object obj) {
         |    if (obj == null) {
         |      return false;
         |    }
         |    if (!(obj is ${structName})) {
         |      return false;
         |    }
         |    var that = obj as ${structName};
         |    return true
       """.stripMargin +
      members.map({ case StructMemberS(memberName, variability, memberType) =>
        s"        && ${memberName}.Equals(that.${memberName})\n"
      }).mkString("") +
      s"        ;\n" +
      s"  }\n" +
      s"  public override int GetHashCode() {\n" +
      s"    return GetDeterministicHashCode();\n" +
      s"  }\n" +
      s"  public int GetDeterministicHashCode() { return hashCode; }\n" +
      s"  public int CompareTo(${structName} that) {\n" +
      members.map({ case StructMemberS(memberName, variability, memberType) =>
        s"    if (${memberName} != that.${memberName}) {\n" +
          s"      return ${memberName}.CompareTo(that.${memberName});\n" +
          s"    }\n"
      }).mkString("") +
      s"    return 0;\n" +
      s"  }\n" +
      s"  public override string ToString() { return DStr(); }\n" +
      s"  public string DStr() {\n" +
      (if (members.nonEmpty) {
        "    return \"" + structName + "(\" +\n" +
        members.map(_.name).map(name => s"        ${name}.DStr()").mkString(" + \", \" +\n") +
        "\n        + \")\";\n"
      } else {
        "    return \"" + structName + "()\";"
      }) +
      s"""
         |    }
         |    public static ${structName} Parse(ParseSource source) {
         |      source.Expect(NAME);
         |      source.Expect("(");
         |""".stripMargin +
      members.zipWithIndex.map({ case (StructMemberS(memberName, variability, memberType), index) =>
        val memberCSType = toCS(memberType)
        (if (index != 0) {
           "      source.Expect(\",\");\n"
        } else { "" }) +
        (memberType match {
          case x if memberType.kind.isPrimitive => {
            s"      var ${memberName} = source.Parse${memberType.name.capitalize}();\n"
          }
          case TypeS(_, _, InterfaceKindS(_, _)) => {
            s"      var ${memberName} = ${memberCSType}Parser.Parse(source);\n"
          }
          case _ => {
            s"      var ${memberName} = ${memberCSType}.Parse(source);\n"
          }
        })
      }).mkString("") +
      s"""      source.Expect(")");
         |      return new ${structName}(${members.map(_.name).mkString(", ")});
         |  }
         |}
       """.stripMargin

    Map(structName -> structCode)
  }

}
