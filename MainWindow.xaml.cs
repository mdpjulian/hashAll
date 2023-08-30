using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Security.Cryptography;
using System.ComponentModel;
using System.Windows.Input;

namespace hashAll
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        private List<DataItem> dataList = new();
        public MainWindow()
        {
            InitializeComponent();





            dataList.Add(new DataItem("MD5", ""));
            dataList.Add(new DataItem("SHA256", ""));
            dataList.Add(new DataItem("SHA512", ""));


            hashListView.ItemsSource = dataList;
            hashListView.MouseDoubleClick += MyDataList_MouseDoubleClick;


        }


        private void MyDataList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Retrieve the selected MyDataItem
            DataItem selectedItem = (DataItem)hashListView.SelectedItem;

            // Check if an item is selected
            if (selectedItem != null)
            {
                // Copy the value of Property2 to the clipboard
                Clipboard.SetText(selectedItem.Property2);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {  
            string str = TextboxInput.Text;
            if (!String.IsNullOrEmpty(str))
            {
                dataList[0].Property2 = HashGenerator.ComputeMD5(str);
                dataList[1].Property2 = HashGenerator.ComputeSHA256(str);
                dataList[2].Property2 = HashGenerator.ComputeSHA512(str);

                Header2.Width = 900;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }

    public class DataItem : INotifyPropertyChanged
    {
        private string property2 = string.Empty;
        public string? Property1 { get; set; }

        public string Property2
        {
            get { return property2; }
            set
            {
                if (property2 != value)
                {
                    property2 = value;
                    OnPropertyChanged(nameof(Property2));
                }
            }
        }
        // Add more properties for other columns
        public DataItem(string property1,
                          string property2 = "")
        {
            Property1 = property1;
            Property2 = property2;
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public class HashGenerator
    {
        public static string ComputeMD5(string s)
        {
            using MD5 md5 = MD5.Create();
            byte[] hashValue = md5.ComputeHash(Encoding.UTF8.GetBytes(s));
            return Convert.ToHexString(hashValue);
        }

       public static string ComputeSHA256(string s)
        {
            using SHA256 sha256 = SHA256.Create();
            byte[] hashValue = sha256.ComputeHash(Encoding.UTF8.GetBytes(s));
            return Convert.ToHexString(hashValue);
        }
        public static string ComputeSHA512(string s)
        {
            using SHA512 sha512 = SHA512.Create();
            byte[] hashValue = sha512.ComputeHash(Encoding.UTF8.GetBytes(s));
            return Convert.ToHexString(hashValue);
        }
    }
}


