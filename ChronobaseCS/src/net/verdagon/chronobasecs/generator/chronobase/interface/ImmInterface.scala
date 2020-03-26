package net.verdagon.chronobasecs.generator.chronobase.interface

import net.verdagon.chronobasecs.compiled.{ImmutableS, InterfaceS}
import net.verdagon.chronobasecs.generator.chronobase.Generator.signatureToString
import net.verdagon.chronobasecs.generator.chronobase.ChronobaseOptions

object ImmInterface {

  def generateInterface(opt: ChronobaseOptions, interface: InterfaceS): Map[String, String] = {
    val InterfaceS(
    interfaceName,
    ImmutableS,
    methods,
    parentInterfaces,
    ancestorInterfaces,
    childInterfaces,
    childStructs,
    descendantInterfaces,
    descendantStructs)
    = interface

    val mainInterface =
      s"public interface ${interfaceName} {\n" +
        s"  string DStr();\n" +
        s"  int GetDeterministicHashCode();\n" +
        s"  void Visit${interfaceName}(${interfaceName}Visitor visitor);\n" +
        methods.map("  " + signatureToString(_) + ";\n").mkString("") +
        "}\n"

    val nullClassName = s"Null${interfaceName}"
    val nullClass =
      s"""
         |public class ${nullClassName} : ${interfaceName} {
         |  public static ${nullClassName} Null = new Null${interfaceName}();
         |  public string DStr() { return "null"; }
         |  public int GetDeterministicHashCode() { return 0; }
         |  public void Visit${interfaceName}(${interfaceName}Visitor visitor) { throw new Exception("Called method on a null!"); }
         |""".stripMargin +
        methods.map({ method =>
          "  public " + signatureToString(method) + "{ throw new Exception(\"Called method on a null!\"); }\n"
        }).mkString("") +
        "}\n"

    val visitorName = s"${interfaceName}Visitor";
    val visitorClass =
      s"""
         |public interface ${visitorName} {
         |""".stripMargin +
        childInterfaces.map(_.name).map({ subInterfaceName =>
          s"  void Visit${interfaceName}(${subInterfaceName} obj);\n"
        }).mkString("") +
        childStructs.map(_.name).map({ structName =>
          s"  void Visit${interfaceName}(${structName}As${interfaceName} obj);\n"
        }).mkString("") +
        "}\n"

    val parserName = s"${interfaceName}Parser";
    val parserClass =
      s"""
         |public static class ${parserName} {
         |  public static ${interfaceName} Parse(ParseSource source) {
         |    var nextThingPeek = source.PeekNextWord();
         |    switch (nextThingPeek) {
         |""".stripMargin +
        descendantStructs.map(_.name).map({ structName =>
          "      case \"" + structName + "\":\n" +
            s"        return new ${structName}As${interfaceName}(${structName}.Parse(source));\n"
        }).mkString("") +
        "      default:\n" +
        "        throw new Exception(\"Unexpected: \" + nextThingPeek);\n" +
        "    }\n" +
        "  }\n" +
        "}\n"

    Map(
      interfaceName -> mainInterface,
      nullClassName -> nullClass,
      visitorName -> visitorClass,
      parserName -> parserClass)
  }
}
