package net.verdagon.chronobasecs.generator

import net.verdagon.chronobasecs.compiled._
import net.verdagon.chronobasecs.generator.chronobase.{Generator, ChronobaseOptions}

object MutStructImpl {

  def generateStructImpl(
                          opt: ChronobaseOptions,
                          struct: StructS,
                          impl: ImplS,
                          methods: List[FunctionS],
  ): Map[String, String] = {
    val structName = struct.name
    val interfaceName = impl.interface.name
    val typeclassName = structName + "As" + interfaceName

    val typeclassCode =
      s"""
         |public class ${typeclassName} : ${interfaceName} {
         |  public readonly ${structName} obj;
         |  public int id => obj.id;
         |  public Root root => obj.root;
         |  public void Delete() { obj.Delete(); }
         |  public bool Exists() { return obj.Exists(); }
         |  public ${typeclassName}(${structName} obj) {
         |    this.obj = obj;
         |  }
         |  public void FindReachableObjects(SortedSet<int> foundIds) {
         |    obj.FindReachableObjects(foundIds);
         |  }
         |""".stripMargin +
       struct.ancestorInterfaces.map(_.name).map(ancestorName => {
         s"""  public bool Is(${ancestorName} that) {
            |    if (!this.Exists()) {
            |      throw new Exception("Called Is on a null!");
            |    }
            |    if (!that.Exists()) {
            |      throw new Exception("Called Is on a null!");
            |    }
            |    return root == that.root && obj.id == that.id;
            |  }
            |  public bool NullableIs(${ancestorName} that) {
            |    if (!this.Exists() && !that.Exists()) {
            |      return true;
            |    }
            |    if (!this.Exists() || !that.Exists()) {
            |      return false;
            |    }
            |    return this.Is(that);
            |  }
            |  public ${ancestorName} As${ancestorName}() {
            |    return new ${structName}As${ancestorName}(obj);
            |  }
            |""".stripMargin
       }).mkString("") +
        s"""
       """.stripMargin +
        methods.map({ case FunctionS(signature, externalFunctionName) =>
          val signatureWithoutThis = SignatureS(signature.name, signature.returnType, signature.parameters.tail);
          s"  public " + Generator.signatureToString(signatureWithoutThis) + " {\n" +
            s"    return ${externalFunctionName}(obj" +
            signature.parameters.tail.map({ case ParameterS(name, tyype, _) => ", " + name }).mkString("") +
            s");\n" +
            s"  }\n"
        }).mkString("") +
        s"""
           |}
           |public static class ${typeclassName}Caster {
           |  public static ${typeclassName} As${interfaceName}(this ${structName} obj) {
           |    return new ${typeclassName}(obj);
           |  }
           |}
           |""".stripMargin
    Map(typeclassName -> typeclassCode)
  }
}
