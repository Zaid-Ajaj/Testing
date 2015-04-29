﻿using Bridge.Linq;
using Bridge.QUnit;
using System.Linq;

namespace ClientTestLibrary.Linq
{
    class TestLinqPartitioningOperators
    {
        public static void Test(Assert assert)
        {
            assert.Expect(8);

            var numbers = new[] { 1, 3, 5, 7, 9 };
            var firstTwo = numbers.Take(2).ToArray();
            assert.DeepEqual(firstTwo, new[] { 1, 3 }, "Take() the first two array elements");

            var lastThree = numbers.TakeFromLast(3).ToArray();
            assert.DeepEqual(lastThree, new[] { 5, 7, 9 }, "TakeFromLast() the last three array elements");

            var exceptTwoLast = numbers.TakeExceptLast(2).ToArray();
            assert.DeepEqual(exceptTwoLast, new[] { 1, 3, 5 }, "TakeExceptLast() the first array elements except the last two");

            var takeWhileLessTwo = numbers.TakeWhile((number) => number < 2).ToArray();
            assert.DeepEqual(takeWhileLessTwo, new[] { 1 }, "TakeWhile() less two");

            var takeWhileSome = numbers.TakeWhile((number, index) => number - index <= 4).ToArray();
            assert.DeepEqual(takeWhileSome, new[] { 1, 3, 5, 7 }, "TakeWhile() by value and index");

            var skipThree = numbers.Skip(3).ToArray();
            assert.DeepEqual(skipThree, new[] { 7, 9 }, "Skip() the first three");

            var skipWhileLessNine = numbers.SkipWhile(number => number < 9).ToArray();
            assert.DeepEqual(skipWhileLessNine, new[] { 9 }, "SkipWhile() less then 9");

            var skipWhileSome = numbers.SkipWhile((number, index) => number <= 3 && index < 2 ).ToArray();
            assert.DeepEqual(skipWhileSome, new[] { 5, 7, 9 }, "SkipWhile() by value and index");
        }
    }
}
