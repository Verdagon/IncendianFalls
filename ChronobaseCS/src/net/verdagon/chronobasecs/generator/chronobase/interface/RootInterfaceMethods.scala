package net.verdagon.chronobasecs.generator

import net.verdagon.chronobasecs.compiled.{InterfaceS, MutableS}
import net.verdagon.chronobasecs.generator.chronobase.ChronobaseOptions

object RootInterfaceMethods {

  def generateRootInterfaceInstanceMethods(
                                            opt: ChronobaseOptions,
                                            interface: InterfaceS
  ): String = {
    val InterfaceS(
      interfaceName,
      MutableS,
      methods,
      parentInterfaces,
      ancestorInterfaces,
      childInterfaces,
      childStructs,
      descendantInterfaces,
      descendantStructs)
    = interface

    s"""
       |  public ${interfaceName} Get${interfaceName}(int id) {
       |""".stripMargin +
    descendantStructs.map(_.name).map(structName => {
      s"""    if (rootIncarnation.incarnations${structName}.ContainsKey(id)) {
         |      return new ${structName}As${interfaceName}(new ${structName}(this, id));
         |    }
         |""".stripMargin
    }).mkString("") +
    s"""    throw new Exception("Unknown ${interfaceName}: " + id);
       |  }
       |  public ${interfaceName} Get${interfaceName}OrNull(int id) {
       |""".stripMargin +
    descendantStructs.map(_.name).map(structName => {
      s"""    if (rootIncarnation.incarnations${structName}.ContainsKey(id)) {
         |      return new ${structName}As${interfaceName}(new ${structName}(this, id));
         |    }
         |""".stripMargin
    }).mkString("") +
    s"""    return Null${interfaceName}.Null;
       |  }
       |  public bool ${interfaceName}Exists(int id) {
       |    return Get${interfaceName}OrNull(id) != null;
       |  }
       |  public void CheckHas${interfaceName}(${interfaceName} thing) {
       |    Get${interfaceName}(thing.id);
       |  }
       |  public void CheckHas${interfaceName}(int id) {
       |    Get${interfaceName}(id);
       |  }
       |""".stripMargin
  }

}
