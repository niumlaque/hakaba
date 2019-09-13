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

namespace NilaLib.Windows
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    /// <summary>
    /// ホットキーを登録する機能を提供します。
    /// </summary>
    public class HotKey : IDisposable
    {
        /// <summary>
        /// ホットキーに登録するキーの組み合わせ。
        /// Control: Keys.ControlKey
        /// Shift: Keys.ShiftKey
        /// Alt: Keys.Alt
        /// </summary>
        private readonly IEnumerable<Keys> keys;

        /// <summary>
        /// ホットキーが押下された際の処理。
        /// </summary>
        private readonly Action onHotKeyPressed;

        /// <summary>
        /// ホットキーの識別子。
        /// </summary>
        private readonly int id = -1;

        /// <summary>
        /// ホットキーを処理するフォーム。
        /// </summary>
        private HotKeyForm form;

        /// <summary>
        /// ホットキーに登録する組み合わせと押下された際の処理を指定して、NilaLib.Windows.HotKey クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="keys">ホットキーに登録するキーの組み合わせ。</param>
        /// <param name="onHotKeyPressed">ホットキーが押下された際の処理。</param>
        /// <param name="id">ホットキーの識別子。内部で勝手に割り当てる場合は -1。</param>
        protected HotKey(IEnumerable<Keys> keys, Action onHotKeyPressed, int id)
        {
            this.keys = keys;
            this.onHotKeyPressed = onHotKeyPressed;
            this.id = id;
        }

        /// <summary>
        /// 現在のインスタンスを削除します。
        /// </summary>
        ~HotKey()
        {
            this.Dispose();
        }

        /// <summary>
        /// ホットキーに登録する組み合わせと押下された際の処理を指定して、NilaLib.Windows.HotKey クラスの新しいインスタンスを作成して返します。
        /// </summary>
        /// <param name="keys">ホットキーに登録するキーの組み合わせ。</param>
        /// <param name="onHotKeyPressed">ホットキーが押下された際の処理。</param>
        /// <param name="id">ホットキーの識別子。内部で勝手に割り当てる場合は -1。</param>
        /// <returns>NilaLib.Windows.HotKey クラスの新しいインスタンス。</returns>
        public static HotKey New(IEnumerable<Keys> keys, Action onHotKeyPressed, int id = -1)
        {
            return new HotKey(keys, onHotKeyPressed, id);
        }

        /// <summary>
        /// ホットキーの登録をします。
        /// </summary>
        public virtual void Register()
        {
            if (this.form == null)
            {
                var form = new HotKeyForm(this.keys, this.onHotKeyPressed, this.id);
            }
        }

        /// <summary>
        /// ホットキーの登録を解除します。
        /// </summary>
        public virtual void Dispose()
        {
            if (this.form != null)
            {
                this.form.Dispose();
                this.form = null;
            }
        }

        /// <summary>
        /// ホットキーのウィンドウメッセージを処理する機能を提供します。
        /// </summary>
        protected class HotKeyForm : Form
        {
            /// <summary>
            /// ホットキーのウィンドウメッセージ ID。
            /// </summary>
            private const int WM_HOTKEY = 0x0312;

            /// <summary>
            /// ホットキーが押下された際の処理。
            /// </summary>
            private readonly Action onHotKeyPressed;

            /// <summary>
            /// 登録したホットキーの識別子。
            /// </summary>
            private int id = -1;

            /// <summary>
            /// ホットキーに登録する組み合わせと押下された際の処理を指定して、NilaLib.Windows.HotKey.HotKeyForm クラスの新しいインスタンスを作成します。
            /// </summary>
            /// <param name="keys">ホットキーに登録するキーの組み合わせ。</param>
            /// <param name="onHotKeyPressed">ホットキーが押下された際の処理。</param>
            /// <param name="id">ホットキーの識別子。内部で勝手に割り当てる場合は -1。</param>
            public HotKeyForm(IEnumerable<Keys> keys, Action onHotKeyPressed, int id = -1)
            {
                this.onHotKeyPressed = onHotKeyPressed ?? (() => { });
                this.RegisterHotKey(keys, onHotKeyPressed, id);
            }

            /// <summary>
            /// キー修飾子フラグを定義します。
            /// </summary>
            protected enum MOD_KEY : int
            {
                /// <summary>
                /// 何も表さないことを表します。
                /// </summary>
                NONE = 0x0,

                /// <summary>
                /// ALT キーを表します。
                /// </summary>
                ALT = 0x1,

                /// <summary>
                /// Control キーを表します。
                /// </summary>
                CONTROL = 0x2,

                /// <summary>
                /// Shift キーを表します。
                /// </summary>
                SHIFT = 0x4,

                /// <summary>
                /// Windows キーを表します。
                /// </summary>
                WIN = 0x8
            }

            /// <summary>
            /// ホットキーを登録します。
            /// </summary>
            /// <param name="keys">ホットキーに登録するキーの組み合わせ。</param>
            /// <param name="onHotKeyPressed">ホットキーが押下された際の処理。</param>
            /// <param name="id">ホットキーの識別子。内部で勝手に割り当てる場合は -1。</param>
            protected virtual void RegisterHotKey(IEnumerable<Keys> keys, Action onHotKeyPressed, int id)
            {
                var modKeys = MOD_KEY.NONE;
                if (keys.Contains(Keys.ControlKey))
                {
                    modKeys |= MOD_KEY.CONTROL;
                }

                if (keys.Contains(Keys.ShiftKey))
                {
                    modKeys |= MOD_KEY.SHIFT;
                }

                if (keys.Contains(Keys.Alt))
                {
                    modKeys |= MOD_KEY.ALT;
                }

                var key = keys.FirstOrDefault(x => x != Keys.ControlKey && x != Keys.ShiftKey && x != Keys.Alt);
                if (key == Keys.None)
                {
                    throw new Exception("Failed to register hotkey");
                }

                if (id >= 0)
                {
                    if (RegisterHotKey(this.Handle, id, modKeys, key) != 0)
                    {
                        this.id = id;
                    }
                    else
                    {
                        throw new Exception("Failed to register hotkey");
                    }
                }
                else
                {
                    var startID = 0x0000;
                    var endID = 0xbfff;
                    if (Path.GetExtension(this.GetType().Assembly.FullName).ToLower().EndsWith("dll"))
                    {
                        startID = 0xc000;
                        endID = 0xffff;
                    }

                    for (int i = startID; i <= endID; ++i)
                    {
                        if (RegisterHotKey(this.Handle, i, modKeys, key) != 0)
                        {
                            this.id = i;
                            break;
                        }
                    }

                    if (this.id < 0)
                    {
                        throw new Exception("Failed to register hotkey");
                    }
                }
            }

            /// <summary>
            /// このフォームへ送られてきたウィンドウメッセージを処理します。
            /// </summary>
            /// <param name="m">処理対象の Windows System.Windows.Forms.Message。</param>
            protected override void WndProc(ref Message m)
            {
                base.WndProc(ref m);
                if (m.Msg == WM_HOTKEY && (int)m.WParam == this.id)
                {
                    this.onHotKeyPressed();
                }
            }

            /// <summary>
            /// このインスタンスで保持している全てのリソースを破棄し、ホットキーの登録を解除します。
            /// </summary>
            /// <param name="disposing">マネージ リソースとアンマネージ リソースの両方を解放する場合は true。アンマネージ リソースだけを解放する場合は false。</param>
            protected override void Dispose(bool disposing)
            {
                base.Dispose(disposing);
                UnregisterHotKey(this.Handle, this.id);
            }

            /// <summary>
            /// システム全体に適用されるのホットキーの登録を行います。
            /// </summary>
            /// <param name="hWnd">ウィンドウのハンドル。</param>
            /// <param name="id">ホットキーの識別子。</param>
            /// <param name="fsModifiers">キー修飾子フラグ。</param>
            /// <param name="vk">仮想キーコード。</param>
            /// <returns>関数が成功すると 0 以外の値。関数が失敗すると 0。</returns>
            [DllImport("user32.dll")]
            private static extern int RegisterHotKey(IntPtr hWnd, int id, MOD_KEY fsModifiers, Keys vk);

            /// <summary>
            /// RegisterHotKey 関数で登録したホットキーを解除します。
            /// </summary>
            /// <param name="hWnd">ウィンドウのハンドル。</param>
            /// <param name="id">ホットキーの識別子。</param>
            /// <returns>関数が成功すると 0 以外の値。関数が失敗すると 0。</returns>
            [DllImport("user32.dll")]
            private static extern int UnregisterHotKey(IntPtr hWnd, int id);
        }
    }
}
