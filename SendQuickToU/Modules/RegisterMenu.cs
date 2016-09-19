using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SendQuickToU.Modules
{
    class RegisterMenu
    {
        //public void Menu(string name,string appPath)
        //{
        //    RegistryKey key;
        //    key = Registry.ClassesRoot.CreateSubKey(@"Folder\shell\" + name);
        //    key = Registry.ClassesRoot.CreateSubKey(@"Folder\shell\" + name + "command");
        //    key.SetValue("", appPath);
        //}

        /// <summary>
        /// 参考资料：http://www.codeproject.com/Articles/15171/Simple-shell-context-menu
        /// </summary>
        /// <param name="fileType"></param>
        /// <param name="menuName"></param>
        /// <param name="menuNameEnAlias"></param>
        /// <param name="appregPath"></param>
        public static void Register(string fileType, string menuName, string menuNameEnAlias, string appregPath)
        {
            // create path to registry location
            string regPath = string.Format(@"{0}\shell\{1}", fileType, menuNameEnAlias);

            // add context menu to the registry
            using (RegistryKey key = Registry.ClassesRoot.CreateSubKey(regPath))
            {
                key.SetValue(null, menuName);
            }

            // add command that is invoked to the registry
            using (RegistryKey key = Registry.ClassesRoot.CreateSubKey(string.Format(@"{0}\command", regPath)))
            {
                key.SetValue(null, appregPath);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileType"></param>
        /// <param name="menuNameEnAlias"></param>
        public static void Unregister(string fileType, string menuNameEnAlias)
        {
            Debug.Assert(!string.IsNullOrEmpty(fileType) && !string.IsNullOrEmpty(menuNameEnAlias));

            // path to the registry location
            string regPath = string.Format(@"{0}\shell\{1}", fileType, menuNameEnAlias);

            // remove context menu from the registry
            Registry.ClassesRoot.DeleteSubKeyTree(regPath);
        }
    }
}
