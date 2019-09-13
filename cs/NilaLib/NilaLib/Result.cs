/*
 * The MIT License (MIT)
 *
 * Copyright (c) 2018-2019 Niumlaque
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

namespace NilaLib
{
    using System;
    using System.ComponentModel;

    /// <summary>
    /// 処理の結果を表します。
    /// </summary>
    /// <typeparam name="T">結果値の型。</typeparam>
    public struct Result<T>
    {
        /// <summary>
        /// 成功したかどうかを示す値を指定して、NilaLib.Result&lt;T&gt; 構造体を作成します。
        /// </summary>
        /// <param name="success">成功したかどうかを示す値。</param>
        public Result(bool success)
            : this(success, default(T), null)
        {
        }

        /// <summary>
        /// 成功したかどうかを示す値･結果値を指定して、NilaLib.Result&lt;T&gt; 構造体を作成します。
        /// </summary>
        /// <param name="success">成功したかどうかを示す値。</param>
        /// <param name="value">結果値。</param>
        public Result(bool success, T value)
            : this(success, value, null)
        {
        }

        /// <summary>
        /// 成功したかどうかを示す値･例外を指定して、NilaLib.Result&lt;T&gt; 構造体を作成します。
        /// </summary>
        /// <param name="success">成功したかどうかを示す値。</param>
        /// <param name="exception">例外。</param>
        public Result(bool success, Exception exception)
            : this(success, default(T), exception)
        {
        }

        /// <summary>
        /// 成功したかどうかを示す値･結果値･例外を指定して、NilaLib.Result&lt;T&gt; 構造体を作成します。
        /// </summary>
        /// <param name="success">成功したかどうかを示す値。</param>
        /// <param name="value">結果値。</param>
        /// <param name="exception">例外。</param>
        public Result(bool success, T value, Exception exception)
            : this()
        {
            this.Success = success;
            this.Value = value;
            this.Exception = exception;
        }

        /// <summary>
        /// 対象の結果が成功したかどうかを返します。
        /// </summary>
        /// <param name="obj">処理の結果。</param>
        /// <returns>対象の結果が成功したかどうかを示す値。</returns>
        public static bool operator true(Result<T> obj)
        {
            return obj.Success;
        }

        /// <summary>
        /// 対象の結果が失敗したかどうかを返します。
        /// </summary>
        /// <param name="obj">処理の結果。</param>
        /// <returns>対象の結果が失敗したかどうかを示す値。</returns>
        public static bool operator false(Result<T> obj)
        {
            return !obj.Success;
        }

        /// <summary>
        /// 対象の成功結果を反転した値を返します。
        /// </summary>
        /// <param name="obj">処理の結果。</param>
        /// <returns>対象の成功結果を反転した値。</returns>
        public static bool operator !(Result<T> obj)
        {
            return !obj.Success;
        }

        /// <summary>
        /// NilaLib.Result と等しい NilaLib.Result&lt;T&gt; 構造体を作成します。
        /// </summary>
        /// <param name="obj">変換対象の NilaLib.Result。</param>
        /// <returns>NilaLib.Result を変換した NilaLib.Result&lt;T&gt;。</returns>
        public static implicit operator Result<T>(Result obj)
        {
            return new Result<T>(obj.Success, default(T), obj.Exception);
        }

        /// <summary>
        /// NilaLib.Result&lt;Result.Any&gt; から NilaLib.Result&lt;T&gt; 構造体を作成します。
        /// </summary>
        /// <param name="obj">変換対象の NilaLib.Result&lt;Result.Any&gt;。</param>
        /// <returns>NilaLib.Result&lt;Result.Any&gt; を変換した NilaLib.Result&lt;T&gt;。</returns>
        public static implicit operator Result<T>(Result<Result.Any> obj)
        {
            return new Result<T>(obj.Success, default(T), obj.Exception);
        }

        /// <summary>
        /// 成功したかどうかを示す値を取得します。
        /// </summary>
        public bool Success { private set; get; }

        /// <summary>
        /// 結果値を取得します。結果値が指定されていない場合はデフォルト値を返します。
        /// </summary>
        public T Value { private set; get; }

        /// <summary>
        /// 例外を取得します。例外が指定されていない場合は null を返します。
        /// </summary>
        public Exception Exception { private set; get; }

        /// <summary>
        /// 値を新しい値に変換します。
        /// </summary>
        /// <typeparam name="T">変換前の型。</typeparam>
        /// <typeparam name="TResult">solver によって返される値の型。</typeparam>
        /// <param name="source">変換前の値。</param>
        /// <param name="solver">値に適用する変換関数。</param>
        /// <returns>結果値に対して変換関数を呼び出した結果として得られる NilaLib.Solver&lt;T,TResult&gt;。</returns>
        public Solver<T, TResult> Solve<TResult>(Func<T, TResult> solver)
        {
            return new Solver<T, TResult>(this.Value, solver);
        }

        /// <summary>
        /// このインスタンスと指定した NilaLib.Result&lt;T&gt; が等しいかどうかを示します。
        /// </summary>
        /// <param name="obj">比較対象のもう 1 つのオブジェクト。</param>
        /// <returns>obj とこのインスタンスが同じ型で、同じ値を表している場合は true。それ以外の場合は false。</returns>
        public bool Equals(Result<T> obj)
        {
            return this.Success == obj.Success && this.Value.Equals(obj.Value);
        }

        /// <summary>
        /// このインスタンスと指定したオブジェクトが等しいかどうかを示します。
        /// </summary>
        /// <param name="obj">比較対象のもう 1 つのオブジェクト。</param>
        /// <returns>obj とこのインスタンスが同じ型で、同じ値を表している場合は true。それ以外の場合は false。</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
            {
                return false;
            }

            return this.Equals((Result<T>)obj);
        }

        /// <summary>
        /// 対象のインスタンスのハッシュコードを返します。
        /// </summary>
        /// <returns>このインスタンスのハッシュコードである 32 ビット符号付き整数。</returns>
        public override int GetHashCode()
        {
            var hash = 283;
            hash ^= this.Success.GetHashCode();
            hash ^= this.Value != null ? this.Value.GetHashCode() : 997;
            hash ^= this.Exception != null ? this.Exception.GetHashCode() : 733;
            return hash;
        }

        /// <summary>
        /// 結果値を返します。結果が失敗している場合は failure で指定した値を返します。
        /// </summary>
        /// <param name="failure">結果が失敗している場合に戻す値。</param>
        /// <returns>結果値。失敗している場合は failure で指定された値。</returns>
        public T GetValue(T failure = default(T))
        {
            if (this.Success)
            {
                return this.Value;
            }

            return failure;
        }

        /// <summary>
        /// 使用しないでください。
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new static bool Equals(object objA, object objB)
        {
            return object.Equals(objA, objB);
        }

        /// <summary>
        /// 使用しないでください。
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new static bool ReferenceEquals(object objA, object objB)
        {
            return object.ReferenceEquals(objA, objB);
        }

        /// <summary>
        /// 現在の NilaLib.Result&lt;T&gt; を表す文字列を返します。
        /// </summary>
        /// <returns>現在の NilaLib.Result&lt;T&gt; を表す文字列。</returns>
        public override string ToString()
        {
            return string.Format("Success={0},Type={1},Value={2}", this.Success, typeof(T).FullName, this.Value);
        }
    }

    /// <summary>
    /// 処理の結果を表します。
    /// </summary>
    public struct Result
    {
        /// <summary>
        /// 何も示さないことを表します。
        /// </summary>
        public enum Any { }

        /// <summary>
        /// 指定された処理を実行し、成功したかどうかを表す NilaLib.Result 構造体を返します。
        /// </summary>
        /// <param name="f">実行する System.Action デリゲート。</param>
        /// <returns>成功したかどうかを表す NilaLib.Result 構造体。</returns>
        public static Result Try(Action f)
        {
            try
            {
                f();
                return Result.Succeeded();
            }
            catch (Exception ex)
            {
                return Result.Failed(ex);
            }
        }

        /// <summary>
        /// 指定された処理を実行し、成功したかどうかを表す NilaLib.Result&lt;T&gt; 構造体を返します。
        /// </summary>
        /// <typeparam name="T">戻り値の型。</typeparam>
        /// <param name="f">実行する System.Func&lt;T&gt; デリゲート。</param>
        /// <returns>成功したかどうかを表す NilaLib.Result&lt;T&gt; 構造体。</returns>
        public static Result<T> Try<T>(Func<T> f)
        {
            try
            {
                return Result.Succeeded(f());
            }
            catch (Exception ex)
            {
                return Result.Failed(ex);
            }
        }

        /// <summary>
        /// 成功を表す NilaLib.Result 構造体を返します。
        /// </summary>
        /// <returns>成功を表す NilaLib.Result 構造体。</returns>
        public static Result Succeeded()
        {
            return new Result(true);
        }

        /// <summary>
        /// 指定した結果値を使用して、成功を表す NilaLib.Result&lt;T&gt; 構造体を返します。
        /// </summary>
        /// <typeparam name="T">結果値の型。</typeparam>
        /// <param name="value">結果値。</param>
        /// <returns>成功を表す NilaLib.Result&lt;T&gt; 構造体。</returns>
        public static Result<T> Succeeded<T>(T value)
        {
            return new Result<T>(true, value);
        }

        /// <summary>
        /// 失敗を表す NilaLib.Result 構造体を返します。
        /// </summary>
        /// <returns>失敗を表す NilaLib.Result 構造体。</returns>
        public static Result Failed()
        {
            return new Result(false);
        }

        /// <summary>
        /// 指定した例外を使用して、失敗を表す NilaLib.Result 構造体を返します。
        /// </summary>
        /// <param name="exception">例外。</param>
        /// <returns>失敗を表す NilaLib.Result 構造体。</returns>
        public static Result Failed(Exception exception)
        {
            return new Result(false, exception);
        }

        /// <summary>
        /// 指定したエラーメッセージ使用して、失敗を表す NilaLib.Result 構造体を返します。
        /// </summary>
        /// <param name="message">エラーメッセージ。</param>
        /// <returns>失敗を表す NilaLib.Result 構造体。</returns>
        public static Result Failed(string message)
        {
            return new Result(false, new Exception(message));
        }

        /// <summary>
        /// 指定したエラーメッセージと、この例外の原因である内部例外への参照を使用して、失敗を表す NilaLib.Result 構造体を返します。
        /// </summary>
        /// <param name="message">エラーメッセージ。</param>
        /// <param name="innerException">現在の例外の原因である例外。</param>
        /// <returns>失敗を表す NilaLib.Result 構造体。</returns>
        public static Result Failed(string message, Exception innerException)
        {
            return new Result(false, new Exception(message, innerException));
        }

        /// <summary>
        /// 使用しないでください。
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new static bool Equals(object objA, object objB)
        {
            return object.Equals(objA, objB);
        }

        /// <summary>
        /// 使用しないでください。
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new static bool ReferenceEquals(object objA, object objB)
        {
            return object.ReferenceEquals(objA, objB);
        }

        /// <summary>
        /// 成功したかどうかを示す値を指定して、NilaLib.Result 構造体を作成します。
        /// </summary>
        /// <param name="success">成功したかどうかを示す値。</param>
        public Result(bool success)
            : this(success, null)
        {
        }

        /// <summary>
        /// 成功したかどうかを示す値･例外を指定して、NilaLib.Result 構造体を作成します。
        /// </summary>
        /// <param name="success">成功したかどうかを示す値。</param>
        /// <param name="exception">例外。</param>
        public Result(bool success, Exception exception)
            : this()
        {
            this.Success = success;
            this.Exception = exception;
        }

        /// <summary>
        /// NilaLib.Result&lt;Result.Any&gt; と等しい NilaLib.Result 構造体を作成します。
        /// </summary>
        /// <param name="obj">変換対象の NilaLib.Result&lt;Result.Any&gt;。</param>
        /// <returns>NilaLib.Result&lt;Result.Any&gt; を変換した NilaLib.Result。</returns>
        public static implicit operator Result(Result<Any> obj)
        {
            return new Result(obj.Success, obj.Exception);
        }

        /// <summary>
        /// 対象の結果が成功したかどうかを返します。
        /// </summary>
        /// <param name="obj">処理の結果。</param>
        /// <returns>対象の結果が成功したかどうかを示す値。</returns>
        public static bool operator true(Result obj)
        {
            return obj.Success;
        }

        /// <summary>
        /// 対象の結果が失敗したかどうかを返します。
        /// </summary>
        /// <param name="obj">処理の結果。</param>
        /// <returns>対象の結果が失敗したかどうかを示す値。</returns>
        public static bool operator false(Result obj)
        {
            return !obj.Success;
        }

        /// <summary>
        /// 対象の成功結果を反転した値を返します。
        /// </summary>
        /// <param name="obj">処理の結果。</param>
        /// <returns>対象の成功結果を反転した値。</returns>
        public static bool operator !(Result obj)
        {
            return !obj.Success;
        }

        /// <summary>
        /// 成功したかどうかを示す値を取得します。
        /// </summary>
        public bool Success { private set; get; }

        /// <summary>
        /// 例外を取得します。例外が指定されていない場合は null を返します。
        /// </summary>
        public Exception Exception { private set; get; }

        /// <summary>
        /// このインスタンスと指定した NilaLib.Result が等しいかどうかを示します。
        /// </summary>
        /// <param name="obj">比較対象のもう 1 つのオブジェクト。</param>
        /// <returns>obj とこのインスタンスが同じ型で、同じ値を表している場合は true。それ以外の場合は false。</returns>
        public bool Equals(Result obj)
        {
            return this.Success == obj.Success;
        }

        /// <summary>
        /// このインスタンスと指定したオブジェクトが等しいかどうかを示します。
        /// </summary>
        /// <param name="obj">比較対象のもう 1 つのオブジェクト。</param>
        /// <returns>obj とこのインスタンスが同じ型で、同じ値を表している場合は true。それ以外の場合は false。</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
            {
                return false;
            }

            return this.Equals((Result)obj);
        }

        /// <summary>
        /// 対象のインスタンスのハッシュコードを返します。
        /// </summary>
        /// <returns>このインスタンスのハッシュコードである 32 ビット符号付き整数。</returns>
        public override int GetHashCode()
        {
            var hash = 283;
            hash ^= this.Success.GetHashCode();
            hash ^= this.Exception != null ? this.Exception.GetHashCode() : 733;
            return hash;
        }

        /// <summary>
        /// 現在の NilaLib.Result を表す文字列を返します。
        /// </summary>
        /// <returns>現在の NilaLib.Result を表す文字列。</returns>
        public override string ToString()
        {
            return string.Format("Success={0}", this.Success);
        }
    }

    /// <summary>
    /// TSource に対する変換処理を保持します。
    /// </summary>
    /// <typeparam name="TSource">結果の型。</typeparam>
    /// <typeparam name="TResult">処理後の型。</typeparam>
    public class Solver<TSource, TResult>
    {
        /// <summary>
        /// 変換対象と変換関数を指定して、NilaLib.Solver&lt;TSource,TResult&gt; クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="source">変換関数を呼び出す対象となる値。</param>
        /// <param name="solver">値に適用する変換関数。</param>
        internal Solver(TSource source, Func<TSource, TResult> solver)
        {
            this.source = source;
            this.solver = solver;
        }

        /// <summary>
        /// 値を新しい値に変換します。
        /// </summary>
        /// <typeparam name="TResult2">solver によって返される値の型。</typeparam>
        /// <param name="solver">値に適用する変換関数。</param>
        /// <returns>source の値に対して変換関数を呼び出した結果として得られる NilaLib.Solver&lt;TSource,TResult2&gt;。</returns>
        public Solver<TSource, TResult2> Solve<TResult2>(Func<TResult, TResult2> solver)
        {
            return new Solver<TSource, TResult2>(this.source, SolveHelper.CombineSolvers<TSource, TResult, TResult2>(this.solver, solver));
        }

        /// <summary>
        /// 変換関数を適用した結果の値を表す NilaLib.Result&lt;T&gt; を返します。
        /// </summary>
        /// <returns>変換関数を適用した結果の値を表す NilaLib.Result&lt;T&gt;。</returns>
        public Result<TResult> Execute()
        {
            try
            {
                return Result.Succeeded(this.solver(this.source));
            }
            catch (Exception ex)
            {
                return Result.Failed(ex);
            }
        }

        private TSource source;
        private Func<TSource, TResult> solver;
    }

    internal static class SolveHelper
    {
        internal static Func<TSource, TResult> CombineSolvers<TSource, TMiddle, TResult>(Func<TSource, TMiddle> solver1, Func<TMiddle, TResult> solver2)
        {
            return x => solver2(solver1(x));
        }
    }

    namespace Extensions
    {
        /// <summary>
        /// NilaLib.Result を補助するメソッドを提供します。
        /// </summary>
        public static class ResultExtensions
        {
            /// <summary>
            /// 値を新しい値に変換します。
            /// </summary>
            /// <typeparam name="TSource">変換前の型。</typeparam>
            /// <typeparam name="TResult">solver によって返される値の型。</typeparam>
            /// <param name="source">変換前の値。</param>
            /// <param name="solver">値に適用する変換関数。</param>
            /// <returns>source の値に対して変換関数を呼び出した結果として得られる NilaLib.Solver&lt;TSource,TResult&gt;。</returns>
            public static Solver<TSource, TResult> Solve<TSource, TResult>(this TSource source, Func<TSource, TResult> solver)
            {
                return new Solver<TSource, TResult>(source, solver);
            }
        }
    }
}
