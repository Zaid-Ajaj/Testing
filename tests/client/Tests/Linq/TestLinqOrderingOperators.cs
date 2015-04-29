﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bridge;
using Bridge.QUnit;

using ClientTestLibrary.Utilities;

namespace ClientTestLibrary.Linq
{
    class TestLinqOrderingOperators
    {
        public static void Test(Assert assert)
        {
            assert.Expect(8);

            var words = new string[] { "ab2", "ac", "a", "ab12", "", "ab", "bac", "z"};
            var sortedWords = (from word in words orderby word select word).ToArray();
            assert.DeepEqual(sortedWords, new[] {"", "a", "ab", "ab12", "ab2", "ac", "bac", "z" }, "Order by words.");

            var sortedWordsByLength = (from word in words orderby word.Length select word).ToArray();
            assert.DeepEqual(sortedWordsByLength, new[] { "", "a", "z", "ac", "ab", "ab2", "bac", "ab12" }, "Order by word length.");

            var sortedPersonsByName = (from p in Person.GetPersons() orderby p.Name select p.Name).ToArray();
            assert.DeepEqual(sortedPersonsByName, new[] { "Billy", "Dora", "Frank", "Ian", "John", "Mary", "Nemo", "Zeppa" }, "Order by person names.");

            //TODO test with System.StringComparison

            var doubles = new double[] { 1.0, -0.7, 2.1, 0.9, 1.4, 2.9 };
            var sortedDoubles = (from d in doubles orderby d descending select d).ToArray();
            assert.DeepEqual(sortedDoubles, new[] { 2.9, 2.1, 1.4, 1.0, 0.9, -0.7 }, "Order by descending double.");

            var sortedPersonsByCountDesc = (from p in Person.GetPersons() orderby p.Count descending select p.Count).ToArray();
            assert.DeepEqual(sortedPersonsByCountDesc, new[] { 3000, 700, 700, 550, 500, 300, 100, 50 }, "Order by person count descending.");

            var sortedWordsByLengthAndLetters = (from word in words orderby word.Length, word select word).ToArray();
            assert.DeepEqual(sortedWordsByLengthAndLetters, new[] { "", "a", "z", "ab", "ac", "ab2", "bac","ab12" }, "Order by word length then by letters.");

            var sortedWordsByLengthAndLettersLambda = words.OrderBy(x => x.Length).ThenByDescending(x => x).ToArray();
            assert.DeepEqual(sortedWordsByLengthAndLettersLambda, new[] {  "", "z", "a", "ac", "ab", "bac", "ab2", "ab12" }, "Order by word length then by letters as lambda");

            //var numbers = new[] { 2, 4, 6, 1, 5, 7, 9, 0, 8, 3};
            var numbers = new[] { 2, 4, 6, 1, 5 };
            var numbersReversed = numbers.Reverse<int>().ToArray();
            assert.DeepEqual(numbersReversed, new[] { 5, 1, 6, 4, 2 }, "Reverse() numbers.");


        }
    }
}
