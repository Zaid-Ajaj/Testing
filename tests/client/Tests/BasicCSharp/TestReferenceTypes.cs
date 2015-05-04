﻿using System;
using Bridge;
using Bridge.QUnit;

namespace ClientTestLibrary
{
    [FileName("testReferenceTypes.js")]
    class ClassA
    {
        //TODO Add more types
        public static int StatitIntNotInitialized;
        public static string StatitStringNotInitialized;

        public static int StaticInt;
        public static string StaticString;
        public const char CONST_CHAR = 'Q';
        //TODO Add more to check big/small numbers, precision etc
        public const decimal CONST_DECIMAL = 3.123456789324324324m;

        //TODO Add more types
        public int NumberA { get; set; }
        public string StringA { get; set; }
        public bool BoolA { get; set; }
        public double DoubleA { get; set; }
        public decimal DecimalA { get; set; }

        private Aux1 data;
        public Aux1 Data
        {
            get { return data; }
            set { data = value; }
        }

        static ClassA()
        {
            ClassA.StaticString = "Defined string";
            ClassA.StaticInt = -340;
        }

        public ClassA()
        {
            this.NumberA = 10;
            this.StringA = "Str";
            this.BoolA = true;
            this.DoubleA = Double.PositiveInfinity;
            this.DecimalA = Decimal.MinusOne;
            this.Data = new Aux1() { Number = 700 };
        }

        public ClassA(Aux1 d)
            : this()
        {
            if (d == null)
                throw new Exception("Related should not be null");

            this.Data = d;
        }

        //[#89]
        public ClassA(params object[] p)
            : this()
        {
            if (p == null || p.Length < 6)
            {
                throw new Exception("Should pass six parameters");
            }

            if (p[0] is int)
            {
                this.NumberA = (int)p[0];
            }

            if (p[1] is string)
            {
                this.StringA = (string)p[1];
            }

            if (p[2] is bool)
            {
                this.BoolA = (bool)p[2];
            }

            if (p[3] is double)
            {
                this.DoubleA = (double)p[3];
            }

            if (p[4] is decimal)
            {
                this.DecimalA = (decimal)p[4];
            }

            if (p[5] is Aux1)
            {
                this.Data = (Aux1)p[5];
            }
        }

        public ClassA.Aux1 Method1()
        {
            var a1 = new Aux1() { Number = 1 };

            return new Aux1() { Number = 2, Related = a1 };
        }

        public void Method2(ClassA.Aux1 a)
        {
            a.Related = a;
        }

        public string Method3()
        {
            if (this.Data != null)
            {
                return this.Data.ToString();
            }

            return "no data";
        }

        public int Method4(int i, int add)
        {
            i = i + add;
            return i;
        }

        public int Method5(int i = -5)
        {
            return i;
        }

        public int Method5(int i = -50, int k = -60)
        {
            return i + k;
        }

        public static ClassA StaticMethod1(int i, string s, double d)
        {
            ClassA.StatitIntNotInitialized = i;
            ClassA.StatitStringNotInitialized = s;

            return new ClassA() { DoubleA = d };
        }

        //[#89]
        public static ClassA StaticMethod2(params object[] p)
        {
            var i = (int)p[0] + 1000;
            var s = (string)p[1];
            var d = (double)p[2];

            return ClassA.StaticMethod1(i, s, d);
        }

        public static bool TryParse(object o, out int i)
        {
            i = 3;

            return true;
        }

        public static int GetDefaultInt()
        {
            return default(int);
        }

        [FileName("testReferenceTypes.js")]
        //due to [#73] needs priority to be generated after the parent class
        //[Priority(-1)]
        public class Aux1
        {
            public int Number { get; set; }
            public Aux1 Related { get; set; }

            public override string ToString()
            {
                return string.Format("{0} Has related {1}", this.Number, this.Related != null ? this.Related.Number.ToString() : "No");
            }
        }
    }

    [FileName("testReferenceTypes.js")]
    //[#68] Multiple field declaration renders incorrectly
    public class Class68
    {
        public int x, y = 1;

        public void Test()
        {
            //Multiple local vars correctly
            int x = 1, y = 2;

            var z = x + y;
        }
    }

