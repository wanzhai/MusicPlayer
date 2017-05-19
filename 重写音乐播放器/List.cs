using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.ObjectModel;//ObservableCollection

namespace 重写音乐播放器
{
    class List
    {
        //public List<Music> MusicList = new System.Collections.Generic.List<Music>();//基本列表
        //public List<Music> RandomList = new List<Music>();//乱序列表
        //ObservableCollection表示一个动态数据集合，它可在添加、删除项目或刷新整个列表时提供通知。总之强烈推荐
        public ObservableCollection<Music> MusicList = new ObservableCollection<Music>();//基本列表
        public ObservableCollection<Music> RandomList = new ObservableCollection<Music>();//乱序列表
        public void MakeMusicList(string directory)//将文件夹中音乐制成列表
        {
            MusicList.Clear();
            string[] fileArray = Directory.GetFiles(directory, "*.mp3");//GetFiles函数获得文件的名称（包含其路径）
            foreach(string file in fileArray)
            {
                FileInfo fileInfo = new FileInfo(file);
                MusicList.Add(new Music() { Title = fileInfo.Name,
                                            Path = Convert.ToString(fileInfo),
                                            AddTime= DateTime.Now});//依次写入文件名、文件地址、添加时间
                #region 
                /*
                FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read);
                fs.Seek(-128, SeekOrigin.End);//将读取位置移动至倒数128位
                byte[] InfoByte=new byte[128];
                fs.Read(InfoByte, 0, 128);
                fs.Close();
                MusicList.Add(new Music()
                {
                    Title = Encoding.Default.GetString(InfoByte, 3, 30),
                    Artist = Encoding.Default.GetString(InfoByte, 33, 30),
                    Album = Encoding.Default.GetString(InfoByte, 63, 30),
                });
                */
                #endregion//整个代码都是无效的
                #region
                /*byte[] titleByte = new byte[30];//取出歌曲名byte
                int j = 0;
                int i;
                for (i = 2; i < 32; i++)
                {
                    titleByte[j] = InfoByte[i];
                    j++;
                }

                byte[] artistByte = new byte[30];//取出艺术家byte
                j = 0;
                for (; i < 62; i++)
                {
                    artistByte[j] = InfoByte[i];
                    j++;
                }
                byte[] albumByte = new byte[30];
                j = 0;
                for (; i < 92; i++)
                {
                    albumByte[j] = InfoByte[i];
                    j++;
                }
                MusicList.Add(new Music() { Title = Convert.ToString(titleByte),
                                            Artist = Convert.ToString(artistByte),
                                            Album = Convert.ToString(albumByte)});*/
                #endregion
            }
            if (MusicList.Count == 0)
            {
                MessageBox.Show("所选文件夹内没有音乐文件", "垃圾播放器");
            }
        }
        public void MakeRandomList()
        {
            ObservableCollection<Music> TempList = new ObservableCollection<Music>();//临时列表
            foreach(Music music in MusicList)//不知道怎样快速做复制，只好这样了
            {
                TempList.Add(new Music() { Title = music.Title, Path = music.Path, AddTime = music.AddTime });
            }
            Random random = new Random();
            for (int i = 0; i < MusicList.Count() ; i++) //循环次数为整个列表长度
            {
                int j = random.Next(TempList.Count());//随机选中一个，为第j位
                RandomList.Add(new Music() { Title = TempList[j].Title, Path = TempList[j].Path,
                                            AddTime = TempList[j].AddTime });//移到乱序表中
                //TempList[j] = TempList[TempList.Count() - 1 - i];//将临时表中的第j位等于第【长度-i】位，下次循环i加1，第[长度-1]位就不考虑了
                TempList[j] = TempList[TempList.Count() - 1];//将临时表中的第j位等于最后一位
                TempList.RemoveAt(TempList.Count() - 1);//将最后一位移除
            }
        }
    }
}