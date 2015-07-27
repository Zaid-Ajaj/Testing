﻿/* global Bridge */

/**
 * @class ClientTestLibrary.TestDateFunctions
 */
Bridge.define('ClientTestLibrary.TestDateFunctions', {
    statics: {
        /**
         * @static
         * @public
         * @this ClientTestLibrary.TestDateFunctions
         * @memberof ClientTestLibrary.TestDateFunctions
         * @param   {Bridge.QUnit.Assert}    assert    
         * @return  {void}                             
         */
        dateTimes: function (assert) {
            assert.expect(2);

            // TEST
            //[#83] by C#
            var str = "2015-03-24T10:48:09.1500225+03:00";
            var bridgeDate = Bridge.Date.parse(str);
            var bridgeDate1 = new Date(str);

            assert.deepEqual(bridgeDate, bridgeDate1, "[#83] C# bridgeDate = bridgeDate1");

            // TEST
            //[#83] by JavaScript code. This is to check the same issue as above and just to check another way of calling QUnit from JavaScript
            var str = "2015-03-24T10:48:09.1500225+03:00",
    bridgeDate = Bridge.Date.parse(str),
    jsDate = new Date(Date.parse(str)),
    format = "yyyy-MM-dd hh:mm:ss";
    assert.deepEqual(Bridge.Date.format(bridgeDate, format), Bridge.Date.format(jsDate, format), "[#83] js");
        },
        /**
         * @static
         * @public
         * @this ClientTestLibrary.TestDateFunctions
         * @memberof ClientTestLibrary.TestDateFunctions
         * @param   {Bridge.QUnit.Assert}    assert    
         * @return  {void}                             
         */
        bridge329: function (assert) {
            assert.expect(5);

            var d1 = { };
            var b1 = Bridge.Date.tryParse("2001-01-01", null, d1);
            assert.ok(b1, "TryParse parsed '2001 - 01 - 01'");
            assert.equal(d1.v.getFullYear(), 2001, "TryParse works Year");
            assert.equal(d1.v.getMonth() + 1, 1, "TryParse works Month");
            assert.equal(d1.v.getDate(), 1, "TryParse works Day");

            var d2 = Bridge.Date.parse("2001-01-01");
            assert.deepEqual(d2.toString(), d1.v.toString(), "TryParse And Parse give the same result");

        }
    }
});

