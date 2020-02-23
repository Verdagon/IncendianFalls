package net.verdagon.chronobasecs.generator

import net.verdagon.chronobasecs.compiled.{InterfaceS, InterfaceKindS, SignatureS}

object MutInterfaceImpl {

//  def generateInterfaceImpl(
//      subInterfaceS: InterfaceTypeS,
//      superInterfaceS: InterfaceTypeS,
//  ): Map[String, String] = {
//    val subName = subInterfaceS.name
//    val superName = superInterfaceS.name
//    val typeclassName = subName + "As" + superName
//    val typeclassCode =
//      s"""
//         |public class ${typeclassName} : ${superName} {
//         |  public readonly ${subName} obj;
//         |  public int id => obj.id;
//         |  public Root root => obj.root;
//         |  public void Delete() { obj.Delete(); }
//         |  public bool NullableIs(${superName} that) {
//         |    if (!this.Exists() && !that.Exists()) {
//         |      return true;
//         |    }
//         |    if (!this.Exists() || !that.Exists()) {
//         |      return false;
//         |    }
//         |    return this.Is(that);
//         |  }
//         |  public bool Is(${superName} that) {
//         |    if (!this.Exists()) {
//         |      throw new Exception("Called Is on a null!");
//         |    }
//         |    if (!that.Exists()) {
//         |      throw new Exception("Called Is on a null!");
//         |    }
//         |    return root == that.root && obj.id == that.id;
//         |  }
//         |  public bool Exists() { return obj.Exists(); }
//         |  public ${typeclassName}(${subName} obj) {
//         |    this.obj = obj;
//         |  }
//       """.stripMargin +
//  //      superInterfaceS.methods.map({ signature =>
//  //        val signatureWithoutThis = SignatureS(signature.name, signature.returnType, signature.parameters.tail)
//  //        s"  public " + Generator.signatureToString(signatureWithoutThis) + " {\n" +
//  //          s"    return obj.${signature.name}(obj" +
//  //          signature.parameters.tail.map(_.name).map(", " + _).mkString("") +
//  //          s");\n" +
//  //          s"  }\n"
//  //      }).mkString("") +
//        s"""
//           |}
//           |public static class ${typeclassName}Caster {
//           |  public static ${typeclassName} As${superName}(this ${subName} obj) {
//           |    return new ${typeclassName}(obj);
//           |  }
//           |}
//           |""".stripMargin
//    Map(typeclassName -> typeclassCode)
//  }

}
