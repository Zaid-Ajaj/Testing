﻿using System;
using System.Collections.Generic;
using System.Linq;
using Bridge;
using Bridge.QUnit;

using ClientTestLibrary.Utilities;

namespace ClientTestLibrary.Linq
{
    class TestLinqQuantifiers
    {
        public static void Test(Assert assert)
        {
            assert.Expect(4);

            string[] words = { "count", "tree", "mount", "five", "doubt" };
            bool anyOu = words.Any(w => w.Contains("ou"));
            assert.Ok(anyOu, "Any() to return words containing 'ou'.");

            int[] oddNumbers = { 3, 7, 9, 5, 247, 1000001 };
            bool onlyOdd = oddNumbers.All(n => n % 2 == 1);
            assert.Ok(onlyOdd, "All() is odd.");

            int[] someNumbers = { 2, 3, 7, 9, 5, 247, 1000001 };
            bool notOnlyOdd = !someNumbers.All(n => n % 2 == 1);
            assert.Ok(notOnlyOdd, "All() is not only odd.");

            var productGroups =
                    (from p in Person.GetPersons()
                     group p by p.Group into pGroup
                     where pGroup.Any(p => p.Count >= 500)
                     select new { Group = pGroup.Key, Names = pGroup.Select(x => x.Name).ToArray() }).ToArray();

            object[] productGroupsExpected = { new {Group = "C", Names = new[]{"Zeppa", "Billy"}},
                                                 new {Group = "B", Names = new[]{"John", "Dora", "Ian", "Mary"}},
                                                 new {Group = (string)null, Names = new[]{"Nemo"}}
                                             };

            assert.DeepEqual(productGroups, productGroupsExpected, "Any() to return a grouped array of names only for groups having any item with Count > 500.");
        }
    }
}
