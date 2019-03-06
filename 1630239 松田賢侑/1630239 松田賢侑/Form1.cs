using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1630239_松田賢侑
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path = "";
            string Curdir = "C:\\Users\\yuu05\\Anaconda3";//学校のパソコンの個人フォルダをカレントディレクトリに指定する。自分のパソコン内であれば、探したいフォルダ名のパスに書き換える。
            string textfilename = Curdir + "\\pathsave.txt";//のちに保存するためのテキストファイルを準備
            System.IO.StreamWriter textfile;
            string[] files = System.IO.Directory.GetFiles(@Curdir, "*", System.IO.SearchOption.AllDirectories);//個人フォルダ以降のフォルダにあるファイルのパスをすべて配列filesに入れる
            string[] str_files = { };
            str_files = new string[files.Length];//配列str_filesを初期化

            for (int i = 0; i < files.Length; i++)
            { 
                string[] temparray;
                temparray = files[i].ToString().Split('\\');
                str_files[i] = temparray[temparray.Length - 1];
                
            }//配列filesの要素を取り出し、￥で分け、ファイル名の部分をstr_filesに入れる。

            int index = Array.IndexOf(str_files, textBox1.Text);//配列str_filesにtextbox1の内容が含まれていればそのインデックス番号を返し、なければ-1を返す


            if (index == -1)
            {
                MessageBox.Show(textBox1.Text + "は個人フォルダ内に存在しないか、ファイル名が間違ってる可能性があります。もう一度ファイル名を確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string[] temparray2 = files[index].ToString().Split('\\');
                for (int i = 0; i < temparray2.Length - 1; i++)
                {
                    path += temparray2[i];
                    path += "\\";
                    
                }//配列filesのインデックス番号をstring型にし、￥で分け、ファイルのあるディレクトリのパスを作成
                
                DialogResult dr;

                dr = MessageBox.Show("ファイル名　" + textBox1.Text + "が見つかりました。\n パスは" + files[index] + "\nです。エクスプローラーを表示しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (dr == DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start(@path);
                }
                else
                {
                    DialogResult dr2;
                    dr2 = MessageBox.Show("キャンセルされました\nパスをテキストファイルに保存しますか？","確認",MessageBoxButtons.YesNo,MessageBoxIcon.Information);

                    if (dr2 == DialogResult.Yes)
                    {
                        if (System.IO.File.Exists(textfilename))
                        {
                            MessageBox.Show("pathsave.txtにパスを保存しました。");
                            textfile = System.IO.File.AppendText(textfilename);
                            textfile.WriteLine(path+textBox1.Text);
                            textfile.Close();
                        }//pathsave.txtが存在した場合パスを追記する。
                        else
                        {
                            MessageBox.Show("pathsave.txtを作成し、パスを保存しました。");
                            textfile = System.IO.File.CreateText(textfilename);
                            textfile.Close();
                            textfile = System.IO.File.AppendText(textfilename);
                            textfile.WriteLine(path+textBox1.Text);
                            textfile.Close();
                        }//存在しなかった場合pathsave.txtを作成しパスを追記する。
                    }

                    else
                    {
                        MessageBox.Show("キャンセルされました");
                    }
                }
            }
        }
    }
}
