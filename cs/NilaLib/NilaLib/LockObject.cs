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

    /// <summary>
    /// アクセス時に排他制御をする機能を提供します。
    /// </summary>
    /// <typeparam name="T">排他制御するデータの型。</typeparam>
    public class LockObject<T>
    {
        /// <summary>
        /// 排他制御用オブジェクト。
        /// </summary>
        private readonly object syncObject = new object();

        /// <summary>
        /// 排他制御対象のデータ。
        /// </summary>
        private T value;

        /// <summary>
        /// LockObject にアクセスされた場合に一度だけ呼び出される静的コンストラクタ。
        /// </summary>
        static LockObject()
        {
        }

        /// <summary>
        /// default(T) 値を格納する、NilaLib.LockObject&lt;T&gt; クラスの新しいインスタンスを作成します。
        /// </summary>
        internal LockObject()
        {
        }

        /// <summary>
        /// 初期値を指定して、NilaLib.LockObject&lt;T&gt; クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="defaultValue">初期値。</param>
        internal LockObject(T defaultValue)
        {
            this.value = defaultValue;
        }

        /// <summary>
        /// 排他制御をして値を取得または設定します。
        /// </summary>
        public T Value
        {
            get
            {
                lock (this.syncObject)
                {
                    return this.value;
                }
            }

            set
            {
                lock (this.syncObject)
                {
                    this.value = value;
                }
            }
        }

        /// <summary>
        /// データが無いオブジェクトが指定された場合に default(T)を格納する NilaLib.LockObject&lt;T&gt; に変換します。
        /// </summary>
        /// <param name="obj">データ。</param>
        /// <returns>default 値を格納する NilaLib.LockObject&lt;T&gt;。</returns>
        public static implicit operator LockObject<T>(LockObject obj)
        {
            return new LockObject<T>();
        }

        /// <summary>
        /// 排他制御をして f を実行します。
        /// f が終了するまでは NilaLib.LockObject&lt;T&gt;.Value はロックされた状態になります。
        /// </summary>
        /// <param name="f">排他制御をして実行したい処理 f。</param>
        public void ActionWithLock(Action<T> f)
        {
            lock (this.syncObject)
            {
                f(this.value);
            }
        }

        /// <summary>
        /// 排他制御をして f を実行します。
        /// f が終了するまでは NilaLib.LockObject&lt;T&gt;.Value はロックされた状態になります。
        /// </summary>
        /// <typeparam name="U">f の戻り値。</typeparam>
        /// <param name="f">排他制御をして実行したい処理 f。</param>
        /// <returns>f の戻り値。</returns>
        public U ActionWithLock<U>(Func<T, U> f)
        {
            lock (this.syncObject)
            {
                return f(this.value);
            }
        }

        /// <summary>
        /// 排他制御をして、pred が成立する場合に valueSetter の値を設定します。
        /// pred 及び valueSetter が終了するまでは NilaLib.LockObject&lt;T&gt;.Value はロックされた状態になります。
        /// </summary>
        /// <param name="pred">値を設定するための条件。</param>
        /// <param name="valueSetter">設定する値を返す処理 f。</param>
        /// <returns>値が設定できた場合は true。それ以外の場合は false。</returns>
        public bool SetValueIf(Func<T, bool> pred, Func<T> valueSetter)
        {
            lock (this.syncObject)
            {
                if (pred(this.value))
                {
                    try
                    {
                        this.value = valueSetter();
                        return true;
                    }
                    catch
                    {
                        throw;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// 既に設定されている値と設定しようとしている値が違う場合のみ値を設定します。
        /// </summary>
        /// <param name="value">設定する値。</param>
        /// <returns>値を設定した場合は true。値を設定しなかった場合は false。</returns>
        internal bool SetValueIfValueIsDifference(T value)
        {
            lock (this.syncObject)
            {
                if (!this.value.Equals(value))
                {
                    this.value = value;
                    return true;
                }

                return false;
            }
        }
    }

    /// <summary>
    /// NilaLib.LockObject&lt;T&gt; の補助処理を提供します。
    /// </summary>
    public class LockObject
    {
        /// <summary>
        /// NilaLib.LockObject クラスの新しいインスタンスを作成します。
        /// </summary>
        private LockObject()
        {
        }

        /// <summary>
        /// 初期値が無い NilaLib.LockObject クラスの新しいインスタンスを作成して返します。
        /// NilaLib.LockObject&lt;T&gt; に代入可能です。
        /// </summary>
        /// <returns>ONVIFTester.LockObject クラスの新しいインスタンス。</returns>
        public static LockObject New()
        {
            return new LockObject();
        }

        /// <summary>
        /// 初期値を指定して、NilaLib.LockObject&lt;T&gt; クラスの新しいインスタンスを作成して返します。
        /// </summary>
        /// <typeparam name="T">排他制御するデータの型。</typeparam>
        /// <param name="defaultValue">初期値。</param>
        /// <returns>NilaLib.LockObject&lt;T&gt; クラスの新しいインスタンス。</returns>
        public static LockObject<T> New<T>(T defaultValue)
        {
            return new LockObject<T>(defaultValue);
        }
    }
}
