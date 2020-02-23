package net.verdagon.chronobasecs

import net.verdagon.chronobasecs.parsed._
import net.verdagon.chronobasecs.parser.VCSParser
import org.scalatest.{FunSuite, Matchers}

class Tests extends FunSuite with Matchers {
  def parse(codeWithComments: String): SuperstructureP = {
    val codeWithoutComments = codeWithComments.replaceAll("//.*", "");

    VCSParser.parse(VCSParser.superstructure, codeWithoutComments.toCharArray) match {
      case VCSParser.NoSuccess(msg, input) => {
        fail(msg);
      }
      case VCSParser.Success(expr, rest) => {
        if (!rest.atEnd) {
          throw new RuntimeException(rest.pos.longString)
        }
        expr
      }
    }
  }

  test("Simple mut struct") {
    parse(
      """
        |mut struct ExecutionState {
        |  var actingUnit: ?Unit;
        |  var actingUnitDidAction: Bool;
        |}
      """.stripMargin
    ).structs shouldEqual
      List(
        StructP(
          "ExecutionState",
          false,
          MutableP,
          List(
            StructMemberP("actingUnit", VaryingP, TypeP(true, OwnP, NameKindP("Unit"))),
            StructMemberP("actingUnitDidAction", VaryingP, TypeP(false, OwnP, BoolKindP))),
          List(),
          List()))
  }

  test("Simple imm struct") {
    parse(
      """
        |imm struct Location {
        |  indexInGroup: Int;
        |}
      """.stripMargin
    ).structs shouldEqual
      List(
        StructP(
          "Location",
          false,
          ImmutableP,
          List(
            StructMemberP("indexInGroup", FinalP, TypeP(false, OwnP, IntKindP))),
          List(),
          List()))
  }

  test("Simple mut list") {
    parse(
      """
        |mut struct Unit {
        |  events: MutList:IUnitEvent;
        |}
      """.stripMargin
    ).structs shouldEqual
      List(
        StructP(
          "Unit",
          false,
          MutableP,
          List(StructMemberP("events", FinalP, TypeP(false, OwnP, TemplateKindP("MutList", List(TypeP(false, OwnP, NameKindP("IUnitEvent"))))))),
          List(),
          List()))
  }

  test("Mut interface") {
    parse(
      """
        |mut interface IDetail {
        |  fn AffectIncomingDamage(incomingDamage: Int): Int;
        |}
      """.stripMargin
    ).interfaces shouldEqual
      List(
        InterfaceP(
          "IDetail",
          MutableP,
          List(),
          List(
            SignatureP("AffectIncomingDamage", TypeP(false, OwnP, IntKindP), List(ParameterP("incomingDamage", TypeP(false, OwnP, IntKindP), None))))))
  }

  test("Imm interface") {
    parse(
      """
        |imm interface IUnitEvent {
        |  fn GetTime(): Void;
        |}
      """.stripMargin
    ).interfaces shouldEqual
      List(
        InterfaceP(
          "IUnitEvent",
          ImmutableP,
          List(),
          List(
            SignatureP("GetTime", TypeP(false, OwnP, VoidKindP), List()))))
  }

  test("Function with override and param") {
    parse(
      """
        |fn AffectIncomingDamage(
        |    detail: DefendingDetail overrides IDetail,
        |    incomingDamage: Int)
        |: Int {
        |  DefendingDetailExtensions.AffectIncomingDamageImpl
        |}
      """.stripMargin
    ).functions shouldEqual
      List(
        FunctionP(
          SignatureP(
            "AffectIncomingDamage",
            TypeP(false, OwnP, IntKindP),
            List(
              ParameterP("detail", TypeP(false, OwnP, NameKindP("DefendingDetail")), Some("IDetail")),
              ParameterP("incomingDamage", TypeP(false, OwnP, IntKindP), None))),
          "DefendingDetailExtensions.AffectIncomingDamageImpl"))
  }

  test("Function with override") {
    parse(
      """
        |fn GetTime(event: UnitStepEvent overrides IUnitEvent): Int {
        |  UnitStepEventExtensions.GetTime
        |}
      """.stripMargin
    ).functions shouldEqual
      List(
        FunctionP(
          SignatureP(
            "GetTime",
            TypeP(false, OwnP, IntKindP),
            List(
              ParameterP("event", TypeP(false, OwnP, NameKindP("UnitStepEvent")), Some("IUnitEvent")))),
          "UnitStepEventExtensions.GetTime"))
  }

  test("Impl") {
    parse(
      """
        |DefendingDetail isa IDetail;
      """.stripMargin
    ).impls shouldEqual
        List(
          ImplP("DefendingDetail", "IDetail"))
  }
}
