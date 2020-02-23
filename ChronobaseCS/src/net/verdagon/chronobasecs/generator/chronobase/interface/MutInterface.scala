package net.verdagon.chronobasecs.generator

import net.verdagon.chronobasecs.compiled.{InterfaceS, MutableS, SignatureS, SuperstructureS}
import net.verdagon.chronobasecs.generator.chronobase.{Generator, ChronobaseOptions}
import net.verdagon.chronobasecs.generator.chronobase.Generator.signatureToString

object MutInterface {
  def generateInterface(opt: ChronobaseOptions, ssS: SuperstructureS, interface: InterfaceS): Map[String, String] = {
    val InterfaceS(interfaceName, MutableS, methods, _, _, _, _, _, _) = interface

    val mainInterface =
      s"""
         |public interface ${interfaceName} {
         |  ${interfaceName} As${interfaceName}();
         |  Root root { get; }
         |  int id { get; }
         |  void Delete();
         |  bool Exists();
         |  void FindReachableObjects(SortedSet<int> foundIds);
         |  bool Is(${interfaceName} that);
         |  bool NullableIs(${interfaceName} that);
         |""".stripMargin +
      interface.ancestorInterfaces
        .map(_.name)
        .map(ancestorName => s"  ${ancestorName} As${ancestorName}();\n")
        .mkString("") +
      methods.map("  " + signatureToString(_) + ";\n").mkString("") +
      "}"

    val nullClass =
      s"""
         |public class Null${interfaceName} : ${interfaceName} {
         |  public static Null${interfaceName} Null = new Null${interfaceName}();
         |
         |  public Root root { get { return null; } }
         |  public int id { get { return 0; } }
         |  public void Delete() {
         |    throw new Exception("Can't delete a null!");
         |  }
         |  public bool Exists() { return false; }
         |  public bool Is(${interfaceName} that) {
         |    throw new Exception("Called Is on a null!");
         |  }
         |  public void FindReachableObjects(SortedSet<int> foundIds) { }
         |  public bool NullableIs(${interfaceName} that) {
         |    return !that.Exists();
         |  }
         |  public ${interfaceName} As${interfaceName}() {
         |    return this;
         |  }
       """.stripMargin +
      interface.ancestorInterfaces
        .map(_.name)
        .map(ancestorName => ssS.interfaces.find(_.name == ancestorName).get)
        .map(ancestorInterface => {
          s"""  public bool Is(${ancestorInterface.name} that) {
             |    throw new Exception("Called Is on a null!");
             |  }
             |  public bool NullableIs(${ancestorInterface.name} that) {
             |    return !that.Exists();
             |  }
             |  public ${ancestorInterface.name} As${ancestorInterface.name}() {
             |    return Null${ancestorInterface.name}.Null;
             |  }
             |""".stripMargin
        })
        .mkString("") +
        methods.map((method: SignatureS) => {
            s"""
               |  public ${Generator.signatureToString(method)} {
               |    throw new Exception("Called ${method.name} on a null!");
               |  }
             """.stripMargin
          })
          .mkString("") +
      "}"

    Map(
      interfaceName -> mainInterface,
      s"Null${interfaceName}" -> nullClass)
  }
}
