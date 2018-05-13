using System;
using System.Windows.Forms;

namespace Editor
{
    public class EntryPoint
    {
        [STAThread]
        public static void Main()
        {
            Application.Run(new EditorForm());
        }
    }
}