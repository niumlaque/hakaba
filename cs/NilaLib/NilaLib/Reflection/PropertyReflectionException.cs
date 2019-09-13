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
    /// プロパティのリフレクション時に発生した例外を表します。
    /// </summary>
    public class PropertyReflectionException : ReflectionException
    {
        /// <summary>
        /// 型情報とプロパティ名を指定して、NilaLib.Reflection.PropertyReflectionException クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="type">型情報。</param>
        /// <param name="propertyName">プロパティ名。</param>
        public PropertyReflectionException(Type type, string propertyName)
            : base(string.Format("プロパティ {0}.{1} が見つかりませんでした。", type.FullName, propertyName))
        {
            this.Type = type;
            this.PropertyName = propertyName;
        }

        /// <summary>
        /// 型情報を取得します。
        /// </summary>
        public Type Type { get; private set; }

        /// <summary>
        /// プロパティ名を取得します。
        /// </summary>
        public string PropertyName { get; private set; }
    }
}
