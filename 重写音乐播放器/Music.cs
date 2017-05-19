using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 重写音乐播放器
{
    class Music
    {

        private string title;//音乐标题
        private string artist;//音乐作者
        private string album;//专辑，唱片
        private int? length;//时长
        private string path;//文件地址
        private DateTime addtime;//添加日期

        public string Title
        {
            get{ return title; }
            set
            {
                if (title == null)
                {
                    title = value;
                }
            }
        }
        public string Artist
        {
            get{ return artist; }
            set
            {
                if (artist == null)
                {
                    artist = value;
                }
            }
        } 
        public string Album
        {
            get{ return album; }
            set
            {
                if (album == null)
                {
                    album = value;
                }
            }
        }
        public int? Length
        {
            get { return length; }
            set
            {
                if (length ==null )
                {
                    length = value;
                }
            }
        }
        public string Path
        {
            get { return path; }
            set
            {
                path = value;
            }
        }
        public DateTime AddTime
        {
            get { return addtime; }
            set
            {
                addtime = value;
            }
        }
    }
}
