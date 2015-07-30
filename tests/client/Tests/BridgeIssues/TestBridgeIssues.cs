﻿using Bridge;
using Bridge.QUnit;

using System;
using System.Collections;
using System.Collections.Generic;

namespace ClientTestLibrary
{
    [FileName("testBridgeIssues.js")]
    class Bridge169
    {
        public static int Number = 0;

        public static void M1()
        {
            new Action(() => { Bridge169.Number = 1; })();
        }

        public static void M2()
        {
            new Action(() => Bridge169.Number = 2)();
        }
    }

    [FileName("testBridgeIssues.js")]
    abstract class Bridge240A
    {
        public int Data { get; set; }
    }

    [FileName("testBridgeIssues.js")]
    class Bridge240B : Bridge240A
    {
        public string GetString()
        {
            base.Data++;
            return "B";
        }
    }

    [FileName("testBridgeIssues.js")]
    class Bridge266A
    {
        public static object Test()
        {
            // Nothing gets written for Class1 in the output JavaScript due to the "new object()" argument.
            // If null is used instead (as commented-out) then it works as expected.
            // No compile error.
            return Bridge266B.Test("test", new object());
            //Bridge266B.Test("test", null);
        }
    }

    [FileName("testBridgeIssues.js")]
    class Bridge266B
    {
        public static object Test(string arg1, object arg2)
        {
            return arg2;
        }
    }

    [FileName("testBridgeIssues.js")]
    class Bridge272
    {
        [FileName("testBridgeIssues.js")]
        public enum MyEnum { Abc = 1, Def = 2, Ghi = 3 };

        public static MyEnum Test(int i)
        {
            return (MyEnum)i;
        }
    }

    [FileName("testBridgeIssues.js")]
    class Bridge294
    {
        private readonly string Name;
        public Bridge294(string name)
        {
            this.Name = name;
        }

        public string GetName()
        {
            return this.Name;
        }

        public string GetNameThroughGeneric<T>()
        {
            return this.Name;
        }
    }

    [FileName("testBridgeIssues.js")]
    enum Bridge277 { Int }

    //[#304]
    [FileName("testBridgeIssues.js")]
    public class Bridge304 : IBridge304
    {
        public string X { get; set; }

        public void F(string x)
        {
            this.X = x;
        }

        public void F()
        {
            this.X = "void F()";
        }

    }
    [FileName("testBridgeIssues.js")]
    public interface IBridge304
    {
        void F(string x);
    }

    [FileName("testBridgeIssues.js")]
    public class Bridge305 : IEnumerable<string>
    {
        public List<string> Items { get; private set; }

        public Bridge305(string[] items)
        {
            Items = new List<string>(items);
        }

        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
        public IEnumerator<string> GetEnumerator() { return Items.GetEnumerator(); }
    }

    [FileName("testBridgeIssues.js")]
    public class Bridge306A : Bridge306Component<Bridge306A.Props>
    {
        public static string New(Props props)
        {
            return New<Bridge306A>(props);
        }

        public class Props
        {
            public string Name;

            public override string ToString()
            {
                return Name;
            }
        }
    }

    [FileName("testBridgeIssues.js")]
    public class Bridge306B : Bridge306Component<Bridge306B.Props>
    {
        public static string New(Props props)
        {
            return Bridge306Component<Bridge306B.Props>.New<Bridge306B>(props);
        }

        public class Props
        {
            public string Name;

            public override string ToString()
            {
                return Name;
            }
        }
    }

    [FileName("testBridgeIssues.js")]
    public abstract class Bridge306Component<TProps>
    {
        public static string New<TComponent>(TProps props) where TComponent : Bridge306Component<TProps>
        {
            return props.GetClassName() + ":" + props;
        }
    }

    // Tests Bridge GitHub issues
    class TestBridgeIssues
    {

        // Bridge[#169]
        public static void N169(Assert assert)
        {
            assert.Expect(2);

            // TEST
            Bridge169.M1();
            assert.DeepEqual(Bridge169.Number, 1, "M1()");

            // TEST
            Bridge169.M2();
            assert.DeepEqual(Bridge169.Number, 2, "M2()");
        }

        // Bridge[#240]
        public static void N240(Assert assert)
        {
            assert.Expect(3);

            // TEST
            var b = new Bridge240B();
            assert.Ok(b != null, "Instance of B created");
            assert.Equal(b.GetString(), "B", "b.GetString() = 'B'");
            assert.Equal(b.Data, 1, "b.Data = 1");
        }

        // Bridge[#266]
        public static void N266(Assert assert)
        {
            assert.Expect(1);

            // TEST
            assert.Ok(Bridge266A.Test() != null, "new object() call transpiled");
        }

        // Bridge[#272]
        public static void N272(Assert assert)
        {
            assert.Expect(3);

            // TEST
            assert.DeepEqual(Bridge272.Test(1), Bridge272.MyEnum.Abc, "Casted MyEnum.Abc");
            assert.DeepEqual(Bridge272.Test(3), Bridge272.MyEnum.Ghi, "Casted MyEnum.Ghi");
            assert.DeepEqual(Bridge272.Test(4), 4, "Casted MyEnum.Abc");
        }

