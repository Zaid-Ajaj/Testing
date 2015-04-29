﻿using System;
using System.Text;
using Bridge;
using Bridge.QUnit;

namespace ClientTestLibrary
{
    // Tests DateTime functions
    class TestSystemFunctions
    {
        // DateTime functions
        public static void DateTimes(Assert assert)
        {
            assert.Expect(2);

            //[#83] by C#
            var str = "2015-03-24T10:48:09.1500225+03:00";
            var bridgeDate = DateTime.Parse(str);
            var bridgeDate1 = new DateTime(str);
            assert.DeepEqual(bridgeDate, bridgeDate1, "[#83] C# bridgeDate = bridgeDate1");

            //[#83] by JavaScript code. This is to check the same issue as above and just to check another way of calling QUnit from JavaScript
            Script.Write<dynamic>(@"var str = ""2015-03-24T10:48:09.1500225+03:00"",
    bridgeDate = Bridge.Date.parse(str),
    jsDate = new Date(Date.parse(str)),
    format = ""yyyy-MM-dd hh:mm:ss"";
    assert.deepEqual(Bridge.Date.format(bridgeDate, format), Bridge.Date.format(jsDate, format), ""[#83] js"")");
        }

        // String functions
        public static void Strings(Assert assert)
        {
            assert.Expect(2);

            var msg = "HELLO".ToLowerCase();
            assert.DeepEqual(msg, "hello", "'HELLO'.ToLowerCase()");

            msg = "hello".ToUpperCase();
            assert.DeepEqual(msg, "HELLO", "'hello'.ToUpperCase()");
        }

        // StringBuilder functions
        public static void StringBuilders(Assert assert)
        {
            assert.Expect(1);

            var sb = new StringBuilder("foo");

            assert.DeepEqual(sb.ToString(), "foo", "StringBuilder(string) .ctor");
        }
    }
}
