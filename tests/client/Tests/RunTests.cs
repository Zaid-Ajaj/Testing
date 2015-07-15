﻿using Bridge;
using Bridge.Html5;
using Bridge.QUnit;

using ClientTestLibrary.Linq;

namespace ClientTestLibrary
{
    [FileName("testRunner.js")]
    class RunTests
    {
        [Ready]
        public static void Main()
        {
            QUnit.Module("Inheritance, abstract, virtual and overloading");
            QUnit.Test("Overloading static methods", TestOverloadStaticMethods.TestStatic);
            QUnit.Test("Overloading instance methods", TestOverloadInstanceMethods.TestInstance);
            QUnit.Test("Inheritance A instance", TestInheritance.TestA);
            QUnit.Test("Inheritance B instance", TestInheritance.TestB);
            QUnit.Test("Inheritance B instance as A type", TestInheritance.TestAB);
            QUnit.Test("Abstract B instance", TestAbstractClass.TestB);
            QUnit.Test("Abstract C instance", TestAbstractClass.TestC);
            QUnit.Test("Abstract BC instance as A type", TestAbstractClass.TestBC);
            QUnit.Test("Virtual methods", TestVirtualMethods.TestB);

            QUnit.Module("Reference types");
            QUnit.Test("Instance constructors and methods", TestReferenceTypes.TestInstanceConstructorsAndMethods);
            QUnit.Test("Static constructors and methods", TestReferenceTypes.TestStaticConstructorsAndMethods);
            QUnit.Test("Method parameters", TestReferenceTypes.TestMethodParameters);

            QUnit.Module("Value types");
            QUnit.Test("Instance constructors and methods", TestValueTypes.TestInstanceConstructorsAndMethods);
            QUnit.Test("Static constructors and methods", TestValueTypes.TestStaticConstructorsAndMethods);

            QUnit.Module("Interfaces");
            QUnit.Test("Interface method and property", TestInterfaces.TestInterfaceMethodAndProperty);
            QUnit.Test("Explicit interface", TestInterfaces.TestExplicitInterfaceMethodAndProperty);
            QUnit.Test("Simple two interfaces", TestInterfaces.TestTwoInterfaces);

            QUnit.Module("Method parameters");
            QUnit.Test("Default and params", TestMethodParametersClass.Test);

            QUnit.Module("System functions");
            QUnit.Test("DateTime", TestDateFunctions.DateTimes);
            QUnit.Test("String", TestStringFunctions.Strings);
            QUnit.Test("StringBuilder", TestStringBuilderFunctions.StringBuilders);

            QUnit.Module("Try/Catch");
            QUnit.Test("Try/Catch simpe", TestTryCatchBlocks.SimpleTryCatch);
            QUnit.Test("Try/Catch caught exceptions", TestTryCatchBlocks.CaughtExceptions);
            QUnit.Test("Try/Catch thrown exceptions", TestTryCatchBlocks.ThrownExceptions);
            QUnit.Test("Try/Catch/Finally simple", TestTryCatchFinallyBlocks.SimpleTryCatchFinally);
            QUnit.Test("Try/Catch/Finally caught exceptions", TestTryCatchFinallyBlocks.CaughtExceptions);
            QUnit.Test("Try/Catch/Finally thrown exceptions", TestTryCatchFinallyBlocks.ThrownExceptions);

            QUnit.Module("Bridge GitHub issues");
            QUnit.Test("#169", TestBridgeIssues.N169);
            QUnit.Test("#240", TestBridgeIssues.N240);
            QUnit.Test("#266", TestBridgeIssues.N266);
            QUnit.Test("#272", TestBridgeIssues.N272);
            QUnit.Test("#273", TestBridgeIssues.N273);
            QUnit.Test("#277", TestBridgeIssues.N277);
            QUnit.Test("#294", TestBridgeIssues.N294);
            QUnit.Test("#304", TestBridgeIssues.N304);
            QUnit.Test("#305", TestBridgeIssues.N305);
            QUnit.Test("#306", TestBridgeIssues.N306);

            QUnit.Module("LINQ");
            QUnit.Test("Aggregate operators", TestLinqAggregateOperators.Test);
            QUnit.Test("Conversion operators", TestLinqConversionOperators.Test);
            QUnit.Test("Element operators", TestLinqElementOperators.Test);
            QUnit.Test("Generation operators", TestLinqGenerationOperators.Test);
            QUnit.Test("Join operators", TestLinqJoinOperators.Test);
            QUnit.Test("Grouping operators", TestLinqGroupingOperators.Test);
            QUnit.Test("Complex grouping operators.", TestLinqGroupingOperators.TestComplexGrouping);
            QUnit.Test("Anagram grouping operators.", TestLinqGroupingOperators.TestAnagrams);
            QUnit.Test("Miscellaneous operators", TestLinqMiscellaneousOperators.Test);
            QUnit.Test("Ordering operators", TestLinqOrderingOperators.Test);
            QUnit.Test("Partitioning operators", TestLinqPartitioningOperators.Test);
            QUnit.Test("Projection operators", TestLinqProjectionOperators.Test);
            QUnit.Test("Quantifiers", TestLinqQuantifiers.Test);
            QUnit.Test("Query execution", TestLinqQueryExecution.Test);
            QUnit.Test("Restriction operators", TestLinqRestrictionOperators.Test);
            QUnit.Test("Set operators", TestLinqSetOperators.Test);
        }
    }
}
