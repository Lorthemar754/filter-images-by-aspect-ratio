using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.IO;
using System.Text.RegularExpressions;

namespace WpfApp
{
    public partial class MainWindow : Window
    {
        string folder_path, save_folder = String.Empty, image_path;
        double coef;

        public MainWindow()
        {
            InitializeComponent();
        }

        async private void Button_Click(object sender, RoutedEventArgs e)
        {
            string[] allfiles = allfiles = Directory.GetFiles(folder_path);

            pbStatus.Value = 0;
            pbStatus.Maximum = allfiles.Length;

            string pattern = @"\.(jpg|jpeg|png)$";

            coef = counter();

            for (int i = 0; i < pbStatus.Maximum; i++)
            {
                image_path = allfiles[i];

                if (Regex.IsMatch(image_path, pattern))
                {
                    await Task.Run(() => File_mover());
                }

                pbStatus.Value++;
            }

            MessageBox.Show("Done");
        }

        void File_mover()
        {
            double count;
            using (FileStream stream = new FileStream(image_path, FileMode.Open, FileAccess.Read))
            {
                System.Drawing.Image newImage = System.Drawing.Image.FromStream(stream);
                count = (double)newImage.Width / (double)newImage.Height;
            }

            if (save_folder.Length == 0)
            {
                save_folder = (folder_path + @"\result");
                System.IO.Directory.CreateDirectory(folder_path + @"\result");
            }

            if (Math.Round(count, 1) == coef)
            {
                File.Copy(image_path, System.IO.Path.Combine(save_folder, System.IO.Path.GetFileName(image_path)), true);
            }
        }

        public double counter()
        {
            double hies = Convert.ToInt32(text1.Text),
                weis = Convert.ToInt32(text2.Text);

            return Math.Round(weis / hies, 1);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var dialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
            if (dialog.ShowDialog(this).GetValueOrDefault())
            {
                folder_path = dialog.SelectedPath;
                textfolderimag.Text = dialog.SelectedPath;

                Buttongo.IsEnabled = true;

            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var dialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
            if (dialog.ShowDialog(this).GetValueOrDefault())
            {
                save_folder = dialog.SelectedPath;
                kudasohratext.Text = dialog.SelectedPath;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
        }

        private void text1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsDigit(e.Text, 0));
        }

        private void text2_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsDigit(e.Text, 0));
        }

        private void ProgressBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
        }

        private void TextBox_TextChanged_2(object sender, TextChangedEventArgs e)
        {
        }

        private void TextBox_TextChanged_3(object sender, TextChangedEventArgs e)
        {
        }
    }
}
