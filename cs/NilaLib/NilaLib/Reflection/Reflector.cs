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
    /// リフレクション呼び出し機能の拡張メソッド及び補助メソッドを提供します。
    /// </summary>
    public static class Reflector
    {
        /// <summary>
        /// インスタンスメンバ取得用 BindingFlags。
        /// </summary>
        private static readonly BindingFlags InstanceAllFlags =
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

        /// <summary>
        /// 静的メンバ取得用 BindingFlags。
        /// </summary>
        private static readonly BindingFlags StaticAllFlags =
            BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

        #region implementation

        /// <summary>
        /// source インスタンスのメンバメソッド methodName を parameters を指定して呼び出します。
        /// </summary>
        /// <param name="source">呼び出すメソッドをメンバとして保持するインスタンス。</param>
        /// <param name="methodName">呼び出すメソッド名。</param>
        /// <param name="parameters">メソッドに渡すパラメータ。</param>
        /// <returns>メソッドの戻り値が void 型の場合は null。それ以外の場合はメソッドの戻り値。</returns>
        public static object Call(this object source, string methodName, params object[] parameters)
        {
            var m = source.GetType().GetMethod(methodName, Reflector.InstanceAllFlags, null, parameters.Select(x => x.GetType()).ToArray(), null);
            if (m == null)
            {
                throw MethodReflectionException.NotFound(source.GetType(), methodName, parameters);
            }

            try
            {
                return m.Invoke(source, parameters);
            }
            catch (Exception ex)
            {
                throw MethodReflectionException.InvokeError(source.GetType(), methodName, parameters, ex);
            }
        }

        /// <summary>
        /// type 型の静的メソッド methodName を parameters を指定して呼び出します。
        /// </summary>
        /// <param name="type">呼び出すメソッドを静的に保持する型。</param>
        /// <param name="methodName">呼び出すメソッド名。</param>
        /// <param name="parameters">メソッドに渡すパラメータ。</param>
        /// <returns>メソッドの戻り値が void 型の場合は null。それ以外の場合はメソッドの戻り値。</returns>
        public static object Call(Type type, string methodName, params object[] parameters)
        {
            var m = type.GetMethod(methodName, Reflector.StaticAllFlags);
            if (m == null)
            {
                throw MethodReflectionException.NotFound(type, methodName, parameters);
            }

            try
            {
                return m.Invoke(null, parameters);
            }
            catch (Exception ex)
            {
                throw MethodReflectionException.InvokeError(type, methodName, parameters, ex);
            }
        }

        /// <summary>
        /// source インスタンスのメンバフィールド fieldName の値を取得します。
        /// </summary>
        /// <param name="source">取得するフィールドをメンバとして保持するインスタンス。</param>
        /// <param name="fieldName">取得するフィールド名。</param>
        /// <returns>指定したフィールドの値。</returns>
        public static object Field(this object source, string fieldName)
        {
            var field = source.GetType().GetField(fieldName, Reflector.InstanceAllFlags);
            if (field == null)
            {
                throw FieldReflectionException.NotFound(source.GetType(), fieldName);
            }

            return field.GetValue(source);
        }

        /// <summary>
        /// type 型の静的フィールド fieldName の値を取得します。
        /// </summary>
        /// <param name="type">取得するフィールドを静的に保持する型。</param>
        /// <param name="fieldName">取得するフィールド名。</param>
        /// <returns>指定したフィールドの値。</returns>
        public static object Field(Type type, string fieldName)
        {
            var field = type.GetField(fieldName, Reflector.StaticAllFlags);
            if (field == null)
            {
                throw FieldReflectionException.NotFound(type, fieldName);
            }

            return field.GetValue(null);
        }

        /// <summary>
        /// source インスタンスのメンバフィールド fieldName に値 value を設定します。
        /// </summary>
        /// <param name="source">設定するフィールドをメンバとして保持するインスタンス。</param>
        /// <param name="fieldName">設定するフィールド名。</param>
        /// <param name="value">設定する値。</param>
        public static void Field(this object source, string fieldName, object value)
        {
            var field = source.GetType().GetField(fieldName, Reflector.InstanceAllFlags);
            if (field == null)
            {
                throw FieldReflectionException.NotFound(source.GetType(), fieldName);
            }

            field.SetValue(source, value);
        }

        /// <summary>
        /// type 型の静的フィールド fieldName に値 value を設定します。
        /// </summary>
        /// <param name="type">設定するフィールドを静的に保持する型。</param>
        /// <param name="fieldName">設定するフィールド名。</param>
        /// <param name="value">設定する値。</param>
        public static void Field(Type type, string fieldName, object value)
        {
            var field = type.GetField(fieldName, Reflector.StaticAllFlags);
            if (field == null)
            {
                throw FieldReflectionException.NotFound(type, fieldName);
            }

            field.SetValue(null, value);
        }

        /// <summary>
        /// source インスタンスのメンバフィールド fieldName の index 番目の値を取得します。
        /// </summary>
        /// <param name="source">取得するフィールドをメンバとして保持するインスタンス。</param>
        /// <param name="fieldName">取得するフィールド名。</param>
        /// <param name="index">取得するフィールドのインデックス。</param>
        /// <returns>指定したフィールドの index 番目の値。</returns>
        public static object ArrayField(this object source, string fieldName, int index)
        {
            var field = source.GetType().GetField(fieldName, Reflector.InstanceAllFlags);
            if (field == null)
            {
                throw FieldReflectionException.NotFound(source.GetType(), fieldName);
            }

            var obj = field.GetValue(source);
            if (obj == null)
            {
                throw FieldReflectionException.NullCollection(source.GetType(), fieldName);
            }

            var list = obj as System.Collections.IList;
            if (list == null)
            {
                throw FieldReflectionException.CannotSetToCollection(source.GetType(), fieldName);
            }

            return list[index];
        }

        /// <summary>
        /// source インスタンスのメンバフィールド fieldName の index 番目に値を設定します。
        /// </summary>
        /// <param name="source">設定するフィールドをメンバとして保持するインスタンス。</param>
        /// <param name="fieldName">設定するフィールド名。</param>
        /// <param name="index">設定するフィールドのインデックス。</param>
        /// <param name="value">設定する値。</param>
        public static void ArrayField(this object source, string fieldName, int index, object value)
        {
            var field = source.GetType().GetField(fieldName, Reflector.InstanceAllFlags);
            if (field == null)
            {
                throw FieldReflectionException.NotFound(source.GetType(), fieldName);
            }

            var obj = field.GetValue(source);
            if (obj == null)
            {
                throw FieldReflectionException.NullCollection(source.GetType(), fieldName);
            }

            var list = obj as System.Collections.IList;
            if (list == null)
            {
                throw FieldReflectionException.CannotSetToCollection(source.GetType(), fieldName);
            }

            list[index] = value;
        }

        /// <summary>
        /// source 静的フィールド fieldName の index 番目の値を取得します。
        /// </summary>
        /// <param name="type">取得するフィールドを静的に保持するインスタンス。</param>
        /// <param name="fieldName">取得するフィールド名。</param>
        /// <param name="index">取得するフィールドのインデックス。</param>
        /// <returns>指定したフィールドの index 番目の値。</returns>
        public static object ArrayField(Type type, string fieldName, int index)
        {
            var field = type.GetField(fieldName, Reflector.InstanceAllFlags);
            if (field == null)
            {
                throw FieldReflectionException.NotFound(type, fieldName);
            }

            var obj = field.GetValue(null);
            if (obj == null)
            {
                throw FieldReflectionException.NullCollection(type, fieldName);
            }

            var list = obj as System.Collections.IList;
            if (list == null)
            {
                throw FieldReflectionException.CannotSetToCollection(type, fieldName);
            }

            return list[index];
        }

        /// <summary>
        /// source 静的フィールド fieldName の index 番目に値を設定します。
        /// </summary>
        /// <param name="source">設定するフィールドを静的に保持するインスタンス。</param>
        /// <param name="fieldName">設定するフィールド名。</param>
        /// <param name="index">設定するフィールドのインデックス。</param>
        /// <param name="value">設定する値。</param>
        public static void ArrayField(Type type, string fieldName, int index, object value)
        {
            var field = type.GetField(fieldName, Reflector.InstanceAllFlags);
            if (field == null)
            {
                throw FieldReflectionException.NotFound(type, fieldName);
            }

            var obj = field.GetValue(null);
            if (obj == null)
            {
                throw FieldReflectionException.NullCollection(type, fieldName);
            }

            var list = obj as System.Collections.IList;
            if (list == null)
            {
                throw FieldReflectionException.CannotSetToCollection(type, fieldName);
            }

            list[index] = value;
        }

        /// <summary>
        /// source インスタンスのメンバプロパティ propertyName の値を取得します。
        /// </summary>
        /// <param name="source">取得するプロパティをメンバとして保持するインスタンス。</param>
        /// <param name="propertyName">取得するプロパティ名。</param>
        /// <returns>指定したプロパティの値。</returns>
        public static object Property(this object source, string propertyName)
        {
            var prop = source.GetType().GetProperty(propertyName, Reflector.InstanceAllFlags);
            if (prop == null)
            {
                throw new PropertyReflectionException(source.GetType(), propertyName);
            }

            return prop.GetValue(source);
        }

        /// <summary>
        /// type 型の静的プロパティ propertyName の値を取得します。
        /// </summary>
        /// <param name="type">取得するプロパティを静的に保持する型。</param>
        /// <param name="propertyName">取得するプロパティ名。</param>
        /// <returns>指定したプロパティの値。</returns>
        public static object Property(Type type, string propertyName)
        {
            var prop = type.GetProperty(propertyName, Reflector.StaticAllFlags);
            if (prop == null)
            {
                throw new PropertyReflectionException(type, propertyName);
            }

            return prop.GetValue(null);
        }

        /// <summary>
        /// source インスタンスのメンバプロパティ propertyName に値 value を設定します。
        /// </summary>
        /// <param name="source">設定するプロパティをメンバとして保持するインスタンス。</param>
        /// <param name="propertyName">設定するプロパティ名。</param>
        /// <param name="value">設定する値。</param>
        public static void Property(this object source, string propertyName, object value)
        {
            var prop = source.GetType().GetProperty(propertyName, Reflector.InstanceAllFlags);
            if (prop == null)
            {
                throw new PropertyReflectionException(source.GetType(), propertyName);
            }

            prop.SetValue(source, value);
        }

        /// <summary>
        /// type 型の静的プロパティ propertyName に値 value を設定します。
        /// </summary>
        /// <param name="type">設定するプロパティを静的に保持する型。</param>
        /// <param name="propertyName">設定するプロパティ名。</param>
        /// <param name="value">設定する値。</param>
        public static void Property(Type type, string propertyName, object value)
        {
            var prop = type.GetProperty(propertyName, Reflector.StaticAllFlags);
            if (prop == null)
            {
                throw new PropertyReflectionException(type, propertyName);
            }

            prop.SetValue(null, value);
        }

        #endregion

        /// <summary>
        /// 戻り値の型を指定して、source インスタンスのメンバフィールド fieldName の値を取得します。
        /// </summary>
        /// <typeparam name="TReturn">戻り値の型。</typeparam>
        /// <param name="source">取得するフィールドをメンバとして保持するインスタンス。</param>
        /// <param name="fieldName">取得するフィールド名。</param>
        /// <returns>指定したフィールドの値。</returns>
        public static TReturn Field<TReturn>(this object source, string fieldName)
        {
            return (TReturn)source.Field(fieldName);
        }

        /// <summary>
        /// 戻り値の型を指定して、type 型の静的フィールド fieldName の値を取得します。
        /// </summary>
        /// <typeparam name="TReturn">戻り値の型。</typeparam>
        /// <param name="type">取得するフィールドを静的に保持する型。</param>
        /// <param name="fieldName">取得するフィールド名。</param>
        /// <returns>指定したフィールドの値。</returns>
        public static TReturn Field<TReturn>(Type type, string fieldName)
        {
            return (TReturn)Reflector.Field(type, fieldName);
        }

        /// <summary>
        /// 戻り値の型を指定して、source インスタンスのメンバフィールド fieldName の index 番目の値を取得します。
        /// </summary>
        /// <typeparam name="TReturn">戻り値の型。</typeparam>
        /// <param name="source">取得するフィールドをメンバとして保持するインスタンス。</param>
        /// <param name="fieldName">取得するフィールド名。</param>
        /// <param name="index">取得するフィールドのインデックス。</param>
        /// <returns>指定したフィールドの index 番目の値。</returns>
        public static TReturn ArrayField<TReturn>(this object source, string fieldName, int index)
        {
            return (TReturn)source.ArrayField(fieldName, index);
        }

        /// <summary>
        /// 戻り値の型を指定して、type 型の静的フィールド fieldName の index 番目の値を取得します。
        /// </summary>
        /// <typeparam name="TReturn">戻り値の型。</typeparam>
        /// <param name="source">取得するフィールドを静的に保持するインスタンス。</param>
        /// <param name="fieldName">取得するフィールド名。</param>
        /// <param name="index">取得するフィールドのインデックス。</param>
        /// <returns>指定したフィールドの index 番目の値。</returns>
        public static TReturn ArrayField<TReturn>(Type type, string fieldName, int index)
        {
            return (TReturn)Reflector.ArrayField(type, fieldName, index);
        }

        /// <summary>
        /// 戻り値の型を指定して、source インスタンスのメンバプロパティ propertyName の値を取得します。
        /// </summary>
        /// <typeparam name="TReturn">戻り値の型。</typeparam>
        /// <param name="source">取得するプロパティをメンバとして保持するインスタンス。</param>
        /// <param name="propertyName">取得するプロパティ名。</param>
        /// <returns>指定したプロパティの値。</returns>
        public static TReturn Property<TReturn>(this object source, string propertyName)
        {
            return (TReturn)source.Property(propertyName);
        }

        /// <summary>
        /// 戻り値の型を指定して、type 型の静的プロパティ propertyName の値を取得します。
        /// </summary>
        /// <typeparam name="TReturn">戻り値の型。</typeparam>
        /// <param name="type">取得するプロパティを静的に保持する型。</param>
        /// <param name="propertyName">取得するプロパティ名。</param>
        /// <returns>指定したプロパティの値。</returns>
        public static TReturn Property<TReturn>(Type type, string propertyName)
        {
            return (TReturn)Reflector.Property(type, propertyName);
        }

        /// <summary>
        /// 指定された path の .NET アセンブリをロードします。
        /// </summary>
        /// <param name="path">ロードする .NET アセンブリのパス。</param>
        /// <returns>アセンブリ情報を格納する Reflector.AssemblyInformation。</returns>
        public static AssemblyInformation LoadFromFile(string path)
        {
            return new AssemblyInformation(Assembly.LoadFile(path));
        }

        /// <summary>
        /// 指定された型のインスタンスを作成して返します。
        /// </summary>
        /// <param name="type">作成するインスタンスの型。</param>
        /// <param name="parameters">コンストラクタに渡す引数。</param>
        /// <returns>作成したインスタンス。</returns>
        public static object CreateInstance(Type type, params object[] parameters)
        {
            return type.Assembly.CreateInstance(type.FullName, false, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public, null, parameters, null, null);
        }
    }
}
