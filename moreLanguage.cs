using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MoreLanguage
{
        
    public static class moreLanguage
    {
        private static class LanguagePkg
        {
            public static Dictionary<string, string> lockeLanguang=null;
            public static Dictionary<string, string> otherLanguage=null;
        }
        /// <summary>
        /// 提前设置好的路径
        /// </summary>
        private readonly static string path = "/Users/a0624/Downloads/工作簿1.csv";

        /// <summary>
        /// 初始化 不同的语言包 添加到languagepkg
        /// </summary>
        /// <param name="type">语言包的类型</param>>
        public static void init0(LanguageType type)
        {

            Task task =new Task(()=>{
                var startReadFileAsync= StartReadFileAsync(path, type);
                startReadFileAsync.Wait();
                LanguagePkg.lockeLanguang=startReadFileAsync.Result;
                //language.otherLanguage=StartReadFileAsync(otherPath, LanguageType.中文);
                //可以添加更多语言包
            });
            task.Start();

        }

        /// <summary>
        /// 开始从指定到路径中读取文件，并只读取指定到语言类型内容
        /// </summary>
        /// <param name="type">语言类型</param>
        /// <param name="path">文件路径</param>>
        /// <returns>语言包</returns>>
        private static async Task<Dictionary<string,string>> StartReadFileAsync(string path,LanguageType type)
        {
            StreamReader streamReader = new StreamReader(path,Encoding.Default);
            string line;
            List<string> strings=new List<string>();
            while ((line = streamReader.ReadLine()) != null)
            {
                strings.Add(line);
            }
           return fixString(strings.ToArray(),type);
        }


        /// <summary>
        /// 按行 解析字符串
        /// </summary>
        /// <param name="strs"></param>
        /// <param name="type">语言类型</param>>
        /// <returns>语言包</returns>
        private static Dictionary<string,string> fixString(string[] strs,LanguageType type)
        {
            int languageTypeNum=-1;
            Dictionary<string ,string> languagePkg = new Dictionary<string, string>();
            string[] languageLine =strs[0].Split(new[] {","}, StringSplitOptions.None);
            for (int i = 0; i < languageLine.Length; i++)
            {
                if (languageLine[i]==type.ToString())
                {
                    languageTypeNum = i;
                    break;
                }
            }
            if (languageTypeNum==-1)
            {
                return null;
            }
            string[] s;
            for (int i = 1; i < strs.Length; i++)
            {
                s=strs[i].Split(new[] {","}, StringSplitOptions.None);
                languagePkg.Add(s[0],s[languageTypeNum]);
            }
            return languagePkg;
        }

        /// <summary>
        /// 根据给的key 返回对应的文字
        /// </summary>
        /// <param name="key">与文字关联的key</param>
        /// <param name="pkgType">语言包类型</param>
        /// <returns>wored</returns>
        public static string ReadWored(string key,LanguagePakTyep pkgType)
        {
            switch (pkgType)
            {
                case LanguagePakTyep.basic :
                    
                    if (LanguagePkg.lockeLanguang!=null&&LanguagePkg.lockeLanguang.ContainsKey(key))
                    {
                        return LanguagePkg.lockeLanguang[key];
                    }
                    else
                    {
                        return "";
                    }
                case LanguagePakTyep.other :
                    if (LanguagePkg.otherLanguage!=null&&LanguagePkg.otherLanguage.ContainsKey(key))
                    {
                        return LanguagePkg.otherLanguage[key];
                    }
                    else
                    {
                        return "";
                    }
                default:
                    break;
            }
            return "";
        }
        
       
    }

    public enum LanguagePakTyep
    {
        basic,
        other
    }

    public enum LanguageType
    {
        中文,
        英语
    }
}
