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
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// アセンブリ情報を表します。
    /// </summary>
    public class AssemblyInformation
    {
        /// <summary>
        /// アセンブリ本体。
        /// </summary>
        private readonly Assembly assembly;

        /// <summary>
        /// アセンブリを指定して、NilaLib.Reflection.AssemblyInformation クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="assembly">アセンブリ。</param>
        internal AssemblyInformation(Assembly assembly)
        {
            this.assembly = assembly;
        }

        /// <summary>
        /// アセンブリに格納される全ての型情報を返します。
        /// </summary>
        /// <returns>アセンブリに格納される全ての型情報。</returns>
        public Type[] GetTypes()
        {
            return this.assembly.GetTypes();
        }

        /// <summary>
        /// アセンブリを含まない型の完全修飾名を指定して、一致する型情報を返します。
        /// </summary>
        /// <param name="fullname">アセンブリを含まない型の完全修飾名。</param>
        /// <returns>fullname と一致する型情報。一致する型が無い場合は null。</returns>
        public Type GetTypeFromFullName(string fullname)
        {
            return this.GetTypes().FirstOrDefault(x => x.FullName == fullname);
        }

        /// <summary>
        /// 型名を指定して、一致する型情報を返します。
        /// </summary>
        /// <param name="name">型名。</param>
        /// <returns>name と一致する型情報。一致する型が無い場合は null。</returns>
        public Type GetTypeFromName(string name)
        {
            return this.GetTypes().FirstOrDefault(x => x.Name == name);
        }

        /// <summary>
        /// 指定された型のインスタンスを作成て返します。
        /// </summary>
        /// <param name="type">作成するインスタンスの型。</param>
        /// <param name="parameters">コンストラクタに渡す引数。</param>
        /// <returns>作成したインスタンス。</returns>
        public object CreateInstance(Type type, params object[] parameters)
        {
            return Reflector.CreateInstance(type, parameters);
        }

        /// <summary>
        /// 型名を指定して、一致する型のインスタンスを作成して返します。
        /// </summary>
        /// <param name="name">型名。</param>
        /// <param name="parameters">コンストラクタに渡す引数。</param>
        /// <returns>作成したインスタンス。</returns>
        public object CreateInstanceFromTypeName(string name, params object[] parameters)
        {
            var type = this.GetTypeFromName(name);
            if (type == null)
            {
                throw new TypeNotFoundException(name, this.assembly);
            }

            return this.CreateInstance(type, parameters);
        }

        /// <summary>
        /// アセンブリを含まない型の完全修飾名を指定して、一致する型のインスタンスを作成して返します。
        /// </summary>
        /// <param name="name">アセンブリを含まない型の完全修飾名。</param>
        /// <param name="parameters">コンストラクタに渡す引数。</param>
        /// <returns>作成したインスタンス。</returns>
        public object CreateInstanceFromTypeFullName(string name, params object[] parameters)
        {
            var type = this.GetTypeFromFullName(name);
            if (type == null)
            {
                throw new TypeNotFoundException(name, this.assembly);
            }

            return this.CreateInstance(type, parameters);
        }
    }
}
