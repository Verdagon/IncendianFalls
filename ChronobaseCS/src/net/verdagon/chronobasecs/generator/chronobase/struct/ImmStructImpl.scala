package net.verdagon.chronobasecs.generator.chronobase.struct

import net.verdagon.chronobasecs.compiled._
import net.verdagon.chronobasecs.generator.chronobase.ChronobaseOptions
import net.verdagon.chronobasecs.generator.chronobase.{Generator, ChronobaseOptions}

object ImmStructImpl {

  def generateImmStructImpl(
                             opt: ChronobaseOptions,
                             struct: StructS,
                             impl: ImplS,
                             methods: List[FunctionS]
  ): Map[String, String] = {
    val structName = struct.name
    val interfaceName = impl.interface.name
    val typeclassName = structName + "As" + interfaceName

    val typeclassCode =
      s"""
         |public class ${typeclassName} : ${interfaceName} {
         |  public readonly ${structName} obj;
         |  public ${typeclassName}(${structName} obj) {
         |    this.obj = obj;
         |  }
         |  public string DStr() { return obj.DStr(); }
         |  public int GetDeterministicHashCode() { return obj.GetDeterministicHashCode(); }
         |  public override int GetHashCode() { return GetDeterministicHashCode(); }
       """.stripMargin +
        methods.map({ case FunctionS(signature, externFunction) =>
          val signatureWithoutThis = SignatureS(signature.name, signature.returnType, signature.parameters.tail);
          val argsStr = signature.parameters.tail.map({ case ParameterS(name, tyype, _) => ", " + name }).mkString("")
          val signatureStr = Generator.signatureToString(signatureWithoutThis)
          s"  public ${signatureStr} { return ${externFunction}(obj${argsStr}); }\n"
        }).mkString("") +
      s"""
         |  public void Visit${interfaceName}(${interfaceName}Visitor visitor) { visitor.Visit${interfaceName}(this); }
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
