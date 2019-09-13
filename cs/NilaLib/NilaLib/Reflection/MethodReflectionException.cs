/*
 * The MIT License (MIT)
 *
 * Copyright (c) 2018 Niumlaque
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

namespace NilaLib.Reflection
{
    using System;

    /// <summary>
    /// メソッドのリフレクション時に発生した例外を表します。
    /// </summary>
    public class MethodReflectionException : ReflectionException
    {
        /// <summary>
        /// 例外メッセージとこの例外の原因である内部例外を指定して、NilaLib.Reflection.MethodReflectionException クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="message">例外メッセージ。</param>
        /// <param name="innerException">この例外の原因である内部例外。</param>
        protected MethodReflectionException(string message, Exception innerException = null)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// 型情報を取得します。
        /// </summary>
        public Type Type { get; private set; }

        /// <summary>
        /// メソッド名を取得します。
        /// </summary>
        public string MethodName { get; private set; }

        /// <summary>
        /// メソッドに渡された引数を取得します。
        /// </summary>
        public object[] Args { get; private set; }

        /// <summary>
        /// リフレクション時に対象のメソッドが見つからなかったことを表す NilaLib.Reflection.MethodReflectionException クラスのインスタンスを作成して返します。
        /// </summary>
        /// <param name="type">型情報。</param>
        /// <param name="methodName">メソッド名。</param>
        /// <param name="args">引数。</param>
        /// <returns>NilaLib.Reflection.MethodReflectionException クラスのインスタンス。</returns>
        public static MethodReflectionException NotFound(Type type, string methodName, object[] args)
        {
            var ex = new MethodReflectionException(string.Format("メソッド {0}.{1} が見つかりませんでした。", type.FullName, methodName));
            ex.Type = type;
            ex.MethodName = methodName;
            ex.Args = args;
            return ex;
        }

        /// <summary>
        /// リフレクションで取得したメソッドを呼び出し時にエラーが発生したことを表す NilaLib.Reflection.MethodReflectionException クラスの新しいインスタンスを作成して返します。
        /// </summary>
        /// <param name="type">型情報。</param>
        /// <param name="methodName">メソッド名。</param>
        /// <param name="args">引数。</param>
        /// <param name="innerException">この例外の原因である内部例外。</param>
        /// <returns>NilaLib.Reflection.MethodReflectionException クラスのインスタンス。</returns>
        public static MethodReflectionException InvokeError(Type type, string methodName, object[] args, Exception innerException)
        {
            var ex = new MethodReflectionException(string.Format("メソッド {0}.{1} の呼び出しに失敗しました。", type.FullName, methodName), innerException);
            ex.Type = type;
            ex.MethodName = methodName;
            ex.Args = args;
            return ex;
        }
    }
}
