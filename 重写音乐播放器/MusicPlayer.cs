using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;

namespace 重写音乐播放器
{
    class MusicPlayer:MediaPlayer,INotifyPropertyChanged
    {
        public MusicPlayer()//构造函数
        {
            NowPlayingNumber = 0;//从列表中第一位开始播放
            PlayState = playState.stoped;//播放状态初始化为停止状态
            LoopState = loopState.positiveOrder;//循环状态初始化为顺序播放
        }
        public event PropertyChangedEventHandler PropertyChanged;//NowPlayingNumber改变的事件
        public enum playState : int//播放状态变量有三个状态值
        {
            stoped = 0,
            playing = 1,
            paused = 2
        }
        public playState PlayState;
        private int nowPlayingNumber;
        public int NowPlayingNumber
        {
            get { return nowPlayingNumber; }
            set
            {
                nowPlayingNumber = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("NowPlayingNumber"));
                }
            }
        }//记录当前播放的是列表第几位
        public ObservableCollection<Music> NowPlayingList;

        public enum loopState : int//循环状态
        {
            positiveOrder = 0,//正序
            invertedOrder = 1,//倒序
            disOrder = 2,//乱序
            singleTuneCirculation = 3//单曲循环
        }
        public loopState LoopState;
        
        public void PlayorPause()//暂停播放键功能方法
        {
            switch (PlayState)//再检查播放状态
            {
                case playState.stoped://如果是停止状态
                    if (NowPlayingList.Count() != 0)//如果列表非空
                    {
                        PlayState = playState.playing;//先更改播放状态
                        Open(new Uri(Convert.ToString(NowPlayingList[NowPlayingNumber].Path)));//打开文件
                        Play();//播放，以上两步缺一不可
                    }
                    else
                    {
                        MessageBox.Show("当前播放列表为空", "错误");
                    }
                    break;
                case playState.playing://如果是正在播放的状态
                    PlayState = playState.paused;//修改播放状态标记
                    Pause();//暂停
                    break;
                case playState.paused://如果是暂停状态
                    PlayState = playState.playing;
                    Play();//播放之
                    break;
            }
        }
        public void Next()//切下一首歌
        {
            if (NowPlayingList.Count() == 0)//先检测nowPlayingList是否为空，出现情况为用户未加载音乐时点击“下一首”按钮
            {
                return;
            }
            else
            {
                switch (LoopState)//根据循环状态执行相应代码
                {
                    case loopState.singleTuneCirculation://如果单曲循环
                    case loopState.positiveOrder://如果是顺序播放
                    case loopState.disOrder://如果乱序播放
                        this.ListsNext();
                        break;
                    case loopState.invertedOrder://如果倒序播放
                        this.ListsLast();
                        break;
                }
                PlayState = playState.playing;//改变播放状态
            }
        }

        public void Last()//切上一首歌
        {
            if (NowPlayingList.Count() == 0)
            {
                return;
            }
            switch (LoopState)//根据循环状态执行相应代码
            {
                case loopState.positiveOrder://如果是顺序播放
                case loopState.singleTuneCirculation://如果单曲循环
                case loopState.disOrder://如果乱序播放
                    this.ListsLast();
                    break;
                case loopState.invertedOrder://如果倒序播放
                    this.ListsNext();
                    break;
            }
            PlayState = playState.playing;//改变播放状态
        }
        
        private void ListsNext()//顺序表的下一首和逆序表的上一首的逻辑是相同的，所以把重复的代码提取出来
        {
            if (NowPlayingNumber < NowPlayingList.Count() - 1)//如果还没播放到列表末尾
            {
                NowPlayingNumber++;
            }
            else//如果已经播放到了列表末尾
            {
                NowPlayingNumber = 0;
            }
            Open(new Uri(NowPlayingList[NowPlayingNumber].Path));
            Play();
        }
        private void ListsLast()//顺序表的上一首和逆序表的下一首的逻辑是相同的，所以把重复的代码提取出来
        {
            if (NowPlayingNumber > 0)
            {
                NowPlayingNumber--;
            }
            else
            {
                NowPlayingNumber = NowPlayingList.Count() - 1;
            }
            Open(new Uri(NowPlayingList[NowPlayingNumber].Path));
            Play();
        }
        public void SwitchLoopState(List MainList)//本应该是这样的切换循环状态方法
        {
            LoopState++;
            if (LoopState==loopState.disOrder)//如果循环状态是乱序的话就改NowPlayingList为乱序表
            {
                MainList.MakeRandomList();//在需要乱序表的时候再制作乱序表
                NowPlayingList = MainList.RandomList;
            }
            else//否则就改为顺序表
            {
                NowPlayingList = MainList.MusicList;
            }
        }
        
        public void UserSayPlayThis(Music tempMusic,List MainList)//播放用户在音乐列表里选择的歌曲
        {
            //先检查tempMusic是否是空值，因为用户可能双击了列表区的空白地方
            if (tempMusic == null)
            {
                return;
            }
            if (tempMusic.Path == NowPlayingList[NowPlayingNumber].Path)//如果用户选择的音乐与当前所播放的音乐是同一个文件，则没有操作
            {
                return;
            }
            else
            {
                PlayState = playState.playing;//改变播放状态
                Open(new Uri(tempMusic.Path));
                Play();
            }
            //下边用来根据当前文件地址在列表中查找相应文件，从而定位用户选择的歌在列表中的位置，方便播放完之后自动进入下一首
            switch (LoopState)
            {
                case loopState.invertedOrder://如果是逆序的话
                case loopState.positiveOrder://如果是顺序播放的话
                case loopState.singleTuneCirculation://如果是单曲循环的话
                    for (int i = 0; i <= MainList.MusicList.Count() - 1; i++) //遍历MusicList列表,一一比对文件地址
                    {
                        if (MainList.MusicList[i].Path == tempMusic.Path)//如果两者地址一样的话
                        {
                            NowPlayingNumber = i;//就更改NowPlayingNumber的值
                        }
                    }
                    break;
                case loopState.disOrder://如果是乱序的话
                    for (int i = 0; i <= MainList.RandomList.Count() - 1; i++)//遍历乱序表
                    {
                        if (MainList.RandomList[i].Path == tempMusic.Path)
                        {
                            NowPlayingNumber = i;
                        }
                    }
                    break;
            }
        }
    }
}