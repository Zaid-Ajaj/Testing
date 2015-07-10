﻿/// <reference path="..\..\www\qunit\qunit.d.ts" />
/// <reference path="..\..\www\typescript\bridge.d.ts" />
/// <reference path="..\..\www\typescript\functions.d.ts" />

QUnit.module("TypeScript - Functions");
QUnit.test("Parameters", function (assert) {
    var func = new Functions.Parameters();

    assert.expect(0);

    // TODO Bridge/#292
    // QUnit.deepEqual(func.getSomething(), 5, "Default parameter #292");
    //function buildName(firstName: string, lastName = "Smith") {
    //    // JavaScript added for the default parameter
    //    // if (typeof lastName === "undefined") { lastName = "Smith"; }
    //    return firstName + " " + lastName;
    //}
    //var result1 = buildName("Bob");
});

QUnit.test("Function types", function (assert) {
    var d = new Functions.DelegateClass();

    var ds: string;
    var di: number;
    d.methodVoidDelegate = () => di = 7;
    d.methodStringDelegate = (s: string) => ds = s;
    d.methodStringDelegateIntResult = (s: string) => s.length;

    d.methodVoidDelegate();
    QUnit.deepEqual(di, 7, "methodVoidDelegate");

    d.methodStringDelegate("Privet");
    QUnit.deepEqual(ds, "Privet", "methodStringDelegate");

    QUnit.deepEqual(d.methodStringDelegateIntResult("Privet"), 6, "methodStringDelegateIntResult");
});