using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace ArchiLib{

    public class GetDatasFromPathExtention{
        /// <summary>
    /// 根据文件夹路径获取文件夹下所有包含的文件夹 返回文件夹名称和路径 返回Dictionary<string, string>
    /// </summary>
    /// <param name="folderPath"></param>
    /// <returns></returns>
        public static List<FolderData> GetFoldersFromPath(string folderPath)
        {
            if (Directory.Exists(folderPath))
            {
                List<FolderData> folderDatas = new List<FolderData>();
                DirectoryInfo directoryInfo = new DirectoryInfo(folderPath);// class System.IO.DirectoryInfo
                DirectoryInfo[] directoryInfos = directoryInfo.GetDirectories();
                for (int i = 0; i < directoryInfos.Length; i++)
                {
                    System.Console.WriteLine(folderPath + "/" + directoryInfos[i].Name);
                    FolderData data = new FolderData();
                        data.name = directoryInfos[i].Name;
                        data.path = folderPath + "/" + directoryInfos[i].Name;
                    folderDatas.Add(data);
                }
                return folderDatas;
            }
            return null;
        }

        /// <summary>
        /// 根据文件夹路径获取文件夹下所有的包含下列图片格式的图片  返回list<Sprite>
        /// </summary>
        /// <param name="folderPath"></param>
        /// <returns></returns>
        public static List<Sprite> GetSpritesFromPath(string folderPath)
        {

            if (Directory.Exists(folderPath))
            {
                List<Sprite> spritelist = new List<Sprite>();
                List<string> filePaths = new List<string>();
                string imgtype = "*.BMP|*.JPG|*.GIF|*.PNG";
                string[] ImageType = imgtype.Split('|');
                for (int i = 0; i < ImageType.Length; i++)
                {
                    //获取d盘中a文件夹下所有的图片路径  
                    string[] dirs = Directory.GetFiles(folderPath, ImageType[i]);
                    for (int j = 0; j < dirs.Length; j++)
                    {
                        filePaths.Add(dirs[j]);
                        //  print("图片文件地址："+dirs[j]);
                    }
                }
                foreach (string path in filePaths)
                {
                    spritelist.Add(LoadTextureToSprite(path));
                }
                return spritelist;
            }
            return null;
        }


        /// <summary>
        /// 根据图片地址 获取到图片为Sprite
        /// </summary>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        public static Sprite LoadTextureToSprite(string imagePath)
        {
            Texture2D t2d = new Texture2D(100, 100);
            //根据路劲读取字节流再转换成图片形式
            t2d.LoadImage(getImageByte(imagePath));
            //将Texture创建成Sprite 参数分别为图片源文件,Rect值给出起始点和大小 以及锚点的位置
            Sprite sp = Sprite.Create(t2d, new Rect(0, 0, t2d.width, t2d.height), Vector2.zero);
            sp.name = Path.GetFileName(imagePath).Split('.')[0];
            return sp;
        }



        /// <summary>
        /// 根据文件夹路径获取文件夹下所有的包含下列图片格式的图片  返回list<Sprite>
        /// </summary>
        /// <param name="folderPath"></param>
        /// <returns></returns>
        public static List<Texture2D> GetTexture2DsFromPath(string folderPath)
        {

            if (Directory.Exists(folderPath))
            {
                List<Texture2D> texture2dlist = new List<Texture2D>();
                List<string> filePaths = new List<string>();
                string imgtype = "*.BMP|*.JPG|*.GIF|*.PNG";
                string[] ImageType = imgtype.Split('|');
                for (int i = 0; i < ImageType.Length; i++)
                {
                    //获取d盘中a文件夹下所有的图片路径  
                    string[] dirs = Directory.GetFiles(folderPath, ImageType[i]);
                    for (int j = 0; j < dirs.Length; j++)
                    {
                        filePaths.Add(dirs[j]);
                        //  print("图片文件地址："+dirs[j]);
                    }
                }
                foreach (string path in filePaths)
                {
                    texture2dlist.Add(LoadTexture2D(path));// 
                }
                return texture2dlist;
            }

            return null;
        }


        public static Texture2D LoadTexture2D(string imagePath)// 2D纹理
        {
            Texture2D t2d = new Texture2D(100, 100);
            //根据路劲读取字节流再转换成图片形式
            t2d.LoadImage(getImageByte(imagePath));// using UnityEngine;
    
            return t2d;
        }



        /// <summary>
        /// 根据图片路径 获取到图片byte[]  LoadTextureToSprite(cs:79 图片到Sprite)和LoadTexture2D(cs:127)
        /// </summary>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        public static byte[] getImageByte(string imagePath)// 图片转成比特
        {
            //读取到文件
            FileStream files = new FileStream(imagePath, FileMode.Open);
            //新建比特流对象
            byte[] imgByte = new byte[files.Length];
            //将文件写入对应比特流对象
            files.Read(imgByte, 0, imgByte.Length);
            //关闭文件
            files.Close();
            //返回比特流的值
            return imgByte;
        }


        /// <summary>
        /// 根据文件夹路径获取文件夹下所有的包含下列视频格式的视频地址  返回list<string>
        /// </summary>
        /// <param name="folderPath"></param>
        /// <returns></returns>
        public static List<string> GetAVpathFromPath(string folderPath)
        {
            if (Directory.Exists(folderPath))// IO关键字
            {
                List<string> avfilespath = new List<string>();
                string avtype = "*.MP4";
                string[] dirs = Directory.GetFiles(folderPath, avtype);
                for (int j = 0; j < dirs.Length; j++)
                {
                    avfilespath.Add(dirs[j]);
                    // print("视频地址：" + dirs[j]);
                }
                return avfilespath;
            }
            return null;
        }



        /// <summary>
        /// 根据文件夹 路径和文件类型 获取文件名和文件路径 返回Dictionary<string, string>
        /// </summary>
        /// <param name="folderPath"></param>
        /// <param name="fileType">列如：string wordtype = "*.DOC|*.DOCX";</param>
        /// <returns></returns>
        public static List<FileData> GetDataFromPathType(string folderPath, string fileType)
        {
            if (Directory.Exists(folderPath))
            {
                List<FileData> fileDatas = new List<FileData>();
                string[] FileType = fileType.Split('|');
                for (int i = 0; i < FileType.Length; i++)
                {
                    string[] dirs = Directory.GetFiles(folderPath, FileType[i]);// 
                    for (int j = 0; j < dirs.Length; j++)
                    {
                        FileData data = new FileData();// 数据结构
                        data.name = Path.GetFileName(dirs[j]);// 
                        data.path = dirs[j];
                        fileDatas.Add(data);// 循环结算加入
                    
                        // Path.GetFileName(dirs[j]);
                        //  print(Path.GetFileName(dirs[j])+"    "+ dirs[j]);
                        // FileData.Add(Path.GetFileName(dirs[j]), dirs[j]);                 
                    }
                }
                return fileDatas;
            }
            return null;
        }
    }
    public class FolderData
    {
        public string name;
        public string path;
    }
    public class FileData
    {
        public string name;
        public string path;
    }
}