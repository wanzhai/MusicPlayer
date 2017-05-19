using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace 重写音乐播放器
{
    /// <summary>
    /// SmartWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SmartWindow : Window
    {
        List MainList = new List();
        MusicPlayer MainPlayer = new MusicPlayer();
        public SmartWindow()
        {
            InitializeComponent();
        }

        private void Last_Click(object sender, RoutedEventArgs e)
        {
            MainPlayer.Last();
        }

        private void PlayorPause_Click(object sender, RoutedEventArgs e)
        {
            MainPlayer.PlayorPause();
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            MainPlayer.Next();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();//实例化文件夹选择对话框,需要添加System.Windwos.Forms的引用
            dialog.Description = "请选择文件夹";
            dialog.ShowDialog();//显示对话框
            if (dialog.SelectedPath != "")//如果什么都不选的话交给下个函数会出错
            {
                MainList.MakeMusicList(dialog.SelectedPath);
            }
            if (MainList.MusicList.Count == 0)
            {
                return;//如果所选文件夹内没有音乐文件就不执行下边的操作了
            }
        }

        private void SwitchLoopState(object sender, RoutedEventArgs e)
        {
            MainPlayer.SwitchLoopState();
        }
        
        private void PositiveOrder_Click(object sender, RoutedEventArgs e)
        {
            MainPlayer.SwitchLoopState(MusicPlayer.loopState.positiveOrder,MainList);
            //positive.Background = Brushes.Yellow;
        }

        private void InvertedOrder_Click(object sender, RoutedEventArgs e)
        {
            MainPlayer.SwitchLoopState(MusicPlayer.loopState.invertedOrder, MainList);
            //inverted.Background = Brushes.Yellow;
        }

        private void DisOrder_Click(object sender, RoutedEventArgs e)
        {
            MainPlayer.SwitchLoopState(MusicPlayer.loopState.disOrder, MainList);
            //dis.Background = Brushes.Yellow;
        }

        private void SingleTuneCirculation_Click(object sender, RoutedEventArgs e)
        {
            MainPlayer.SwitchLoopState(MusicPlayer.loopState.singleTuneCirculation, MainList);
            //single.Background = Brushes.Yellow;
        }
    }
}
