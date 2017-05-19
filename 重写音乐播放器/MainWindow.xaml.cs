using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;

namespace 重写音乐播放器
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        List MainList;
        MusicPlayer MainPlayer;
        public MainWindow()
        {
            InitializeComponent();
            MainList = new List();
            MainPlayer = new MusicPlayer();
            MainPlayer.NowPlayingList = MainList.MusicList;
            //专门的集合对象使用的binding方法
            this.musicListListVIew.ItemsSource = MainPlayer.NowPlayingList;
            //当前播放音乐的显示的binding
            this.musicNameLabel.SetBinding(System.Windows.Controls.Label.ContentProperty, new System.Windows.Data.Binding("NowPlayingNumber") { Source = MainPlayer });
            
            //音量的binding
            volumeSlider.SetBinding(System.Windows.Controls.Slider.ValueProperty, new System.Windows.Data.Binding("Volume") { Source = MainPlayer });

            MainPlayer.MediaEnded += this.AutoNext;
        }
        private void Buton_Click(object sender, RoutedEventArgs e)
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
        private void Last_Click(object sender, RoutedEventArgs e)//上一首
        {
            MainPlayer.Last();
        }
        private void PlayOrPause_Click(object sender, RoutedEventArgs e)//播放暂停
        {
            MainPlayer.PlayorPause();
        }
        private void Next_Click(object sender, RoutedEventArgs e)//下一首
        {
            MainPlayer.Next();
        }

        private void PlayThis_DoubleClick(object sender, MouseButtonEventArgs e)//播放用户在音乐列表里选择的歌曲
        {
            Music TempMusic = musicListListVIew.SelectedItem as Music;
            MainPlayer.UserSayPlayThis(TempMusic, MainList);
        }

        protected void AutoNext(object sender,EventArgs e)
        {
            MainPlayer.Next();
        }

        private void switchLoopStateButton_Click(object sender, RoutedEventArgs e)
        {
            MainPlayer.SwitchLoopState(MainList);
        }
    }
}