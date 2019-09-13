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
    /// フィールドのリフレクション時に発生した例外を表します。
    /// </summary>
    public class FieldReflectionException : ReflectionException
    {
        /// <summary>
        /// 例外メッセージとこの例外の原因である内部例外を指定して、NilaLib.Reflection.FieldReflectionException クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="message">例外メッセージ。</param>
        /// <param name="innerException">この例外の原因である内部例外。</param>
        protected FieldReflectionException(string message, Exception innerException = null)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// 型情報を取得します。
        /// </summary>
        public Type Type { get; private set; }

        /// <summary>
        /// フィールド名を取得します。
        /// </summary>
        public string FieldName { get; private set; }

        /// <summary>
        /// リフレクション時に対象のフィールドが見つからなかったことを表す NilaLib.Reflection.FieldReflectionException クラスのインスタンスを作成して返します。
        /// </summary>
        /// <param name="type">型情報。</param>
        /// <param name="fieldName">フィールド名。</param>
        /// <returns>NilaLib.Reflection.FieldReflectionException クラスのインスタンス。</returns>
        public static FieldReflectionException NotFound(Type type, string fieldName)
        {
            var ex = new FieldReflectionException(string.Format("フィールド {0}.{1} が見つかりませんでした。", type.FullName, fieldName));
            ex.Type = type;
            ex.FieldName = fieldName;
            return ex;
        }

        /// <summary>
        /// リフレクション時に対象のフィールドが値の設定が可能なコレクションではなかったことを表す NilaLib.Reflection.FieldReflectionException クラスのインスタンスを作成して返します。
        /// </summary>
        /// <param name="type">型情報。</param>
        /// <param name="fieldName">フィールド名。</param>
        /// <returns>NilaLib.Reflection.FieldReflectionException クラスのインスタンス。</returns>
        public static FieldReflectionException CannotSetToCollection(Type type, string fieldName)
        {
            var ex = new FieldReflectionException(string.Format("フィールド {0}.{1} は値の設定が可能なコレクションではありませんでした。", type.FullName, fieldName));
            ex.Type = type;
            ex.FieldName = fieldName;
            return ex;
        }

        /// <summary>
        /// リフレクション時に対象のフィールドが null のため、コレクションとして参照できなかったことを表す NilaLib.Reflection.FieldReflectionException クラスのインスタンスを作成して返します。
        /// </summary>
        /// <param name="type">型情報。</param>
        /// <param name="fieldName">フィールド名。</param>
        /// <returns>NilaLib.Reflection.FieldReflectionException クラスのインスタンス。</returns>
        public static FieldReflectionException NullCollection(Type type, string fieldName)
        {
            var ex = new FieldReflectionException(string.Format("フィールド {0}.{1} は null のため、コレクションとして参照できません。", type.FullName, fieldName));
            ex.Type = type;
            ex.FieldName = fieldName;
            return ex;
        }
    }
}
