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
    /// リフレクションに失敗した際の例外の基底クラスを表します。
    /// </summary>
    public class ReflectionException : Exception
    {
        /// <summary>
        /// 例外メッセージとこの例外の原因である内部例外を指定して、NilaLib.Reflection.ReflectionException クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="message">例外メッセージ。</param>
        /// <param name="innerException">この例外の原因である内部例外。</param>
        internal ReflectionException(string message, Exception innerException = null) : base(message, innerException)
        {
        }
    }
}
