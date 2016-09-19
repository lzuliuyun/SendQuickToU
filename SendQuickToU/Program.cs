using IWshRuntimeLibrary;
using SendQuickToU.Modle;
using SendQuickToU.Modules;
using System;
using System.IO;
using System.Windows.Forms;

namespace SendQuickToU
{
    static class Program
    {
        private static FileShortcut[] fileShortcuts = new FileShortcut[2] {
            new FileShortcut("->快捷方式文档", "createShortcut", "Folder","\"%L\""),
            new FileShortcut("->新建记事本文件", "createTxtDocument", "Directory\\Background","\"%V\"")
            //new FileShortcut("快捷方式到文档", "send to user document", "Folder"),
        };
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //SendToUserDocumentDir(new string[] { @"C:\Users\lzuliuyun\Desktop", "createTxtDocument" });
            //MessageBox.Show(string.Join(",", args));
            SendToUserDocumentDir(new string[] { @"C:\Users\lzuliuyun\Downloads\MyContextMenu", "send folder to user document" });
            if (!ProcessCommand(args))
            {
                //MessageBox.Show(string.Join(",",args));
                SendToUserDocumentDir(args);
            }
        }

		static bool ProcessCommand(string[] args)
        {
            // register
            if (args.Length == 0 || string.Compare(args[0], "-register", true) == 0)
            {
                int len = fileShortcuts.Length;
                string menuCommand = "";
                for (int i = 0; i < len; i++)
                {
                    FileShortcut fileshortcut = fileShortcuts[i];
                    //第一个是关联的程序路径  第二个是从哪开始启动的路径 第三个是标识当前类别的参数
                   menuCommand = string.Format("\"{0}\" " + fileshortcut.ParaType + " \"" + fileshortcut.RightNameAlias + "\"", Application.ExecutablePath);
                    RegisterMenu.Register(fileshortcut.FileType, fileshortcut.RightName, fileshortcut.RightNameAlias, menuCommand);
                }
               
                return true;
            }

            // unregister		
            if (string.Compare(args[0], "-unregister", true) == 0)
            {
                int len = fileShortcuts.Length;
                for (int i = 0; i < len; i++)
                {
                    FileShortcut fileshortcut = fileShortcuts[i];
                    RegisterMenu.Unregister(fileshortcut.FileType, fileshortcut.RightNameAlias);
                }
               
                return true;
            }

            return false;

            //// register
            //if (args.Length == 0 || string.Compare(args[0], "-register", true) == 0)
            //{
            //    string menuCommandFolder = string.Format("\"{0}\" \"%L\" \"fileshortcut\"", Application.ExecutablePath);
            //    RegisterMenu.Register("Folder", "快捷方式到文档", "send to user document",menuCommand);
            //    return true;
            //}

            //// unregister		
            //if (string.Compare(args[0], "-unregister", true) == 0)
            //{
            //    RegisterMenu.Unregister("Folder", "send to user document");
            //    return true;
            //}

            // return false;


        }


        static void SendToUserDocumentDir(string[] args)
        {
            try
            {
                if(args[1] == fileShortcuts[0].RightNameAlias)
                {
                    string folderDir = args[0];
                    DirectoryInfo foldInfo = new DirectoryInfo(folderDir);
                    string dinDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    string name = foldInfo.Name;
                    IWshRuntimeLibrary.WshShell shell = new IWshRuntimeLibrary.WshShell();
                    //通过该对象的 CreateShortcut 方法来创建 IWshShortcut 接口的实例对象
                    //*.lnk，可以写写全路径名称，这样就不用设置WorkingDirectory 属性
                    IWshRuntimeLibrary.IWshShortcut shortcut = (IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut(dinDir + "\\" + foldInfo.Name +".lnk");

                    //就是将这个快捷方式放在哪个目录下，如果在CreateShortcut的时候用的全路径，这个步骤可以省略
                    //shortcut.Description = foldInfo.Name;
                    shortcut.TargetPath = folderDir;//快捷方式指向路径
                    //shortcut.WorkingDirectory = "";
                    //shortcut.WindowStyle = 1; //(透过快捷键打开文件夹的时候1.Normal window普通窗口,3.Maximized最大化窗口,7.Minimized最小化)
                    //shortcut.Description = 快捷方式描述信息;
                    //shortcut.IconLocation = “自定义图标”
                    //shortcut.Hotkey = "shift + A"; //hotkey如shift + A
                    shortcut.Save();//保存
                }
                else if (args[1] == fileShortcuts[1].RightNameAlias)
                {
                    string folderDir = args[0];
                    //MessageBox.Show(folderDir);
                    string fileName = "index";
                    string path = @folderDir+"//"+ fileName + ".txt";

                    int i = 0;
                    while(System.IO.File.Exists(path))
                    {
                        i++;
                        path = @folderDir + "//" + fileName + "_"+ i + ".txt";                       
                    }

                    System.IO.File.Create(path);
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
        }
    }
}
