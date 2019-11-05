using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Keyboard
    {
        static private List<string> Keys = new List<string>();
        static public void AddToList(string KeyName)
        {
            Keys.Add(KeyName);
        }
        static public void DelFromList(string KeyName)
        {
            while (Keys.Remove(KeyName)) ;
        }
        static public bool KeyPress(string KeyName)
        {
            return Keys.Contains(KeyName);
        }
    }
}