        // Bridge[#273]
        public static void N273(Assert assert)
        {
            assert.Expect(4);

            // TEST
            var items = new List<int>() { 0, 1, 2, 3, 4 };

            var r = items.Slice(-1).ToArray();
            assert.DeepEqual(r, new[] { 4 }, "Slices start = -1");

            r = items.Slice(1).ToArray();
            assert.DeepEqual(r, new[] { 1, 2, 3, 4 }, "Slices start = 1");

            r = items.Slice(-3, 4).ToArray();
            assert.DeepEqual(r, new[] { 2, 3 }, "Slices start = -3, end = 3");

            r = items.Slice(1, 3).ToArray();
            assert.DeepEqual(r, new[] { 1, 2 }, "Slices start = 1, end = 2");

        }

        // Bridge[#277]
        public static void N277(Assert assert)
        {
            assert.Expect(1);

            assert.Equal(Bridge277.Int, 0, "Enum member with reserved name initialized");
        }

        // Bridge[#294]
        public static void N294(Assert assert)
        {
            assert.Expect(2);

            var c = new Bridge294("Vlad");

            assert.Equal(c.GetName(), "Vlad", "Class method works");
            assert.Equal(c.GetNameThroughGeneric<int>(), "Vlad", "Generic class method works");

        }

        // Bridge[#304]
        public static void N304(Assert assert)
        {
            assert.Expect(2);

            var c = new Bridge304();
            IBridge304 i = c;

            i.F("1");
            assert.Equal(c.X, "1", "Interface method works");

            c.F();
            assert.Equal(c.X, "void F()", "Class method works");
        }

        // Bridge[#305]
        public static void N305(Assert assert)
        {
            assert.Expect(1);

            var c = new Bridge305(new[] { "1", "2", "3" });

            var result = string.Empty;
            foreach (var item in c)
            {
                result = result + item;
            }

            assert.Equal(result, "123", "IEnumerator works");
        }

        // Bridge[#306]
        public static void N306(Assert assert)
        {
            assert.Expect(2);

            var b = Bridge306B.New(new Bridge306B.Props() { Name = "B" });
            assert.Equal(b, "ClientTestLibrary.Bridge306B.Props:B", "Bridge306B.New() works");

            var a = Bridge306A.New(new Bridge306A.Props() { Name = "A" });
            assert.Equal(a, "ClientTestLibrary.Bridge306A.Props:A", "Bridge306A.New() works");
        }

        // Bridge[#335]
        public static void N335(Assert assert)
        {
            assert.Expect(1);

            var l = new List<string>(new[] { "1", "2", "3", "1" });
            assert.Equal(l.IndexOf("1", 2), 3, "IndexOf with startIndex used");
        }

        // Bridge[#336]
        public static void N336(Assert assert)
        {
            assert.Expect(2);

            var l = new List<string>(new[] { "4" });

            l.InsertRange(0, new[] { "1", "2" });
            assert.DeepEqual(l.ToArray(), new[] { "1", "2", "4" }, "InsertRange works (1)");

            l.InsertRange(2, new[] { "3" });
            assert.DeepEqual(l.ToArray(), new[] { "1", "2", "3", "4" }, "InsertRange works (2)");
        }

        // Bridge[#337]
        public static void N337(Assert assert)
        {
            assert.Expect(4);

            var l = new List<string>(new[] { "1", "2" });

            var b = l.Remove("7");
            assert.NotOk(b, "Remove() not existing element returns false");
            assert.DeepEqual(l.ToArray(), new[] { "1", "2" }, "Remove() not existing does not change the List");

            b = l.Remove("2");
            assert.Ok(b, "Remove() existing element returns true");
            assert.DeepEqual(l.ToArray(), new[] { "1" }, "Remove() not existing changes the List");
        }

        // Bridge[#338]
        public static void N338(Assert assert)
        {
            assert.Expect(1);

            var l = new List<string>(1000);

            var b = l is IList<string>;

            assert.Ok(b, "List<T> declares it implemets IList<T>");
        }


        // Bridge[#339]
        public static void N339(Assert assert)
        {
            assert.Expect(2);

            var c = Comparer<int>.Default;

            assert.Ok(c != null, "Comparer<int>.Default works");
            assert.Ok(c is IComparer<int>, "Comparer<T> declares it implemets IComparer<T>");
        }

        // Bridge[#340]
        public static void N340(Assert assert)
        {
            assert.Expect(6);

            var c = EqualityComparer<int>.Default;

            assert.Ok(c != null, "EqualityComparer<int>.Default works");
            assert.Ok(c.Equals(10, 10), "EqualityComparer<int>.Default.Equals(10, 10) works");
            assert.NotOk(c.Equals(10, 11), "EqualityComparer<int>.Default.Equals(10, 11) works");

            var s = EqualityComparer<string>.Default;
            assert.Ok(s != null, "EqualityComparer<string>.Default works");
            assert.Ok(s.Equals("a", "a"), "EqualityComparer<string>.Default.Equals(\"a\", \"a\") works");
            assert.NotOk(s.Equals("a", "b"), "EqualityComparer<string>.Default.Equals(\"a\", \"b\") works");
        }
    }
}
