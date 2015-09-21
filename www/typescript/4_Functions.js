﻿/// <reference path="..\..\www\qunit\qunit.d.ts" />
/// <reference path="..\..\www\typescriptjs\bridge.d.ts" />
/// <reference path="..\..\www\typescriptjs\functions.d.ts" />
QUnit.module("TypeScript - Functions");
QUnit.test("Parameters", function (assert) {
    var func = new Functions.Parameters();

    // TODO Bridge/#292
    // QUnit.deepEqual(func.getSomething(), 5, "Default parameter #292");
    //function buildName(firstName: string, lastName = "Smith") {
    //    // JavaScript added for the default parameter
    //    // if (typeof lastName === "undefined") { lastName = "Smith"; }
    //    return firstName + " " + lastName;
    //}
    //var result1 = buildName("Bob");
    // #293
    QUnit.deepEqual(func.join([1, 2, 3]), "123", "params argument becomes Array #293");
});

QUnit.test("Function types", function (assert) {
    var d = new Functions.DelegateClass();

    var ds;
    var di;

    d.methodVoidDelegate = function () {
        return di = 7;
    };
    d.methodVoidDelegate();
    QUnit.deepEqual(di, 7, "methodVoidDelegate");

    d.methodStringDelegate = function (s) {
        return ds = s;
    };
    d.methodStringDelegate("Privet");
    QUnit.deepEqual(ds, "Privet", "methodStringDelegate");

    d.methodStringDelegateIntResult = function (s) {
        return s.length;
    };
    QUnit.deepEqual(d.methodStringDelegateIntResult("Privet"), 6, "methodStringDelegateIntResult");
});
