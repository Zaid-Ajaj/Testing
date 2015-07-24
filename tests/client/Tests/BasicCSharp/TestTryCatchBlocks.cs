﻿using Bridge;
using Bridge.QUnit;
using System;

#pragma warning disable 162	// CS0162: Unreachable code detected. Disable because we want to assert that code does not reach unreachable parts

namespace ClientTestLibrary
{
    // Tests try and catch blocks
    class TestTryCatchBlocks
    {
        #region Tests

        //[#84] Does not compile
        public static void SimpleTryCatch(Assert assert)
        {
            assert.Expect(1);

            var result = TryCatch("Good");

            assert.Equal(result, "Good", "TryCatch() executes");
        }

        public static void CaughtExceptions(Assert assert)
        {
            assert.Expect(3);

            TryCatchWithCaughtException();
            assert.Ok(true, "Exception catch");

            TryCatchWithCaughtTypedException();
            assert.Ok(true, "Typed exception catch");

            var exceptionMessage = TryCatchWithCaughtArgumentException();
            assert.DeepEqual(exceptionMessage, "catch me", "Typed exception catch with exception message");
        }

        public static void ThrownExceptions(Assert assert)
        {
            assert.Expect(12);

            //#230
            assert.Throws(TryCatchWithNotCaughtTypedException, "catch me", "A.Typed exception is not Caught");
            assert.Ok(IsATry, "A. exception not caught - try section called");
            assert.Ok(!IsACatch, "A. exception not caught - catch section not called");

            //#229
            assert.Throws(TryCatchWithNotCaughtTypedExceptionAndArgument, "catch me", "[#229] B. Typed exception is not Caught; and argument");
            assert.Ok(IsBTry, "[#229] B. exception not caught - try section called");
            assert.Ok(!IsBCatch, "B. exception not caught - catch section not called");

            //#231
            assert.Throws(TryCatchWithRethrow, "catch me", "[#231] C. Rethrow");
            assert.Ok(IsCTry, "C. exception caught and re-thrown - try section called");
            assert.Ok(IsCCatch, "C. exception caught and re-thrown - catch section called");

            assert.Throws(TryCatchWithRethrowEx, new Func<object, bool>((error) => { return error.ToString() == "catch me"; }), "D. Rethrow with parameter");
            assert.Ok(IsDTry, "D. exception caught and re-thrown  - try section called");
            assert.Ok(IsDCatch, "D. exception caught and re-thrown  - catch section called");
        }

        public static void Bridge320(Assert assert)
        {
            assert.Expect(1);

            string exceptionMessage = string.Empty;

            try
            {
                Script.Write("\"someString\".SomeNotExistingMethod();");
            }
            catch (Exception ex)
            {
                exceptionMessage = ex.Message;
            }

            assert.Equal(exceptionMessage, "\"someString\".SomeNotExistingMethod is not a function", "ex.Message works on built-in JavaScript type");
        }

        #endregion Tests

        private static string TryCatch(string s)
        {
            try
            {
                return s;
            }
            catch
            {
                return string.Empty;
            }
        }

        #region CaughtExceptions

        private static void TryCatchWithCaughtException()
        {
            try
            {
                throw new Exception();
            }
            catch
            {
            }
        }

        private static void TryCatchWithCaughtTypedException()
        {
            try
            {
                throw new Exception();
            }
            catch (Exception)
            {
            }
        }

        private static string TryCatchWithCaughtArgumentException()
        {
            try
            {
                throw new ArgumentException("catch me");
            }
            catch (ArgumentException ex)
            {
                return ex.Message;
            }
        }

        #endregion CaughtExceptions

        #region ThrownExceptions

        public static bool IsATry { get; set; }
        public static bool IsACatch { get; set; }

        private static void TryCatchWithNotCaughtTypedException()
        {
            IsATry = false;
            IsACatch = false;

            try
            {
                IsATry = true;
                throw new Exception("catch me");
            }
            catch (ArgumentException)
            {
                IsATry = true;
            }

            IsATry = false;
        }

        public static bool IsBTry { get; set; }
        public static bool IsBCatch { get; set; }

        private static void TryCatchWithNotCaughtTypedExceptionAndArgument()
        {
            IsBTry = false;
            IsBCatch = false;

            try
            {
                IsBTry = true;
                throw new Exception("catch me");
                IsBTry = false;
            }
            catch (InvalidCastException ex)
            {
                IsBCatch = true;
                var s = ex.Message;
            }

            IsBTry = false;
        }

        public static bool IsCTry { get; set; }
        public static bool IsCCatch { get; set; }

        private static void TryCatchWithRethrow()
        {
            IsCTry = false;
            IsCCatch = false;

            try
            {
                IsCTry = true;
                throw new InvalidOperationException("catch me");
                IsCTry = false;
            }
            catch (Exception)
            {
                IsCCatch = true;
                throw;
            }

            IsCTry = false;
        }

        public static bool IsDTry { get; set; }
        public static bool IsDCatch { get; set; }

        private static void TryCatchWithRethrowEx()
        {
            IsDTry = false;
            IsDCatch = false;

            try
            {
                IsDTry = true;
                throw new ArgumentException("catch me");
                IsDTry = false;
            }
            catch (Exception ex)
            {
                IsDCatch = true;
                throw ex;
            }

            IsDTry = false;
        }

        #endregion ThrownExceptions
    }
}