    // Tests:
    // Reference type constructors, params method parameters, method overloading, nested classes, [FileName]
    // Full properties, short get/set properties, exceptions
    class TestReferenceTypes
    {
        //Check instance methods and constructors
        public static void TestInstanceConstructorsAndMethods(Assert assert)
        {
            assert.Expect(26);

            //Check parameterless constructor
            var a = new ClassA();

            // TEST
            assert.DeepEqual(a.NumberA, 10, "NumberA 10");
            assert.DeepEqual(a.StringA, "Str", "StringA Str");
            assert.DeepEqual(a.BoolA, true, "BoolA true");
            assert.Ok(a.DoubleA == Double.PositiveInfinity, "DoubleA Double.PositiveInfinity");
            assert.DeepEqual(a.DecimalA, -1, "DecimalA Decimal.MinusOne");
            assert.Ok(a.Data != null, "Data not null");
            assert.DeepEqual(a.Data.Number, 700, "Data.Number 700");

            // TEST
            //Check constructor with parameter
            assert.Throws(TestSet1FailureHelper.TestConstructor1Failure, "Related should not be null", "Related should not be null");

            // TEST
            //Check constructor with parameter
            assert.Throws(TestSet1FailureHelper.TestConstructor2Failure, "Should pass six parameters", "Should pass six parameters");

            a = new ClassA(150, "151", true, 1.53d, 1.54m, new ClassA.Aux1() { Number = 155 });

            assert.DeepEqual(a.NumberA, 150, "NumberA 150");
            assert.DeepEqual(a.StringA, "151", "StringA 151");
            assert.DeepEqual(a.BoolA, true, "BoolA true");
            assert.DeepEqual(a.DoubleA, 1.53, "DoubleA Double.PositiveInfinity");
            assert.DeepEqual(a.DecimalA, 1.54, "DecimalA 154");
            assert.Ok(a.Data != null, "Data not null");
            assert.DeepEqual(a.Data.Number, 155, "Data.Number 155");

            // TEST
            //Check instance methods
            var b = a.Method1();

            assert.Ok(b != null, "b not null");
            assert.DeepEqual(b.Number, 2, "b Number 2");
            assert.Ok(b.Related != null, "b.Related not null");
            assert.DeepEqual(b.Related.Number, 1, "b.Related Number 1");

            a.Data = b;
            assert.DeepEqual(a.Method3(), "2 Has related 1", "Method3 2 Has related 1");
            a.Data = null;
            assert.DeepEqual(a.Method3(), "no data", "Method3 no data");

            // TEST
            //Check [#68]
            var c68 = new Class68();

            assert.DeepEqual(c68.x, 0, "c68.x 0");
            assert.DeepEqual(c68.y, 1, "c68.y 1");

            // TEST
            //Check local vars do not get overridden by fields
            c68.Test();

            assert.DeepEqual(c68.x, 0, "c68.x 0");
            assert.DeepEqual(c68.y, 1, "c68.y 1");
        }

        //Check static methods and constructor
        public static void TestStaticConstructorsAndMethods(Assert assert)
        {
            assert.Expect(13);

            // TEST
            //Check static fields initialization
            assert.DeepEqual(ClassA.StatitIntNotInitialized, 0, "#74 StatitInt not initialized");
            assert.DeepEqual(ClassA.StatitStringNotInitialized, null, "#74 StatitString not initialized");
            assert.DeepEqual(ClassA.CONST_CHAR, 81, "#74 CONST_CHAR Q");
            assert.DeepEqual(ClassA.CONST_DECIMAL, 3.123456789324324324, "#74 CONST_DECIMAL 3.123456789324324324m");

            // TEST
            //Check static constructor
            assert.DeepEqual(ClassA.StaticInt, -340, "StatitInt initialized");
            assert.DeepEqual(ClassA.StaticString, "Defined string", "StatitString initialized");

            // TEST
            //Check static methods
            var a = ClassA.StaticMethod1(678, "ASD", double.NaN);

            assert.DeepEqual(ClassA.StatitIntNotInitialized, 678, "StatitIntNotInitialized 678");
            assert.DeepEqual(ClassA.StatitStringNotInitialized, "ASD", "ClassA.StatitStringNotInitialized ASD");
            assert.DeepEqual(a.DoubleA, double.NaN, "DoubleA double.NaN");

            a = ClassA.StaticMethod2((object)678, "QWE", 234);
            assert.DeepEqual(ClassA.StatitIntNotInitialized, 1678, "StatitIntNotInitialized 1678");
            assert.DeepEqual(ClassA.StatitStringNotInitialized, "QWE", "ClassA.StatitStringNotInitialized QWE");
            assert.DeepEqual(a.DoubleA, 234, "DoubleA 234");

            assert.Throws(TestSet1FailureHelper.StaticMethod2Failure, "Unable to cast type String to type Bridge.Int", "Cast exception should occur");
        }

        //Check default parameters, method parameters, default values
        public static void TestMethodParameters(Assert assert)
        {
            assert.Expect(16);

            //Check default parameters
            var ra = new ClassA();
            int r = ra.Method5(5);

            assert.DeepEqual(r, 5, "r 5");
            r = ra.Method5(i: 15);
            assert.DeepEqual(r, 15, "r 15");
            r = ra.Method5(5, 6);
            assert.DeepEqual(r, 11, "r 11");
            r = ra.Method5(k: 6);
            assert.DeepEqual(r, -44, "r -44");

            //Check referencing did not change data
            var a = new ClassA();
            var b = a.Method1();
            var c = b.Related;

            a.Method2(b);
            assert.Ok(b != null, "b not null");
            assert.DeepEqual(b.Number, 2, "b Number 2");
            assert.Ok(b.Related != null, "b.Related not null");
            assert.DeepEqual(b.Related.Number, 2, "b.Related Number 2");

            assert.Ok(c != null, "c not null");
            assert.DeepEqual(c.Number, 1, "c Number 1");
            assert.Ok(c.Related == null, "c.Related null");

            //Check value local parameter
            var input = 1;
            var result = a.Method4(input, 4);

            assert.DeepEqual(input, 1, "input 1");
            assert.DeepEqual(result, 5, "result 5");

            // TEST
            //[#86]
            var di = ClassA.GetDefaultInt();
            assert.DeepEqual(di, 0, "di 0");

            // TEST
            //Check  "out parameter"
            //[#85]
            int i;
            var tryResult = ClassA.TryParse("", out i);

            assert.Ok(tryResult, "tryResult");
            assert.DeepEqual(i, 3, "i 3");
        }
    }

    [FileName("testReferenceTypes.js")]
    public class TestSet1FailureHelper
    {
        //For testing exception throwing in constructors we need a separate method as constructors cannot be delegates
        public static void TestConstructor1Failure()
        {
            new ClassA((ClassA.Aux1)null);
        }

        public static void TestConstructor2Failure()
        {
            var t = new ClassA(new object[2]);
        }

        public static void StaticMethod2Failure()
        {
            ClassA.StaticMethod2("1", "some string", "345.345435");
        }
    }
}
