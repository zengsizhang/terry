﻿namespace LostMinerLib.Util
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;

    public class INIClass
    {
        public string inipath;

        public INIClass(string INIPath)
        {
            this.inipath = INIPath;
        }

        public bool ExistINIFile()
        {
            return File.Exists(this.inipath);
        }

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        public string IniReadValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(500);
            int i = GetPrivateProfileString(Section, Key, "", temp, 500, this.inipath);
            return temp.ToString();
        }

        public void IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this.inipath);
        }

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
    }
}

