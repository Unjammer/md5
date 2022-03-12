using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;


namespace md5
{
    class Program
    {
        static string MainDir = "";
        static string MainFile = @".\mods.list";
        static List<string> listfile = new List<string>();
        static void Main(string[] args)
        {
            if (args == null)
            {
                System.Console.WriteLine("invalid name file"); // Check for null array
            }
            else
            {


                for (int i = 0; i < args.Length; i++)
                {
                    if (i == 0)
                    {
                        MainDir = args[0];
                        
                    }
                    if (i == 1)
                    {
                        if (args[1] != null) MainFile = args[1];
                    }
                    

                }
                DirSearch_ex3(args[0]);
            }
        }

        static void DirSearch_ex3(string sDir)
        {
            //Console.WriteLine("DirSearch..(" + sDir + ")");
            try
            {

                foreach (string f in Directory.GetFiles(sDir))
                {
                    listfile.Add(GetSHA1HashFromFile(f).Replace(MainDir + @"\",""));
                }

                foreach (string d in Directory.GetDirectories(sDir))
                {
                    DirSearch_ex3(d);
                }
            }
            catch (System.Exception excpt)
            {
                Console.WriteLine(excpt.Message + " (" + sDir + ")");
            }
            System.IO.File.WriteAllLines(MainFile, listfile);
        }

        static string GetSHA1HashFromFile(string filename)
        {
            using (FileStream stream = File.OpenRead(filename))
            {
                using (SHA1Managed sha = new SHA1Managed())
                {
                    byte[] checksum = sha.ComputeHash(stream);
                    string sendCheckSum = BitConverter.ToString(checksum).Replace("-", string.Empty);
                    return filename + ";" + sendCheckSum;
                }
            }
        }

        static string GetMD5HashFromFile(string fileName)
        {
            FileStream file = new FileStream(fileName, FileMode.Open);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] retVal = md5.ComputeHash(file);
            file.Close();

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
