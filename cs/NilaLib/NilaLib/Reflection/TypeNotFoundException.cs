﻿/*
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
    using System.Reflection;

    /// <summary>
    /// 型が見つからない例外を表します。
    /// </summary>
    public class TypeNotFoundException : Exception
    {
        /// <summary>
        /// 型名とアセンブリを指定して、NilaLib.Reflection.TypeNotFoundException クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="typeName">型名。</param>
        /// <param name="assembly">アセンブリ情報。</param>
        public TypeNotFoundException(string typeName, Assembly assembly)
            : base(string.Format("型名 {0} が {1} から見つかりませんでした。", typeName, assembly.Location))
        {
            this.TypeName = typeName;
            this.Assembly = assembly;
        }

        /// <summary>
        /// 型名を取得します。
        /// </summary>
        public string TypeName { get; private set; }

        /// <summary>
        /// アセンブリを取得します。
        /// </summary>
        public Assembly Assembly { get; private set; }
    }
}
