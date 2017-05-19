using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 重写音乐播放器
{
    class Class1
    {
    }
    //淘汰掉的PlayorPause代码
    /*switch (LoopState)//先检查循环状态，根据循环状态采取不同的方案
            {
                case loopState.positiveOrder://如果是正序播放
                    switch (PlayState)//再检查播放状态
                    {
                        case playState.stoped://如果是停止状态
                            FromStopedToPlaying(NowPlayingList);
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
                    break;
                case loopState.invertedOrder://如果是逆序播放
                    switch (PlayState)//再检查播放状态
                    {
                        case playState.stoped://如果是停止状态
                            FromStopedToPlaying(NowPlayingList);
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
                    break;
                case loopState.disOrder://如果乱序播放
                    switch (PlayState)//再检查播放状态
                    {
                        case playState.stoped://如果是停止状态
                            FromStopedToPlaying(NowPlayingList);
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
                    break;
                case loopState.singleTuneCirculation://如果是单曲循环
                    switch (PlayState)//再检查播放状态
                    {
                        case playState.stoped://如果是停止状态
                            FromStopedToPlaying(NowPlayingList);
                            break;
                        case playState.playing://如果是正在播放的状态
                            PlayState = playState.paused;//修改播放状态标记
                            Pause();//暂停
                            break;
                        case playState.paused://如果是暂停状态
                            Play();//播放之
                            break;
                    }
                    break;
            }*/
}
